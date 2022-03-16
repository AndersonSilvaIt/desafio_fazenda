using GA.Fazenda.Business.Notificacoes;
using System.Collections.Generic;

namespace GA.Fazenda.Business.Interfaces
{
    public interface INotificador
	{
		bool TemNotificacao();
		List<Notificacao> ObterNotificacoes();
		void Handle(Notificacao notificacao);
	}
}
