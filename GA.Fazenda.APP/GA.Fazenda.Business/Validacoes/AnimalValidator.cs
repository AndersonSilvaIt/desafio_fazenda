using FluentValidation;
using GA.Fazenda.Business.Models;

namespace GA.Fazenda.Business.Validacoes
{
    public class AnimalValidator : AbstractValidator<Animal>
    {
        public AnimalValidator()
        {
            RuleFor(f => f.Tag)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(1, 15)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MatLength} caracteres.");

            RuleFor(f => NumeroValidator.Validar(f.Tag)).Equal(true)
                    .WithMessage("O campo {PropertyName} éstá inválido.");
        }
    }

    internal  class NumeroValidator
    {
        public static bool Validar(string tag)
        {
            var tagNumeros = ApenasNumeros(tag);

            if (tagNumeros.Length <= 0 || tagNumeros.Length > 15) return false;

            return true;
        }

        public static string ApenasNumeros(string valor)
        {
            var onlyNumber = "";
            foreach (var s in valor)
            {
                if (char.IsDigit(s))
                {
                    onlyNumber += s;
                }
            }
            return onlyNumber.Trim();
        }
    }
}
