
namespace cadastro.Application.DTOs
{
    public record UsuarioCreateDto
    (
        string Nome,
        string Email,
        string Senha
    );
}