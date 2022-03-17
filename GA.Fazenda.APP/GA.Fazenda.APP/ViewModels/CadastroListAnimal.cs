using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.Fazenda.APP.ViewModels
{
    public class CadastroListAnimal
    {
        [StringLength(15)]
        public string Tag { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Fazenda")]
        public int? FazendaId { get; set; }

        [NotMapped]
        [DisplayName("Animais")]
        public List<SelectListItem> AnimaisLista { get; set; } = new List<SelectListItem>();

        public List<AnimalVM> Animais { get; set; }
        public string ListAnimaisJson { get; set; }
        public IEnumerable<FazendaVM> Fazendas { get; set; }
    }
}
