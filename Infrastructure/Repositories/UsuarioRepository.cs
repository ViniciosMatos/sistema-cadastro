using cadastro.Application.Interfaces;
using cadastro.Domain.Entities;
using cadastro.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace cadastro.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync(CancellationToken ct)
        {
            return await _context.Usuarios.ToListAsync(ct);
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id, CancellationToken ct)
        {
            return await _context.Usuarios.FindAsync(new object[] { id }, ct);
        }

        public async Task<Usuario> PostUsuarioAsync(Usuario usuario, CancellationToken ct)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync(ct);
            return usuario;
        }

       public async Task UpdateUsuarioAsync(Usuario usuario, CancellationToken ct)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteUsuarioAsync(Usuario usuario, CancellationToken ct)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync(ct);
        }
        
        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _context.SaveChangesAsync(ct);
        }
    }
}