using cadastro.Application.DTOs;
using cadastro.Application.Interfaces;
using cadastro.Domain.Entities;

namespace cadastro.Application.Services
{
    public static class UsuarioFactory
    {
        public static UsuarioReadDto? ToReadDto(Usuario usuario)
        {
            if (usuario == null)
                throw new InvalidOperationException("Usuário não encontrado.");

            return new UsuarioReadDto(
                Id: usuario.Id,
                Nome: usuario.Nome,
                Email: usuario.Email
            );
        }

        public static Usuario Criar(UsuarioCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new ArgumentException("O nome não pode estar vazio");

            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ArgumentException("O email não pode estar vazio");

            if (string.IsNullOrWhiteSpace(dto.Senha))
                throw new ArgumentException("A senha não pode estar vazio");

            Usuario novoUsuario = new Usuario();
            novoUsuario.Nome = dto.Nome;
            novoUsuario.Email = dto.Email;
            novoUsuario.Senha = dto.Senha;
            novoUsuario.DataCriacao = DateTime.Now;
            novoUsuario.DataAtualizacao = null;
            return novoUsuario;
        }

        public static Usuario Put(UsuarioUpdateDto dto, Usuario usuario)
        {
            if (usuario == null)
                throw new InvalidOperationException("Usuário não encontrado.");

            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new ArgumentException("O nome não pode estar vazio");

            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ArgumentException("O email não pode estar vazio");


            if (string.IsNullOrWhiteSpace(dto.Senha))
                throw new ArgumentException("A senha não pode estar vazio");


            usuario.Nome = dto.Nome;
            usuario.Email = dto.Email;
            usuario.Senha = dto.Senha;
            usuario.DataAtualizacao = DateTime.Now;
            return usuario;
        }

        public static async Task<Usuario> Patch(IUsuarioRepository _repo, UsuarioUpdateDto dto, Usuario usuario, CancellationToken ct)
        {
            if (usuario == null)
                throw new InvalidOperationException("Usuário não encontrado.");
            if (!string.IsNullOrWhiteSpace(dto.Nome))
            {
                usuario.Nome = dto.Nome;
            }

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                if (await _repo.EmailExistsAsync(dto.Email, ct) && usuario.Email != dto.Email)
                    throw new ArgumentException("O email já está sendo usado por outro usuario.");
                usuario.Email = dto.Email;
            }

            if (!string.IsNullOrWhiteSpace(dto.Senha))
                usuario.Senha = dto.Senha;

            usuario.DataAtualizacao = DateTime.Now;

            return usuario;
        }
    }
}
