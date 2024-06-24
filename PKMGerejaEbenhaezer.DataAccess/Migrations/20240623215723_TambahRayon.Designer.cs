﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PKMGerejaEbenhaezer.DataAccess.Data;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240623215723_TambahRayon")]
    partial class TambahRayon
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PKMGerejaEbenhaezer.Domain.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserName");

                    b.ToTable("AppUserTable");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Password = "AQAAAAIAAYagAAAAEGNQJmKL5RKdArT9DWkC5DpAm31sVmFy8waliSZW3d8W3AEAW25ZCoRdm0HXtG6/nw==",
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("PKMGerejaEbenhaezer.Domain.Pengumuman", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Judul")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Keterangan")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PathFoto")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Tanggal")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("PengumumanTable");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Judul = "Lionel Messi lebih jago dari Cristiano Ronaldo karena kemampuan dribelnya yang luar biasa dan visi bermain yang sangat tajam.",
                            Keterangan = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Varius quam quisque id diam vel quam elementum pulvinar. Nisi scelerisque eu ultrices vitae. Risus commodo viverra maecenas accumsan lacus vel facilisis. Pharetra diam sit amet nisl. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Pharetra vel turpis nunc eget lorem dolor. Eros donec ac odio tempor orci. Diam sit amet nisl suscipit adipiscing bibendum est ultricies Lorem ipsum dolor sit amet.",
                            PathFoto = "\\img\\gereja1.jpg",
                            Tanggal = new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Judul = "Lionel Messi lebih jago dari Cristiano Ronaldo karena kemampuan dribelnya yang luar biasa dan visi bermain yang sangat tajam.",
                            Keterangan = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Varius quam quisque id diam vel quam elementum pulvinar. Nisi scelerisque eu ultrices vitae. Risus commodo viverra maecenas accumsan lacus vel facilisis. Pharetra diam sit amet nisl. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Pharetra vel turpis nunc eget lorem dolor. Eros donec ac odio tempor orci. Diam sit amet nisl suscipit adipiscing bibendum est ultricies Lorem ipsum dolor sit amet.",
                            PathFoto = "\\img\\gereja1.jpg",
                            Tanggal = new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            Judul = "Lionel Messi lebih jago dari Cristiano Ronaldo karena kemampuan dribelnya yang luar biasa dan visi bermain yang sangat tajam.",
                            Keterangan = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Varius quam quisque id diam vel quam elementum pulvinar. Nisi scelerisque eu ultrices vitae. Risus commodo viverra maecenas accumsan lacus vel facilisis. Pharetra diam sit amet nisl. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Pharetra vel turpis nunc eget lorem dolor. Eros donec ac odio tempor orci. Diam sit amet nisl suscipit adipiscing bibendum est ultricies Lorem ipsum dolor sit amet.",
                            PathFoto = "\\img\\gereja1.jpg",
                            Tanggal = new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            Judul = "Lionel Messi lebih jago dari Cristiano Ronaldo karena kemampuan dribelnya yang luar biasa dan visi bermain yang sangat tajam.",
                            Keterangan = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Varius quam quisque id diam vel quam elementum pulvinar. Nisi scelerisque eu ultrices vitae. Risus commodo viverra maecenas accumsan lacus vel facilisis. Pharetra diam sit amet nisl. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Pharetra vel turpis nunc eget lorem dolor. Eros donec ac odio tempor orci. Diam sit amet nisl suscipit adipiscing bibendum est ultricies Lorem ipsum dolor sit amet.",
                            PathFoto = "\\img\\gereja1.jpg",
                            Tanggal = new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            Judul = "Lionel Messi lebih jago dari Cristiano Ronaldo karena kemampuan dribelnya yang luar biasa dan visi bermain yang sangat tajam.",
                            Keterangan = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Varius quam quisque id diam vel quam elementum pulvinar. Nisi scelerisque eu ultrices vitae. Risus commodo viverra maecenas accumsan lacus vel facilisis. Pharetra diam sit amet nisl. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Pharetra vel turpis nunc eget lorem dolor. Eros donec ac odio tempor orci. Diam sit amet nisl suscipit adipiscing bibendum est ultricies Lorem ipsum dolor sit amet.",
                            PathFoto = "\\img\\gereja1.jpg",
                            Tanggal = new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("PKMGerejaEbenhaezer.Domain.Rayon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("FotoKetua")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<int>("JumlahAnak")
                        .HasColumnType("integer");

                    b.Property<int>("JumlahDewasa")
                        .HasColumnType("integer");

                    b.Property<int>("JumlahLakiLaki")
                        .HasColumnType("integer");

                    b.Property<int>("JumlahLansia")
                        .HasColumnType("integer");

                    b.Property<int>("JumlahPemuda")
                        .HasColumnType("integer");

                    b.Property<int>("JumlahPerempuan")
                        .HasColumnType("integer");

                    b.Property<int>("JumlahRemaja")
                        .HasColumnType("integer");

                    b.Property<string>("KetuaRayon")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nama")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("RayonTable");
                });
#pragma warning restore 612, 618
        }
    }
}
