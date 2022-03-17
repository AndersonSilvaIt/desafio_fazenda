using GA.Fazenda.Business.Interfaces.Repositorios;
using GA.Fazenda.Business.Models;
using GA.Fazenda.Data.Context;
using System.Collections;
using System.Threading.Tasks;

namespace GA.Fazenda.Data.Repository
{
    public class FazendaRepository : BaseRepository<EntidadeFazenda>, IFazendaRepository
    {
        public FazendaRepository(FazendaContexto contexto) : base(contexto)
        { }

    }
}
