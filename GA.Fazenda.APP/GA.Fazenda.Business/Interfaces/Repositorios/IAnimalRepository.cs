using GA.Fazenda.Business.Models;
using System.Threading.Tasks;

namespace GA.Fazenda.Business.Interfaces.Repositorios
{
    public interface IAnimalRepository : IRepository<Animal> 
    {
        Task<Animal> ObterPorFazendaId(int fazendaId);
    }


}
