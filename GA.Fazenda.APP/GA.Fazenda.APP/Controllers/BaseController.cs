using GA.Fazenda.Business.Interfaces;
using GA.Fazenda.Business.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GA.Fazenda.APP.Controllers
{
    public abstract class BaseController : Controller
	{
		private readonly INotificador _notificador;

		public BaseController(INotificador notificador)
		{
			_notificador = notificador;
		}

		protected bool OperacaoValida()
		{
			return !_notificador.TemNotificacao();
		}

		protected void NotificarErro(string mensagem)
		{
			_notificador.Handle(new Notificacao(mensagem));
		}

		protected T DeserializarObjetoResponse<T>(string dados)
		{
			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};

			return JsonSerializer.Deserialize<T>(dados, options);
		}

	}
}
