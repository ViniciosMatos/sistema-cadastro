using cadastro.Application.DTOs;
using FluentValidation;

namespace cadastro.Application.Validators
{
    public class UsuarioCreateValidator : AbstractValidator<UsuarioCreateDto>
    {
        public UsuarioCreateValidator()
        {
            RuleFor(u => u.Nome)
                .NotEmpty()
                .WithMessage("O nome é obrigatório")
                .MinimumLength(3)
                .WithMessage("Minimo de 3 carateres")
                .MaximumLength(100)
                .WithMessage("Máximo de 100 caracteres");

            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage("O email é obrigatório")
                .MaximumLength(150)
                .WithMessage("No máximo 150 caracteres")
                .EmailAddress()
                .WithMessage("Formato do email inválido");
            
            RuleFor(u => u.Senha)
                .NotEmpty()
                .WithMessage("A senha é obrigatória")
                .MinimumLength(6)
                .WithMessage("No mínimo 6 caracteres")
                .MaximumLength(32)
                .WithMessage("No máximo 32 caracteres");
                
        }
    }
}