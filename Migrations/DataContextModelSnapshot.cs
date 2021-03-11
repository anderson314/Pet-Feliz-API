﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PetFelizApi.Data;

namespace PetFelizApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PetFelizApi.Models.Cao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Idade")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Peso")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Porte")
                        .HasColumnType("int");

                    b.Property<int?>("ProprietarioId")
                        .HasColumnType("int");

                    b.Property<string>("Raca")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProprietarioId");

                    b.ToTable("Cao");
                });

            modelBuilder.Entity("PetFelizApi.Models.CaoServico", b =>
                {
                    b.Property<int>("CaoId")
                        .HasColumnType("int");

                    b.Property<int>("ServicoId")
                        .HasColumnType("int");

                    b.HasKey("CaoId", "ServicoId");

                    b.HasIndex("ServicoId");

                    b.ToTable("CaesServico");
                });

            modelBuilder.Entity("PetFelizApi.Models.InformacoesServicoDogWalker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AceitaCartao")
                        .HasColumnType("bit");

                    b.Property<decimal>("AvaliacaoMedia")
                        .HasColumnType("decimal(1,1)");

                    b.Property<int>("DogWalkerId")
                        .HasColumnType("int");

                    b.Property<string>("Preferencias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sobre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ValorServico")
                        .HasColumnType("decimal(4,2)");

                    b.HasKey("Id");

                    b.HasIndex("DogWalkerId")
                        .IsUnique();

                    b.ToTable("ServicoDogWalker");
                });

            modelBuilder.Entity("PetFelizApi.Models.Servico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DataSolicitacao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<string>("HoraInicio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoraSolicitacao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoraTermino")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("LatitudeProp")
                        .HasColumnType("float");

                    b.Property<double>("LongitudeProp")
                        .HasColumnType("float");

                    b.Property<int>("ProprietarioId")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(5,2)");

                    b.HasKey("Id");

                    b.ToTable("Servico");
                });

            modelBuilder.Entity("PetFelizApi.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Disponivel")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("FotoPerfil")
                        .HasColumnType("varbinary(max)");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoConta")
                        .HasColumnType("int");

                    b.Property<string>("WhatsApp")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("PetFelizApi.Models.UsuariosServico", b =>
                {
                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int>("ServicoId")
                        .HasColumnType("int");

                    b.HasKey("UsuarioId", "ServicoId");

                    b.HasIndex("ServicoId");

                    b.ToTable("UsuariosServico");
                });

            modelBuilder.Entity("PetFelizApi.Models.Cao", b =>
                {
                    b.HasOne("PetFelizApi.Models.Usuario", "Proprietario")
                        .WithMany("Caes")
                        .HasForeignKey("ProprietarioId");

                    b.Navigation("Proprietario");
                });

            modelBuilder.Entity("PetFelizApi.Models.CaoServico", b =>
                {
                    b.HasOne("PetFelizApi.Models.Cao", "Cao")
                        .WithMany()
                        .HasForeignKey("CaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetFelizApi.Models.Servico", "Servico")
                        .WithMany("Caes")
                        .HasForeignKey("ServicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cao");

                    b.Navigation("Servico");
                });

            modelBuilder.Entity("PetFelizApi.Models.InformacoesServicoDogWalker", b =>
                {
                    b.HasOne("PetFelizApi.Models.Usuario", "DogWalker")
                        .WithOne("ServicoDogWalker")
                        .HasForeignKey("PetFelizApi.Models.InformacoesServicoDogWalker", "DogWalkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DogWalker");
                });

            modelBuilder.Entity("PetFelizApi.Models.UsuariosServico", b =>
                {
                    b.HasOne("PetFelizApi.Models.Servico", "Servico")
                        .WithMany("Usuarios")
                        .HasForeignKey("ServicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetFelizApi.Models.Usuario", "Usuario")
                        .WithMany("Servicos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Servico");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("PetFelizApi.Models.Servico", b =>
                {
                    b.Navigation("Caes");

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("PetFelizApi.Models.Usuario", b =>
                {
                    b.Navigation("Caes");

                    b.Navigation("ServicoDogWalker");

                    b.Navigation("Servicos");
                });
#pragma warning restore 612, 618
        }
    }
}
