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

                    b.Property<int>("Peso")
                        .HasColumnType("int");

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

            modelBuilder.Entity("PetFelizApi.Models.DogWalker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AceitaCartao")
                        .HasColumnType("bit");

                    b.Property<decimal>("AvaliacaoMedia")
                        .HasColumnType("decimal(1,1)");

                    b.Property<string>("Celular")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Disponivel")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EndDogWalkerId")
                        .HasColumnType("int");

                    b.Property<byte[]>("FotoPerfil")
                        .HasColumnType("varbinary(max)");

                    b.Property<double>("LatitudeDW")
                        .HasColumnType("float");

                    b.Property<double>("LongitudeDW")
                        .HasColumnType("float");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Preferencias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sobre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoConta")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorServico")
                        .HasColumnType("decimal(4,2)");

                    b.HasKey("Id");

                    b.HasIndex("EndDogWalkerId");

                    b.ToTable("DogWalker");
                });

            modelBuilder.Entity("PetFelizApi.Models.EnderecoDogWalker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bairro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cidade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<string>("NmrEndereco")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rua")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EnderecoDogwalker");
                });

            modelBuilder.Entity("PetFelizApi.Models.EnderecoProprietario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bairro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cidade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<string>("NmrEndereco")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rua")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EnderProprietario");
                });

            modelBuilder.Entity("PetFelizApi.Models.Proprietario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("FotoPerfil")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("TipoConta")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Proprietario");
                });

            modelBuilder.Entity("PetFelizApi.Models.SolicitacaoServico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataSolicitacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<DateTime>("HoraInicio")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("HoraSolicitacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("HoraTermino")
                        .HasColumnType("datetime2");

                    b.Property<double>("LatitudeProp")
                        .HasColumnType("float");

                    b.Property<double>("LongitudeProp")
                        .HasColumnType("float");

                    b.Property<decimal>("VlTotal")
                        .HasColumnType("decimal(4,2)");

                    b.HasKey("Id");

                    b.ToTable("SolicitacaoServico");
                });

            modelBuilder.Entity("PetFelizApi.Models.Cao", b =>
                {
                    b.HasOne("PetFelizApi.Models.Proprietario", "Proprietario")
                        .WithMany("Caes")
                        .HasForeignKey("ProprietarioId");

                    b.Navigation("Proprietario");
                });

            modelBuilder.Entity("PetFelizApi.Models.DogWalker", b =>
                {
                    b.HasOne("PetFelizApi.Models.EnderecoDogWalker", "EndDogWalker")
                        .WithMany()
                        .HasForeignKey("EndDogWalkerId");

                    b.Navigation("EndDogWalker");
                });

            modelBuilder.Entity("PetFelizApi.Models.Proprietario", b =>
                {
                    b.Navigation("Caes");
                });
#pragma warning restore 612, 618
        }
    }
}
