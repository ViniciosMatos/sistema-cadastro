namespace cadastro.Application.DTOs
{
    public record UsuarioUpdateDto
    (
        string? Nome,
        string? Email,
        string? Senha
    );
}