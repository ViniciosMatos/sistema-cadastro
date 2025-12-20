using cadastro.Application.DTOs;
using cadastro.Domain.Entities;
using cadastro.Application.Services;

namespace cadastro.Application.Services
{
    public static class UsuarioFactory
    {
        public static UsuarioReadDto? ToReadDto(Usuario usuario)
        {
            if (usuario == null) return null;

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
            
            if ()

            if (string.IsNullOrWhiteSpace(dto.Senha))
                throw new ArgumentException("A senha não pode estar vazio");
            
            Usuario novoUsuario = new Usuario();
            novoUsuario.Nome = dto.Nome;
            novoUsuario.Email = dto.Email;
            novoUsuario.Senha = dto.Senha;
            novoUsuario.DataCriacao = DateTime.Now;
            return novoUsuario;
        }

        public static Usuario Put(UsuarioUpdateDto dto, Usuario usuario)
        {
            if (usuario == null)
                return null;
                
            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new ArgumentException("O nome não pode estar vazio");

            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ArgumentException("O email não pode estar vazio");
            
            UsuarioService

            if (string.IsNullOrWhiteSpace(dto.Senha))
                throw new ArgumentException("A senha não pode estar vazio");
            
            
            usuario.Nome = dto.Nome;
            usuario.Email = dto.Email;
            usuario.Senha = dto.Senha;
            return usuario;
        }

        public static Usuario Patch(UsuarioUpdateDto dto, Usuario usuario)
        {
            if (usuario == null) return null;

            if (!string.IsNullOrWhiteSpace(dto.Nome))
                usuario.Nome = dto.Nome;

            if (!string.IsNullOrWhiteSpace(dto.Email))
                usuario.Email = dto.Email;

            if (!string.IsNullOrWhiteSpace(dto.Senha))
                usuario.Senha = dto.Senha;
            
            return usuario;
        }
    }
}
