using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GA.Fazenda.APP.ViewModels
{
    public class FazendaVM
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        public IEnumerable<AnimalVM> Animais { get; set; }
    }
}
