using cadastro.Application.DTOs;
using cadastro.Domain.Entities;

namespace cadastro.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> ListarUsuariosAsync(CancellationToken ct);
        Task<UsuarioReadDto?> ObterUsuarioPorIdAsync(int id, CancellationToken ct);
        Task<UsuarioReadDto?> ObterUsuarioPorEmailAsync(string email, CancellationToken ct);
        Task<Usuario> CriarUsuarioAsync(UsuarioCreateDto dto, CancellationToken ct);
        Task AtualizarUsuarioAsync(int id, UsuarioUpdateDto dto, CancellationToken ct);
        Task AtualizarParcialUsuarioAsync(int id, UsuarioUpdateDto dto, CancellationToken ct);
        Task<bool> DeletarUsuarioAsync(int id, CancellationToken ct);
        Task<bool> VerificarEmailExistenteAsync(string email, CancellationToken ct);

    }
}