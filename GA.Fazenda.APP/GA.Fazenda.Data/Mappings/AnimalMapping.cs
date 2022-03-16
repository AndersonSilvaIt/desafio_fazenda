using GA.Fazenda.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GA.Fazenda.Data.Mappings
{
    public class AnimalMapping : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> builder)
        {
            builder.ToTable("Animal");

            builder.HasKey(p => p.Id);

            builder.Property(x => x.Tag)
                .IsRequired()
                .HasColumnType("varchar(15)");
        }
    }
}
