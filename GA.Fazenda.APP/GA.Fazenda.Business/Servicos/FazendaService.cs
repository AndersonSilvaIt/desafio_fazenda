using GA.Fazenda.Business.Interfaces;
using GA.Fazenda.Business.Interfaces.Repositorios;
using GA.Fazenda.Business.Interfaces.Servicos;
using GA.Fazenda.Business.Models;
using GA.Fazenda.Business.Validacoes;
using System.Linq;
using System.Threading.Tasks;

namespace GA.Fazenda.Business.Servicos
{
    public class FazendaService : ServicoBase, IFazendaService
	{
		private readonly IFazendaRepository _fazendaRepository;
		private readonly IAnimalRepository _animalRepository;

		public FazendaService(IFazendaRepository fazendaRepository,
								 IAnimalRepository animalRepository,
								INotificador notificador, IUnitOfWork uow) : base(notificador, uow)
		{
			_fazendaRepository = fazendaRepository;
			_animalRepository = animalRepository;
		}

		public async Task Adicionar(EntidadeFazenda fazenda)
		{
			if (!ExecutarValidacao(new FazendaValidator(), fazenda)) return;

			if ((await _fazendaRepository.Buscar(f => f.Nome == fazenda.Nome)).Any())
			{
				Notificar("Já existe um fazenda com este nome informado.");
				return;
			}

			_fazendaRepository.Adicionar(fazenda);
		}

		public async Task Atualizar(EntidadeFazenda fazenda)
		{
			if (!ExecutarValidacao(new FazendaValidator(), fazenda)) return;

			if ((await _fazendaRepository.Buscar(f => f.Nome == fazenda.Nome && f.Id != fazenda.Id)).Any())
			{
				Notificar("Já existe um fazenda com este nome informado.");
				return;
			}

			_fazendaRepository.Atualizar(fazenda);
		}

        public async Task<bool> Commited()
        {
			return await Commit();
        }

        public void Remover(int id)
		{
			if (_animalRepository.ObterPorFazendaId(id).Result != null)
			{
				Notificar("O fazenda possui animais relacionados e não pode ser excluída!");
				return;
			}

			_fazendaRepository.Remover(id);
		}
	}
}
