using FluentValidation;

namespace MazzaTech.Business.Models.Validations
{
    public class ClienteValidation : AbstractValidator<ClienteEntity>
    {
        public ClienteValidation()
        {
            RuleFor(f => f.Nome)
                .NotEmpty().WithMessage("Nome is required")
                .Length(2, 100)
                .WithMessage("A valid nome is required");

            RuleFor(s => s.Email).NotEmpty().WithMessage("Email address is required")
                     .EmailAddress().WithMessage("A valid email is required");
        }
    }
}