using System.Threading.Tasks;

namespace GA.Fazenda.Business.Interfaces.Servicos
{
    public interface IBaseService
    {
        Task<bool> Commit();
    }
}
