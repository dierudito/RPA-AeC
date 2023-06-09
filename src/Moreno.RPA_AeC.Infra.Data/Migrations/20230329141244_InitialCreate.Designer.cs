﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Moreno.RPA_AeC.Infra.Data.Context.Entity;

#nullable disable

namespace Moreno.RPA_AeC.Infra.Data.Migrations
{
    [DbContext(typeof(RPA_AeCDbContext))]
    [Migration("20230329141244_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Moreno.RPA_AeC.Domain.Entities.LogPesquisa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar");

                    b.Property<Guid>("PesquisaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PesquisaId");

                    b.ToTable("Pesquisas_Logs", (string)null);
                });

            modelBuilder.Entity("Moreno.RPA_AeC.Domain.Entities.Pesquisa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Termo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.ToTable("Pesquisas", (string)null);
                });

            modelBuilder.Entity("Moreno.RPA_AeC.Domain.Entities.ResultadoPesquisa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("AoMenosUmCapturado")
                        .HasColumnType("bit");

                    b.Property<string>("Area")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<string>("Autor")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<bool>("CapturadoTotalmente")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataPublicacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar");

                    b.Property<Guid>("PesquisaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.HasIndex("PesquisaId");

                    b.ToTable("Pesquisas_Resultados", (string)null);
                });

            modelBuilder.Entity("Moreno.RPA_AeC.Domain.Entities.LogPesquisa", b =>
                {
                    b.HasOne("Moreno.RPA_AeC.Domain.Entities.Pesquisa", "Pesquisa")
                        .WithMany("LogsPesquisa")
                        .HasForeignKey("PesquisaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pesquisa");
                });

            modelBuilder.Entity("Moreno.RPA_AeC.Domain.Entities.ResultadoPesquisa", b =>
                {
                    b.HasOne("Moreno.RPA_AeC.Domain.Entities.Pesquisa", "Pesquisa")
                        .WithMany("ResultadoPesquisas")
                        .HasForeignKey("PesquisaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pesquisa");
                });

            modelBuilder.Entity("Moreno.RPA_AeC.Domain.Entities.Pesquisa", b =>
                {
                    b.Navigation("LogsPesquisa");

                    b.Navigation("ResultadoPesquisas");
                });
#pragma warning restore 612, 618
        }
    }
}
