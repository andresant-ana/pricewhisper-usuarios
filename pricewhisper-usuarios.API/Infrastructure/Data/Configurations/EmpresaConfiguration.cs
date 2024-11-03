using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.HasKey(e => e.EmpresaId);

            builder.Property(e => e.CNPJ)
                .IsRequired()
                .HasMaxLength(14);

            builder.Property(e => e.RazaoSocial)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.NomeFantasia)
                .HasMaxLength(100);

            builder.HasMany(e => e.Usuarios)
                .WithOne(u => u.Empresa)
                .HasForeignKey(u => u.EmpresaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}