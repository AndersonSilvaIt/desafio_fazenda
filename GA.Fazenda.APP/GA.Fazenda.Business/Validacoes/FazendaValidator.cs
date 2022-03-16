using FluentValidation;
using GA.Fazenda.Business.Models;

namespace GA.Fazenda.Business.Validacoes
{
    public class FazendaValidator : AbstractValidator<EntidadeFazenda>
    {
        public FazendaValidator()
        {
            RuleFor(f => f.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(2, 100)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MatLength} caracteres.");
        }
    }
}
