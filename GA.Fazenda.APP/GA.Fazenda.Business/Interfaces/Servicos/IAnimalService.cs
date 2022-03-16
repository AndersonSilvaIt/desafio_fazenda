using GA.Fazenda.Business.Models;
using System.Threading.Tasks;

namespace GA.Fazenda.Business.Interfaces.Servicos
{
    public interface IAnimalService
    {
        Task Adicionar(Animal animal);
        Task Atualizar(Animal animal);
        void Remover(int id);
    }
}
