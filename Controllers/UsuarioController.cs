using cadastro.Application.DTOs;
using cadastro.Application.Interfaces;
using cadastro.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace cadastro.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Usuario>> GetAll(CancellationToken ct)
        {
            var listaDeUsuarios = await _service.ListarUsuariosAsync(ct);
            return Ok(listaDeUsuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetById(int id, CancellationToken ct)
        {
            var usuarioEncontrado = await _service.ObterUsuarioPorIdAsync(id, ct);
            return Ok(usuarioEncontrado);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Post([FromBody] UsuarioCreateDto dto, [FromServices] IValidator<UsuarioCreateDto> validator, CancellationToken ct)
        {
            var resultadoValidator = await validator.ValidateAsync(dto, ct);

            if (!resultadoValidator.IsValid)
                return ValidationProblem();

            try
            {
                var usuario = await _service.CriarUsuarioAsync(dto, ct);
                return Created($"/usuarios/{usuario.Id}", usuario);
            }
            catch (ArgumentException ex) when (ex.Message.Contains("O email já está sendo usado por outro usuario."))
            {
                return Conflict(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> Delete(int id, CancellationToken ct)
        {
            var deletarUsuario = await _service.DeletarUsuarioAsync(id, ct);

            if (!deletarUsuario)
                return NotFound();
            
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> Put(int id, [FromBody] UsuarioUpdateDto dto, [FromServices] IValidator<UsuarioUpdateDto> validator, CancellationToken ct)
        {
            var resultadoValidator = await validator.ValidateAsync(dto, ct);

            if (!resultadoValidator.IsValid)
                return ValidationProblem();

            try
            {
                await _service.AtualizarUsuarioAsync(id, dto, ct);
                return Ok();
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("O email já está sendo usado por outro usuario."))
            {
                return Conflict(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Usuario>> Patch(int id, [FromBody] UsuarioUpdateDto dto, [FromServices] IValidator<UsuarioUpdateDto> validator, CancellationToken ct )
        {
            var resultadoValidator = await validator.ValidateAsync(dto, ct);

            if(!resultadoValidator.IsValid)
                return ValidationProblem();
            
            try
            {
                await _service.AtualizarParcialUsuarioAsync(id, dto, ct);
                return Ok(dto);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("O email já está sendo usado por outro usuario."))
            {
                return Conflict(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}