using GA.Fazenda.Business.Models;
using System.Collections;
using System.Threading.Tasks;

namespace GA.Fazenda.Business.Interfaces.Repositorios
{
    public interface IFazendaRepository : IRepository<EntidadeFazenda>
    {
        Task<IEnumerable> ObterComAnimais(int id);
    }
}
