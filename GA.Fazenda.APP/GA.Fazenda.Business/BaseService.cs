using FluentValidation.Results;
using FluentValidation;
using GA.Fazenda.Business.Interfaces;
using GA.Fazenda.Business.Notificacoes;
using System.Threading.Tasks;
using GA.Fazenda.Business.Models;

namespace GA.Fazenda.Business
{
    public abstract class BaseService
	{
		private readonly INotificador _notificador;

		private readonly IUnitOfWork _uow;

		public BaseService(INotificador notificador, IUnitOfWork uow)
		{
			_uow = uow;
			_notificador = notificador;
		}

		public async Task<bool> Commit()
		{
			return await _uow.Commit();
		}

		protected void Notificar(ValidationResult validationResult)
		{
			foreach (var item in validationResult.Errors)
			{
				Notificar(item.ErrorMessage);
			}
		}

		protected void Notificar(string mensagem)
		{
			_notificador.Handle(new Notificacao(mensagem));
		}

		protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : EntidadeBase
		{
			var validator = validacao.Validate(entidade);
			if (validator.IsValid) return true;

			Notificar(validator);
			return false;
		}

	}
}
