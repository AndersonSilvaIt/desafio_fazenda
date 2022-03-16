using GA.Fazenda.Business.Interfaces;
using GA.Fazenda.Business.Interfaces.Repositorios;
using GA.Fazenda.Business.Interfaces.Servicos;
using GA.Fazenda.Business.Notificacoes;
using GA.Fazenda.Business.Servicos;
using GA.Fazenda.Data.Context;
using GA.Fazenda.Data.Repository;
using GA.Fazenda.Data.UOW;
using Microsoft.Extensions.DependencyInjection;

namespace GA.Fazenda.APP.Configuration
{
    public static class DependencyInjectionConfig
    {
		public static IServiceCollection ResolveDependencies(this IServiceCollection services)
		{
			services.AddScoped<FazendaContexto>();
			services.AddScoped<IFazendaRepository, FazendaRepository>();
			services.AddScoped<IAnimalRepository, AnimalRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			
			services.AddScoped<INotificador, Notificador>();
			services.AddScoped<IFazendaService, FazendaService>();
			services.AddScoped<IAnimalService, AnimalService>();

			return services;
		}
	}
}
