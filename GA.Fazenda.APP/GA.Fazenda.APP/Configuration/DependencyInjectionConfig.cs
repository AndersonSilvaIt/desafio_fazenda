using GA.Fazenda.Business.Interfaces;
using GA.Fazenda.Business.Notificacoes;
using GA.Fazenda.Data.Context;
using Microsoft.Extensions.DependencyInjection;

namespace GA.Fazenda.APP.Configuration
{
    public static class DependencyInjectionConfig
    {
		public static IServiceCollection ResolveDependencies(this IServiceCollection services)
		{

			services.AddScoped<FazendaContexto>();
			//services.AddScoped<IProdutoRepository, ProdutoRepository>();
			//services.AddScoped<IFornecedorRepository, FornecedorRepository>();
			//services.AddScoped<IEnderecoRepository, EnderecoRepository>();
			//services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();

			services.AddScoped<INotificador, Notificador>();
			//services.AddScoped<IFornecedorService, FornecedorService>();
			//services.AddScoped<IProdutoService, ProdutoService>();

			return services;
		}
	}
}
