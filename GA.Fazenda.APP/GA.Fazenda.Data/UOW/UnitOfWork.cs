using GA.Fazenda.Business.Interfaces;
using GA.Fazenda.Data.Context;
using System;
using System.Threading.Tasks;

namespace GA.Fazenda.Data.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IContext _context;

        public UnitOfWork(FazendaContexto context)
        {
            _context = context;
        }

        public async Task<bool> Commit()
        {
            int changeAmount = 0;
            try
            {
                changeAmount = await _context.SaveChangesContext();
            }
            catch (Exception ex)
            {
                // Realizar algum log aqui, talvez ...
            }
            return changeAmount > 0;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
