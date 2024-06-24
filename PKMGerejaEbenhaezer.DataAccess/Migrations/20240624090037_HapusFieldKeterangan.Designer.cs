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
    [Migration("20240624090037_HapusFieldKeterangan")]
    partial class HapusFieldKeterangan
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
                            Password = "AQAAAAIAAYagAAAAEJBpJCCz0zrrlyTixy9omix+q7be0l2Lchp8gXJkk4iBxL+6L8+hUVnpSfA15iYgSw==",
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

                    b.Property<string>("PathFoto")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Tanggal")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("TanggalDiubah")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("PengumumanTable");
                });
#pragma warning restore 612, 618
        }
    }
}
