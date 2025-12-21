using cadastro.Application.DTOs;
using FluentValidation;

namespace cadastro.Application.Validators
{
    public class UsuarioUpdateValidator : AbstractValidator<UsuarioUpdateDto>
    {
        public UsuarioUpdateValidator()
        {
            RuleFor(u => u.Nome)
                .NotEmpty()
                .WithMessage("O nome é obrigatório")
                .MinimumLength(3)
                .WithMessage("Minimo de 3 carateres")
                .MaximumLength(100)
                .WithMessage("Máximo de 100 caracteres")
                .When(u => !string.IsNullOrEmpty(u.Senha));

            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage("O email é obrigatório")
                .When(u => !string.IsNullOrEmpty(u.Senha))
                .MaximumLength(150)
                .WithMessage("No máximo 150 caracteres")
                .When(u => !string.IsNullOrEmpty(u.Senha))
                .EmailAddress()
                .WithMessage("Formato do email inválido")
                .When(u => !string.IsNullOrEmpty(u.Senha));
            
            RuleFor(u => u.Senha)
                .NotEmpty()
                .WithMessage("A senha é obrigatória")
                .When(u => !string.IsNullOrEmpty(u.Senha))
                .MinimumLength(6)
                .WithMessage("No mínimo 6 caracteres")
                .When(u => !string.IsNullOrEmpty(u.Senha))
                .MaximumLength(32)
                .WithMessage("No máximo 32 caracteres")
                .When(u => !string.IsNullOrEmpty(u.Senha));
        }
    }
}