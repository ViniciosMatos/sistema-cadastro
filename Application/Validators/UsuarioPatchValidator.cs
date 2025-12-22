using cadastro.Application.DTOs;
using FluentValidation;

namespace cadastro.Application.Validators
{
    public class UsuarioPatchValidator : AbstractValidator<UsuarioUpdateDto>
    {
        public UsuarioPatchValidator()
        {
            RuleFor(u => u.Nome)
                .MinimumLength(3)
                .WithMessage("Minimo de 3 carateres")
                .When(u => !string.IsNullOrEmpty(u.Nome))
                .MaximumLength(100)
                .WithMessage("Máximo de 100 caracteres")
                .When(u => !string.IsNullOrEmpty(u.Nome));

            RuleFor(u => u.Email)
                .MaximumLength(150)
                .WithMessage("No máximo 150 caracteres")
                .When(u => !string.IsNullOrEmpty(u.Email))
                .EmailAddress()
                .WithMessage("Formato do email inválido")
                .When(u => !string.IsNullOrEmpty(u.Email));
            
            RuleFor(u => u.Senha)
                .MinimumLength(6)
                .WithMessage("No mínimo 6 caracteres")
                .When(u => !string.IsNullOrEmpty(u.Senha))
                .MaximumLength(32)
                .WithMessage("No máximo 32 caracteres")
                .When(u => !string.IsNullOrEmpty(u.Senha));
        }
    }
}