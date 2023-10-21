using Microsoft.EntityFrameworkCore;
using SA.PTM.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.PTM.DAL.Context
{
    public class PersonelTakipDbContext:DbContext
    {
        public DbSet<Personel> Personeller { get; set; }
        public DbSet<OzlukBilgisi> OzlukBilgileri { get; set; }
        public DbSet<MaasBilgisi> MaasBilgileri { get; set; }
        public DbSet<IzinBilgisi> IzinBilgileri { get; set; }
        public DbSet<EgitimBilgisi> EgitimBilgileri { get; set; }
        public DbSet<Bordro> Bordrolar { get; set; }
        public DbSet<Avans> Avanslar { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<Personel>()
                .HasMany(p => p.OzlukBilgileri)
                .WithOne(o => o.Personel)
                .HasForeignKey(o => o.PersonelId);

            modelBuilder.Entity<Personel>()
                .HasMany(p => p.MaasBilgileri)
                .WithOne(m => m.Personel)
                .HasForeignKey(m => m.PersonelId);

            modelBuilder.Entity<Personel>()
                .HasMany(p => p.IzinBilgileri)
                .WithOne(i => i.Personel)
                .HasForeignKey(i => i.PersonelId);

            modelBuilder.Entity<Personel>()
                .HasMany(p => p.EgitimBilgileri)
                .WithOne(e => e.Personel)
                .HasForeignKey(e => e.PersonelId);

            modelBuilder.Entity<Personel>()
                .HasMany(p => p.Bordrolar)
                .WithOne(b => b.Personel)
                .HasForeignKey(b => b.PersonelId);

            modelBuilder.Entity<Personel>()
                .HasMany(p => p.Avanslar)
                .WithOne(a => a.Personel)
                .HasForeignKey(a => a.PersonelId);
            modelBuilder.Entity<Personel>()
           .HasIndex(p => p.TcNo)
           .IsUnique();


        }
    }
}
