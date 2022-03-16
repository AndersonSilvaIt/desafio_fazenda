using GA.Fazenda.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GA.Fazenda.Business.Interfaces.Repositorios
{
    public interface IAnimalRepository : IRepository<Animal> 
    {
        Task<IEnumerable<Animal>> ObterListaAnimaisComFazendas();
        Task<Animal> ObterPorFazendaId(int fazendaId);
        Task<Animal> ObterAnimalComFazenda(int id); //Retorna o Produto e o fornecedor dele
    }


}
