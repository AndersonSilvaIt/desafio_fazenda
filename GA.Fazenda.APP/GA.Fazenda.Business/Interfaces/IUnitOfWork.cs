using System;
using System.Threading.Tasks;

namespace GA.Fazenda.Business.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
