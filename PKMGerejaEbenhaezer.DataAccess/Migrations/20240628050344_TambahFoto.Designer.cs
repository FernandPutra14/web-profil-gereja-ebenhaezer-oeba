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
    [Migration("20240628050344_TambahFoto")]
    partial class TambahFoto
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PKMGerejaEbenhaezer.Domain.Entity.AppUser", b =>
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
                            Password = "AQAAAAIAAYagAAAAEAYCZ4x7qXvaV2XU9eQOPEM/0lAl1DcogrL4Z0C2mCCleBylRcCGUh1ajRJxUJGSiQ==",
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("PKMGerejaEbenhaezer.Domain.Entity.Foto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("PathFoto")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PathFotoKompresi")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PembuatId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TanggalDiBuat")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("TanggalDiUbah")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("PembuatId");

                    b.ToTable("FotoTable");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PathFoto = "\\img\\pengumuman\\natall.jpg",
                            PathFotoKompresi = "\\img\\pengumuman\\natall.jpg",
                            TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            PathFoto = "\\img\\pengumuman\\rapatt.jpg",
                            PathFotoKompresi = "\\img\\pengumuman\\rapatt.jpg",
                            TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            PathFoto = "\\img\\pengumuman\\tripp.jpg",
                            PathFotoKompresi = "\\img\\pengumuman\\tripp.jpg",
                            TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            PathFoto = "\\img\\pengumuman\\donasii.jpg",
                            PathFotoKompresi = "\\img\\pengumuman\\donasii.jpg",
                            TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            PathFoto = "\\img\\pengumuman\\pelayanann.jpg",
                            PathFotoKompresi = "\\img\\pengumuman\\pelayanann.jpg",
                            TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 6,
                            PathFoto = "/img/generaluser.png",
                            PathFotoKompresi = "/img/generaluser.png",
                            TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("PKMGerejaEbenhaezer.Domain.Entity.Pengumuman", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("FotoId")
                        .HasColumnType("integer");

                    b.Property<string>("Isi")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Judul")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PembuatId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TanggalDiBuat")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("TanggalDiUbah")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("FotoId");

                    b.HasIndex("PembuatId");

                    b.ToTable("PengumumanTable");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Isi = "Kami mengundang seluruh jemaat untuk hadir dalam Ibadah Natal yang akan diadakan pada hari Minggu, 25 Desember 2024, pukul 10.00 WIB di Gereja. Mari kita rayakan kelahiran Yesus Kristus dengan sukacita dan kebersamaan. Setelah ibadah, akan diadakan acara ramah tamah di aula gereja.",
                            Judul = "Ibadah Natal Bersama",
                            TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Isi = "Rapat Anggota Jemaat Tahunan akan dilaksanakan pada hari Sabtu, 27 Juni 2024, pukul 14.00 WIB di aula gereja. Kami mengajak seluruh jemaat untuk hadir dan berpartisipasi dalam membahas laporan tahunan dan rencana kegiatan gereja untuk tahun 2024.",
                            Judul = "Rapat Anggota Jemaat Tahunan",
                            TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            Isi = "Kami mengundang para pemuda gereja untuk mengikuti Retret Pemuda yang akan diadakan pada tanggal 15-17 Juli 2024 di Wisma Retreat Agape. Tema retret kali ini adalah \"Membangun Iman yang Kuat di Era Digital\". Pendaftaran dapat dilakukan melalui sekretariat gereja hingga 1 Juli 2024.",
                            Judul = "Retret Pemuda Gereja",
                            TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            Isi = "Gereja kita akan mengadakan penggalangan dana untuk renovasi bangunan gereja yang direncanakan mulai bulan Juni 2024. Kami mengajak seluruh jemaat untuk berpartisipasi dalam acara ini pada hari Minggu, 5 Juli 2024, pukul 09.00 WIB setelah kebaktian. Donasi dapat diberikan langsung atau melalui rekening gereja.",
                            Judul = "Penggalangan Dana untuk Renovasi Gereja",
                            TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            Isi = "Dalam rangka menyambut Natal, gereja akan mengadakan kegiatan pelayanan sosial ke Panti Asuhan Kasih Ibu pada hari Sabtu, 23 Desember 2024. Kami mengundang jemaat untuk ikut serta dalam kegiatan ini dengan menyumbangkan pakaian layak pakai, mainan, dan sembako. Barang-barang sumbangan dapat dikumpulkan di kantor gereja hingga 21 Desember 2024.",
                            Judul = "Pelayanan Sosial Natal",
                            TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("PKMGerejaEbenhaezer.Domain.Entity.Rayon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("FotoKetuaId")
                        .HasColumnType("integer");

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

                    b.HasIndex("FotoKetuaId");

                    b.ToTable("RayonTable");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            JumlahAnak = 10,
                            JumlahDewasa = 15,
                            JumlahLakiLaki = 25,
                            JumlahLansia = 15,
                            JumlahPemuda = 10,
                            JumlahPerempuan = 35,
                            JumlahRemaja = 10,
                            KetuaRayon = "Ketua Rayon I",
                            Nama = "Rayon I"
                        },
                        new
                        {
                            Id = 2,
                            JumlahAnak = 10,
                            JumlahDewasa = 15,
                            JumlahLakiLaki = 25,
                            JumlahLansia = 15,
                            JumlahPemuda = 10,
                            JumlahPerempuan = 35,
                            JumlahRemaja = 10,
                            KetuaRayon = "Ketua Rayon II",
                            Nama = "Rayon II"
                        },
                        new
                        {
                            Id = 3,
                            JumlahAnak = 10,
                            JumlahDewasa = 15,
                            JumlahLakiLaki = 25,
                            JumlahLansia = 15,
                            JumlahPemuda = 10,
                            JumlahPerempuan = 35,
                            JumlahRemaja = 10,
                            KetuaRayon = "Ketua Rayon III",
                            Nama = "Rayon III"
                        });
                });

            modelBuilder.Entity("PKMGerejaEbenhaezer.Domain.Entity.Foto", b =>
                {
                    b.HasOne("PKMGerejaEbenhaezer.Domain.Entity.AppUser", "Pembuat")
                        .WithMany()
                        .HasForeignKey("PembuatId");

                    b.Navigation("Pembuat");
                });

            modelBuilder.Entity("PKMGerejaEbenhaezer.Domain.Entity.Pengumuman", b =>
                {
                    b.HasOne("PKMGerejaEbenhaezer.Domain.Entity.Foto", "Foto")
                        .WithMany()
                        .HasForeignKey("FotoId");

                    b.HasOne("PKMGerejaEbenhaezer.Domain.Entity.AppUser", "Pembuat")
                        .WithMany()
                        .HasForeignKey("PembuatId");

                    b.Navigation("Foto");

                    b.Navigation("Pembuat");
                });

            modelBuilder.Entity("PKMGerejaEbenhaezer.Domain.Entity.Rayon", b =>
                {
                    b.HasOne("PKMGerejaEbenhaezer.Domain.Entity.Foto", "FotoKetua")
                        .WithMany()
                        .HasForeignKey("FotoKetuaId");

                    b.Navigation("FotoKetua");
                });
#pragma warning restore 612, 618
        }
    }
}
