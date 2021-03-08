﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PetFelizApi.Data;

namespace PetFelizApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210307205500_AdcValorServicoInfoDogW")]
    partial class AdcValorServicoInfoDogW
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                        .HasColumnType("decimal(3,2)");

                    b.HasKey("Id");

                    b.HasIndex("DogWalkerId")
                        .IsUnique();

                    b.ToTable("ServicoDogWalker");
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

            modelBuilder.Entity("PetFelizApi.Models.Cao", b =>
                {
                    b.HasOne("PetFelizApi.Models.Usuario", "Proprietario")
                        .WithMany("Caes")
                        .HasForeignKey("ProprietarioId");

                    b.Navigation("Proprietario");
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

            modelBuilder.Entity("PetFelizApi.Models.Usuario", b =>
                {
                    b.Navigation("Caes");

                    b.Navigation("ServicoDogWalker");
                });
#pragma warning restore 612, 618
        }
    }
}
