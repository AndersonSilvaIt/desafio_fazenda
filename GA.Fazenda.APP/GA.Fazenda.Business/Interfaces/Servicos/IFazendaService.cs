using GA.Fazenda.Business.Models;
using System.Threading.Tasks;

namespace GA.Fazenda.Business.Interfaces.Servicos
{
    public interface IFazendaService
    {
		Task Adicionar(EntidadeFazenda fazenda);
		Task Atualizar(EntidadeFazenda fazenda);
		void Remover(int id);
		Task<bool> Commited();
	}
}
