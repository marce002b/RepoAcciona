﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjApiProvinciasEjer.Models;

namespace ProjApiProvinciasEjer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210129154051_inicial")]
    partial class inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjApiProvinciasEjer.Models.Centroide", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("lat");

                    b.Property<double>("lon");

                    b.HasKey("Id");

                    b.ToTable("Centroide");
                });

            modelBuilder.Entity("ProjApiProvinciasEjer.Models.Municipio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre_Munic");

                    b.Property<int>("ProvinciaId");

                    b.HasKey("Id");

                    b.HasIndex("ProvinciaId");

                    b.ToTable("Municipios");
                });

            modelBuilder.Entity("ProjApiProvinciasEjer.Models.Pais", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Continente");

                    b.Property<string>("Habitantes");

                    b.Property<string>("Nombre_Pais")
                        .HasMaxLength(45);

                    b.Property<string>("Superficie");

                    b.HasKey("Id");

                    b.ToTable("Paises");
                });

            modelBuilder.Entity("ProjApiProvinciasEjer.Models.Provincia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Municipios_Cant");

                    b.Property<string>("Nombre_Pcia")
                        .HasMaxLength(45);

                    b.Property<int>("PaisId");

                    b.Property<int?>("centroideId");

                    b.HasKey("Id");

                    b.HasIndex("PaisId");

                    b.HasIndex("centroideId");

                    b.ToTable("Provincias");
                });

            modelBuilder.Entity("ProjApiProvinciasEjer.Models.Municipio", b =>
                {
                    b.HasOne("ProjApiProvinciasEjer.Models.Provincia", "Provincia")
                        .WithMany("Municipios")
                        .HasForeignKey("ProvinciaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjApiProvinciasEjer.Models.Provincia", b =>
                {
                    b.HasOne("ProjApiProvinciasEjer.Models.Pais", "Pais")
                        .WithMany("Provincias")
                        .HasForeignKey("PaisId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjApiProvinciasEjer.Models.Centroide", "centroide")
                        .WithMany()
                        .HasForeignKey("centroideId");
                });
#pragma warning restore 612, 618
        }
    }
}
