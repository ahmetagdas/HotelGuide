﻿// <auto-generated />
using System;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Entity.Migrations
{
    [DbContext(typeof(OtelDbContext))]
    [Migration("20230729211547_InitialCreateOtelDb")]
    partial class InitialCreateOtelDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Data.Entity.IletisimBilgisi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BilgiIcerigi")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BilgiTipi")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OtelId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OtelId");

                    b.ToTable("IletisimBilgileri");
                });

            modelBuilder.Entity("Data.Entity.Otel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Aciklama")
                        .HasColumnType("text");

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Adres")
                        .HasColumnType("text");

                    b.Property<string>("Eposta")
                        .HasColumnType("text");

                    b.Property<string>("OdaSayisi")
                        .HasColumnType("text");

                    b.Property<string>("Sehir")
                        .HasColumnType("text");

                    b.Property<string>("Telefon")
                        .HasColumnType("text");

                    b.Property<string>("Ulke")
                        .HasColumnType("text");

                    b.Property<string>("WebSitesi")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Oteller");
                });

            modelBuilder.Entity("Data.Entity.OtelYetkilisi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirmaUnvan")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OtelId")
                        .HasColumnType("integer");

                    b.Property<string>("Soyad")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OtelId");

                    b.ToTable("OtelYetkilileri");
                });

            modelBuilder.Entity("Data.Entity.Rapor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Durum")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OtelId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TalepEdildigiTarih")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OtelId");

                    b.ToTable("Raporlar");
                });

            modelBuilder.Entity("Data.Entity.IletisimBilgisi", b =>
                {
                    b.HasOne("Data.Entity.Otel", "Otel")
                        .WithMany("IletisimBilgileri")
                        .HasForeignKey("OtelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Otel");
                });

            modelBuilder.Entity("Data.Entity.OtelYetkilisi", b =>
                {
                    b.HasOne("Data.Entity.Otel", "Otel")
                        .WithMany("Yetkililer")
                        .HasForeignKey("OtelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Otel");
                });

            modelBuilder.Entity("Data.Entity.Rapor", b =>
                {
                    b.HasOne("Data.Entity.Otel", "Otel")
                        .WithMany("Raporlar")
                        .HasForeignKey("OtelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Otel");
                });

            modelBuilder.Entity("Data.Entity.Otel", b =>
                {
                    b.Navigation("IletisimBilgileri");

                    b.Navigation("Raporlar");

                    b.Navigation("Yetkililer");
                });
#pragma warning restore 612, 618
        }
    }
}
