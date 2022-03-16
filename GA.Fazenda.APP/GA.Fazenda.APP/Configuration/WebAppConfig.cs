using GA.Fazenda.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;

namespace GA.Fazenda.APP.Configuration
{
    public static class WebAppConfig
	{
		public static void AddMvcConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<FazendaContexto>(options =>
					options.UseSqlite(configuration.GetConnectionString("DefaultConn")));

			services.AddAutoMapper(typeof(Startup));

			services.AddControllersWithViews();
		}

		public static void UseMvcConfiguration(this IApplicationBuilder app, IHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHsts();

			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.UseRouting();

			

			var supportedCultures = new[] { new CultureInfo("pt-BR") };
			app.UseRequestLocalization(new RequestLocalizationOptions
			{
				DefaultRequestCulture = new RequestCulture("pt-BR"),
				SupportedCultures = supportedCultures,
				SupportedUICultures = supportedCultures
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
