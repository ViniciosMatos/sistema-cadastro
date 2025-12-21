using cadastro.Domain.Entities;

namespace cadastro.Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllUsuariosAsync(CancellationToken ct);
        Task<Usuario?> GetUsuarioByIdAsync(int id, CancellationToken ct);
        Task<Usuario?> GetUsuarioByEmailAsync(string email, CancellationToken ct);
        Task<Usuario> PostUsuarioAsync(Usuario usuario, CancellationToken ct);
        Task UpdateUsuarioAsync(Usuario usuario, CancellationToken ct);
        Task DeleteUsuarioAsync(Usuario usuario, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
        Task<bool?> EmailExistsAsync(string email, CancellationToken ct);
    }
}