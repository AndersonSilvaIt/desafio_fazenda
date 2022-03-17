using Microsoft.EntityFrameworkCore;
using GA.Fazenda.Business.Models;

namespace GA.Fazenda.Data.Mappings
{
    public class FazendaMapping : IEntityTypeConfiguration<EntidadeFazenda>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<EntidadeFazenda> builder)
        {
            builder.ToTable("Fazenda");

            builder.HasKey(p => p.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.HasMany(f => f.Animais)
                   .WithOne(p => p.Fazenda)
                   .HasForeignKey(p => p.FazendaId);
        }
    }
}
