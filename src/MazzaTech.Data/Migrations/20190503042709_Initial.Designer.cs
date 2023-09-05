﻿// <auto-generated />
using MazzaTech.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DevIO.Data.Migrations
{
    [DbContext(typeof(MeuDbContext))]
    partial class MeuDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MazzaTech.Business.Models.Endereco", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Bairro")
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                b.Property<string>("Cep")
                    .IsRequired()
                    .HasColumnType("varchar(8)");

                b.Property<string>("Cidade")
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                b.Property<string>("Complemento")
                    .HasColumnType("varchar(250)");

                b.Property<string>("Estado")
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                b.Property<Guid>("FornecedorId");

                b.Property<string>("Logradouro")
                    .IsRequired()
                    .HasColumnType("varchar(200)");

                b.Property<string>("Numero")
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                b.HasKey("Id");

                b.HasIndex("ClienteId")
                    .IsUnique();

                b.ToTable("Tb_Enderecos");
            });

            modelBuilder.Entity("MazzaTech.Business.Models.Cliente", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<bool>("Ativo");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("varchar(40)");

                b.Property<string>("Nome")
                    .IsRequired()
                    .HasColumnType("varchar(200)");

                b.HasKey("Id");

                b.ToTable("Tb_Clientes");
            });

            modelBuilder.Entity("MazzaTech.Business.Models.Endereco", b =>
            {
                b.HasOne("MazzaTech.Business.Models.Fornecedor", "Clientes")
                    .WithOne("Enderecos")
                    .HasForeignKey("MazzaTech.Business.Models.Endereco", "ClienteId");
            });
        }
    }
}