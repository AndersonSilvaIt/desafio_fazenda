using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GA.Fazenda.APP.ViewModels
{
    public class AnimalVM : VMBase
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(15, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        public string Tag { get; set; }

        public FazendaVM Fazenda { get; set; }

        public IEnumerable<FazendaVM> Fazendas { get; set; }
    }
}