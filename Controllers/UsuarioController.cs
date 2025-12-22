using cadastro.Application.DTOs;
using cadastro.Application.Interfaces;
using cadastro.Application.Services;
using cadastro.Application.Validators;
using cadastro.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace cadastro.Controllers
{
    [ApiController]
    [Route("/usuarios/")]
    public class UsuarioController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<Usuario>> GetAll(IUsuarioService service, CancellationToken ct)
        {
            var listaDeUsuarios = await service.ListarUsuariosAsync(ct);
            return Ok(listaDeUsuarios);
        }

        [HttpGet("/{id}/")]
        public async Task<ActionResult<Usuario>> GetById(int id, IUsuarioService service, CancellationToken ct)
        {
            var usuarioEncontrado = await service.ObterUsuarioPorIdAsync(id, ct);
            return Ok(usuarioEncontrado);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Post(IUsuarioService service, UsuarioCreateDto dto, IValidator<UsuarioCreateDto> validator, CancellationToken ct)
        {
            var resultadoValidator = await validator.ValidateAsync(dto, ct);

            if (!resultadoValidator.IsValid)
                return ValidationProblem();

            try
            {
                var usuario = await service.CriarUsuarioAsync(dto, ct);
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

        [HttpDelete("/{id}/")]
        public async Task<ActionResult<Usuario>> Delete(int id, IUsuarioService service, CancellationToken ct)
        {
            var deletarUsuario = await service.DeletarUsuarioAsync(id, ct);

            if (!deletarUsuario)
                return NotFound();
            
            return NoContent();
        }

        [HttpPut("/{id}/")]
        public async Task<ActionResult<Usuario>> Put(int id, IUsuarioService service, UsuarioUpdateDto dto,IValidator<UsuarioUpdateDto> validator, CancellationToken ct)
        {
            var resultadoValidator = await validator.ValidateAsync(dto, ct);

            if (!resultadoValidator.IsValid)
                return ValidationProblem();

            try
            {
                await service.AtualizarUsuarioAsync(id, dto, ct);
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

        [HttpPatch("/{id}/")]
        public async Task<ActionResult<Usuario>> Patch(int id, IUsuarioService service, UsuarioUpdateDto dto, IValidator<UsuarioUpdateDto> validator, CancellationToken ct )
        {
            var resultadoValidator = await validator.ValidateAsync(dto, ct);

            if(!resultadoValidator.IsValid)
                return NotFound();
            
            try
            {
                await service.AtualizarParcialUsuarioAsync(id, dto, ct);
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