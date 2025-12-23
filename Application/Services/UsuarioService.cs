using cadastro.Application.DTOs;
using cadastro.Application.Interfaces;
using cadastro.Domain.Entities;

namespace cadastro.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;
        public UsuarioService(IUsuarioRepository repository)
        {
            _repo = repository;
        }

        public async Task<IEnumerable<Usuario>> ListarUsuariosAsync(CancellationToken ct)
        {
            return await _repo.GetAllUsuariosAsync(ct);
        }

        public async Task<UsuarioReadDto?> ObterUsuarioPorIdAsync(int id, CancellationToken ct)
        {
            var usuario = await _repo.GetUsuarioByIdAsync(id, ct);
            if (usuario == null) 
                throw new InvalidOperationException("Usuário não encontrado.");

            var lerUsuario = UsuarioFactory.ToReadDto(usuario);
            return lerUsuario;
        }

        public async Task<UsuarioReadDto?> ObterUsuarioPorEmailAsync(string email, CancellationToken ct)
        {
            var usuario = await _repo.GetUsuarioByEmailAsync(email, ct);
            if (usuario == null) 
                throw new InvalidOperationException("Usuário não encontrado.");

            var lerUsuario = UsuarioFactory.ToReadDto(usuario);
            return lerUsuario;
        }

        public async Task<Usuario> CriarUsuarioAsync(UsuarioCreateDto dto, CancellationToken ct)
        {
            var usuario = UsuarioFactory.Criar(dto);
            if (usuario == null) 
                throw new InvalidOperationException("Erro ao criar usuário.");

            if (await _repo.EmailExistsAsync(usuario.Email, ct))
                throw new ArgumentException("O email já está sendo usado por outro usuario.");

            await _repo.PostUsuarioAsync(usuario, ct);
            await _repo.SaveChangesAsync(ct);
            return usuario;
        }
        
        public async Task AtualizarUsuarioAsync(int id, UsuarioUpdateDto dto, CancellationToken ct)
        {
            var usuario = await _repo.GetUsuarioByIdAsync(id, ct);
            if (usuario == null)
                throw new InvalidOperationException("Usuário não encontrado."); 

            var atualizarUsuario = UsuarioFactory.Put(dto, usuario);

            if (await _repo.EmailExistsAsync(usuario.Email, ct) && atualizarUsuario.Email != usuario.Email)
                throw new ArgumentException("O email já está sendo usado por outro usuario.");

            await _repo.UpdateUsuarioAsync(atualizarUsuario, ct);
            await _repo.SaveChangesAsync(ct);
        }
        public async Task AtualizarParcialUsuarioAsync(int id, UsuarioUpdateDto dto, CancellationToken ct)
        {
            var usuario = await _repo.GetUsuarioByIdAsync(id, ct);
            if (usuario == null)
                throw new InvalidOperationException("Usuário não encontrado.");

            var atualizarUsuario = UsuarioFactory.Patch(dto, usuario);

            if (await _repo.EmailExistsAsync(usuario.Email, ct) && atualizarUsuario.Email != usuario.Email)
                throw new ArgumentException("O email já está sendo usado por outro usuario.");
            
            await _repo.UpdateUsuarioAsync(atualizarUsuario, ct);
            await _repo.SaveChangesAsync(ct);
        }
        public async Task<bool> DeletarUsuarioAsync(int id, CancellationToken ct)
        {
            var usuario = await _repo.GetUsuarioByIdAsync(id, ct);
            if (usuario == null) 
                throw new InvalidOperationException("Usuário não encontrado.");
            
            await _repo.DeleteUsuarioAsync(usuario, ct);
            await _repo.SaveChangesAsync(ct);

            return true;
            
        }
        public async Task<bool?> VerificarEmailExistenteAsync(string email, CancellationToken ct)
        {
            if (await _repo.EmailExistsAsync(email, ct))
                return false;
            return true;
        }
    }
}