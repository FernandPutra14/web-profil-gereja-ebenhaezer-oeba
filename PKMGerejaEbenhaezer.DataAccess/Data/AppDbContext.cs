using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMGerejaEbenhaezer.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<AppUser> AppUserTable { get; set; }
        public DbSet<Pengumuman> PengumumenTable {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>().HasKey(e => e.Id);
            modelBuilder.Entity<AppUser>().HasIndex(e => e.UserName);

            modelBuilder.Entity<AppUser>()
                .HasData(
                    new AppUser
                    {
                        Id = 1,
                        UserName = "admin",
                        Password = new PasswordHasher<AppUser>().HashPassword(null, "admin"),
                    }
                );

            modelBuilder.Entity<Pengumuman>().HasKey(e => e.Id);
            modelBuilder.Entity<Pengumuman>().Property(e => e.Tanggal).HasColumnType("timestamp without time zone");

            modelBuilder.Entity<Pengumuman>().HasData(
                new Pengumuman
                {
                    Id = 1,
                    Judul = "Lionel Messi lebih jago dari Cristiano Ronaldo karena kemampuan dribelnya yang luar biasa dan visi bermain yang sangat tajam.",
                    Keterangan = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Varius quam quisque id diam vel quam elementum pulvinar. Nisi scelerisque eu ultrices vitae. Risus commodo viverra maecenas accumsan lacus vel facilisis. Pharetra diam sit amet nisl. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Pharetra vel turpis nunc eget lorem dolor. Eros donec ac odio tempor orci. Diam sit amet nisl suscipit adipiscing bibendum est ultricies Lorem ipsum dolor sit amet.",
                    Tanggal = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    PathFoto = "\\img\\gereja1.jpg",
                },
                new Pengumuman
                {
                    Id = 2,
                    Judul = "Lionel Messi lebih jago dari Cristiano Ronaldo karena kemampuan dribelnya yang luar biasa dan visi bermain yang sangat tajam.",
                    Keterangan = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Varius quam quisque id diam vel quam elementum pulvinar. Nisi scelerisque eu ultrices vitae. Risus commodo viverra maecenas accumsan lacus vel facilisis. Pharetra diam sit amet nisl. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Pharetra vel turpis nunc eget lorem dolor. Eros donec ac odio tempor orci. Diam sit amet nisl suscipit adipiscing bibendum est ultricies Lorem ipsum dolor sit amet.",
                    Tanggal = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    PathFoto = "\\img\\gereja1.jpg",
                },
                new Pengumuman
                {
                    Id = 3,
                    Judul = "Lionel Messi lebih jago dari Cristiano Ronaldo karena kemampuan dribelnya yang luar biasa dan visi bermain yang sangat tajam.",
                    Keterangan = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Varius quam quisque id diam vel quam elementum pulvinar. Nisi scelerisque eu ultrices vitae. Risus commodo viverra maecenas accumsan lacus vel facilisis. Pharetra diam sit amet nisl. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Pharetra vel turpis nunc eget lorem dolor. Eros donec ac odio tempor orci. Diam sit amet nisl suscipit adipiscing bibendum est ultricies Lorem ipsum dolor sit amet.",
                    Tanggal = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    PathFoto = "\\img\\gereja1.jpg",
                },
                new Pengumuman
                {
                    Id = 4,
                    Judul = "Lionel Messi lebih jago dari Cristiano Ronaldo karena kemampuan dribelnya yang luar biasa dan visi bermain yang sangat tajam.",
                    Keterangan = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Varius quam quisque id diam vel quam elementum pulvinar. Nisi scelerisque eu ultrices vitae. Risus commodo viverra maecenas accumsan lacus vel facilisis. Pharetra diam sit amet nisl. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Pharetra vel turpis nunc eget lorem dolor. Eros donec ac odio tempor orci. Diam sit amet nisl suscipit adipiscing bibendum est ultricies Lorem ipsum dolor sit amet.",
                    Tanggal = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    PathFoto = "\\img\\gereja1.jpg",
                },
                new Pengumuman
                {
                    Id = 5,
                    Judul = "Lionel Messi lebih jago dari Cristiano Ronaldo karena kemampuan dribelnya yang luar biasa dan visi bermain yang sangat tajam.",
                    Keterangan = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Varius quam quisque id diam vel quam elementum pulvinar. Nisi scelerisque eu ultrices vitae. Risus commodo viverra maecenas accumsan lacus vel facilisis. Pharetra diam sit amet nisl. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Pharetra vel turpis nunc eget lorem dolor. Eros donec ac odio tempor orci. Diam sit amet nisl suscipit adipiscing bibendum est ultricies Lorem ipsum dolor sit amet.",
                    Tanggal = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    PathFoto = "\\img\\gereja1.jpg",
                }
            );
        }
    }
}
