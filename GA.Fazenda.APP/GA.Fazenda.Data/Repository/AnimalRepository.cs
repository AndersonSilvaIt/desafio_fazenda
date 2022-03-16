using GA.Fazenda.Business.Interfaces.Repositorios;
using GA.Fazenda.Business.Models;
using GA.Fazenda.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GA.Fazenda.Data.Repository
{
    public class AnimalRepository : BaseRepository<Animal>, IAnimalRepository
    {
        public AnimalRepository(FazendaContexto contexto) : base(contexto) { }

        public async Task<Animal> ObterPorFazendaId(int fazendaId)
        {
            return await Db.Animais.AsNoTracking()
                    .FirstOrDefaultAsync(f => f.FazendaId == fazendaId);
        }
    }
}
