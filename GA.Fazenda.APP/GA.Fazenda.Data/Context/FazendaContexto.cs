using GA.Fazenda.Business.Interfaces;
using GA.Fazenda.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GA.Fazenda.Data.Context
{
    public class FazendaContexto : DbContext, IContext
    {
        public FazendaContexto(DbContextOptions options) : base(options)
        { }

		public DbSet<EntidadeFazenda> Fazendas { get; set; }
		public DbSet<Animal> Animais { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			foreach (var property in modelBuilder.Model.GetEntityTypes()
											.SelectMany(e => e.GetProperties()
											.Where(p => p.ClrType == typeof(string))))
			{
				property.SetColumnType("varchar(200)");
			}

			foreach (var property in modelBuilder.Model.GetEntityTypes()
								.SelectMany(e => e.GetProperties()
								.Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?))))
			{
				property.SetColumnType("decimal(18,2)");
			}

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(FazendaContexto).Assembly);

			// VERIFICAR se irá fazer isso, ou colocar uma trava ...
			// Aqui estou retirando o delete cascade das entidades
			foreach (var item in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
			{
				item.DeleteBehavior = DeleteBehavior.ClientSetNull;
			}

			base.OnModelCreating(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}

		public async Task<int> SaveChangesContext()
		{
			int retorno = await base.SaveChangesAsync();
		
			return retorno;
		}
    }
}
