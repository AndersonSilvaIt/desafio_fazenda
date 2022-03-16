using System;
using System.Threading.Tasks;

namespace GA.Fazenda.Business.Interfaces
{
    public interface IContext : IDisposable
    {
       public Task<int> SaveChangesContext();
    }
}
