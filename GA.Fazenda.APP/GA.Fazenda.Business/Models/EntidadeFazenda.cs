using System.Collections.Generic;

namespace GA.Fazenda.Business.Models
{
    public class EntidadeFazenda : EntidadeBase
    {
        public string Nome { get; set; }

        public IEnumerable<Animal> Animais { get; set; }
    }
}
