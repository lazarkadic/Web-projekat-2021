﻿// <auto-generated />
using System;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BackEnd.Migrations
{
    [DbContext(typeof(KlinikaContext))]
    [Migration("20210926185719_V1")]
    partial class V1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BackEnd.Models.Lekar", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Ime");

                    b.Property<string>("Prezime")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Prezime");

                    b.Property<string>("StruncaSprema")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("StrucnaSprema");

                    b.HasKey("ID");

                    b.ToTable("Lekar");
                });

            modelBuilder.Entity("BackEnd.Models.Pacijent", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BrojSobeID")
                        .HasColumnType("int");

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Ime");

                    b.Property<int?>("IzabraniLekarID")
                        .HasColumnType("int");

                    b.Property<string>("JMBG")
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)")
                        .HasColumnName("JMBG");

                    b.Property<string>("Prezime")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Prezime");

                    b.HasKey("ID");

                    b.HasIndex("BrojSobeID");

                    b.HasIndex("IzabraniLekarID");

                    b.ToTable("Pacijent");
                });

            modelBuilder.Entity("BackEnd.Models.Soba", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrKreveta")
                        .HasColumnType("int")
                        .HasColumnName("BrKreveta");

                    b.Property<int>("BrSobe")
                        .HasColumnType("int")
                        .HasColumnName("BrSobe");

                    b.HasKey("ID");

                    b.ToTable("Soba");
                });

            modelBuilder.Entity("BackEnd.Models.Pacijent", b =>
                {
                    b.HasOne("BackEnd.Models.Soba", "BrojSobe")
                        .WithMany("ListaPacijenata")
                        .HasForeignKey("BrojSobeID");

                    b.HasOne("BackEnd.Models.Lekar", "IzabraniLekar")
                        .WithMany("DodeljeniPacijenati")
                        .HasForeignKey("IzabraniLekarID");

                    b.Navigation("BrojSobe");

                    b.Navigation("IzabraniLekar");
                });

            modelBuilder.Entity("BackEnd.Models.Lekar", b =>
                {
                    b.Navigation("DodeljeniPacijenati");
                });

            modelBuilder.Entity("BackEnd.Models.Soba", b =>
                {
                    b.Navigation("ListaPacijenata");
                });
#pragma warning restore 612, 618
        }
    }
}