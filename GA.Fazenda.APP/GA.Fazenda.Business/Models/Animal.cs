namespace GA.Fazenda.Business.Models
{
    public class Animal : EntidadeBase
    {
        public string Tag { get; set; }

        public EntidadeFazenda Fazenda { get; set; }
        public int FazendaId { get; set; }
    }
}
