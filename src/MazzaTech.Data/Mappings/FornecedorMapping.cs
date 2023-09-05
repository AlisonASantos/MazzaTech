using MazzaTech.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MazzaTech.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<ClienteEntity>
    {
        public void Configure(EntityTypeBuilder<ClienteEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Email)
                .IsRequired()
                .HasColumnType("varchar(40)");

            // 1 : 1 => Cliente : Endereco
            builder.HasMany(f => f.Enderecos)
                .WithOne(e => e.Cliente)
                 .HasForeignKey(p => p.ClienteId);

            builder.ToTable("Tb_Clientes");
        }
    }
}