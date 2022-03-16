using GA.Fazenda.Business.Interfaces;
using GA.Fazenda.Business.Interfaces.Repositorios;
using GA.Fazenda.Business.Interfaces.Servicos;
using GA.Fazenda.Business.Models;
using GA.Fazenda.Business.Validacoes;
using System.Linq;
using System.Threading.Tasks;

namespace GA.Fazenda.Business.Servicos
{
	public class AnimalService : ServicoBase, IAnimalService
	{
		private readonly IAnimalRepository _animalRepository;

		public AnimalService(IAnimalRepository animalRepository,
								INotificador notificador, IUnitOfWork uow) : base(notificador, uow)
		{
			_animalRepository = animalRepository;
		}

		public async Task Adicionar(Animal animal)
		{
			if (!ExecutarValidacao(new AnimalValidator(), animal)) return;

			if ((await _animalRepository.Buscar(f => f.Tag == animal.Tag)).Any())
			{
				Notificar("Já existe um animal com esta tag informada.");
				return;
			}

			_animalRepository.Adicionar(animal);
		}

		public async Task Atualizar(Animal animal)
		{
			if (!ExecutarValidacao(new AnimalValidator(), animal)) return;

			if ((await _animalRepository.Buscar(f => f.Tag == animal.Tag && f.Id != animal.Id)).Any())
			{
				Notificar("Já existe um animal com esta tag informado.");
				return;
			}

			_animalRepository.Atualizar(animal);
		}

        public async Task<bool> Commited()
        {
			return await Commit();
        }

        public void Remover(int id)
		{
			_animalRepository.Remover(id);
		}
	}
}
