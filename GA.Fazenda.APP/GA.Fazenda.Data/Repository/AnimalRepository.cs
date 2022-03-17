using GA.Fazenda.Business.Interfaces.Repositorios;
using GA.Fazenda.Business.Models;
using GA.Fazenda.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GA.Fazenda.Data.Repository
{
    public class AnimalRepository : BaseRepository<Animal>, IAnimalRepository
    {
        public AnimalRepository(FazendaContexto contexto) : base(contexto) { }

        public async Task<Animal> ObterAnimalComFazenda(int id)
        {
            return await Db.Animais.AsNoTracking().Include(f => f.Fazenda)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Animal>> ObterListaAnimaisComFazendas()
        {
            return await Db.Animais.AsNoTracking().Include(f => f.Fazenda)
                        .OrderBy(a => a.Tag).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Animal>> ObterListaAnimaisComFazendasPorFiltro(string tag, int fazendaId)
        {
            var query = Db.Animais.AsNoTracking().Include(f => f.Fazenda);
            List<Animal> lista;
            tag = string.IsNullOrWhiteSpace(tag) ? "" : tag;

            if (fazendaId > 0)
                lista = await query.Where(x => x.Tag.Contains(tag) && x.FazendaId == fazendaId).OrderBy(x => x.Tag).AsNoTracking().ToListAsync();
            else
                lista = await query.Where(x => x.Tag.Contains(tag)).OrderBy(x => x.Tag).AsNoTracking().ToListAsync();

            return lista;
        }

        public async Task<Animal> ObterPorFazendaId(int fazendaId)
        {
            return await Db.Animais.AsNoTracking()
                    .FirstOrDefaultAsync(f => f.FazendaId == fazendaId);
        }
    }
}
