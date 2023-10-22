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
        public DbSet<Avans> Avanslar { get; set; }
        public DbSet<Bordro> Bordrolar { get; set; }
        public DbSet<EgitimBilgisi> EgitimBilgileri { get; set; }
        public DbSet<IzinBilgisi> IzinBilgileri { get; set; }
        public DbSet<MaasBilgisi> MaasBilgileri { get; set; }
        public DbSet<OzlukBilgisi> OzlukBilgileri { get; set; }
        public DbSet<Yonetici> Yoneticiler { get; set; }
        public DbSet<Okul> Okullar { get; set; }
        public DbSet<Sertifika> Sertifikalar { get; set; }
        public DbSet<SirketIciEgitim> SirketIciEgitimler { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Personel ile Avans arasındaki ilişki (1-N)
            modelBuilder.Entity<Personel>()
                .HasMany(p => p.Avanslar)
                .WithOne(a => a.Personel)
                .HasForeignKey(a => a.PersonelId);

            // Personel ile Bordro arasındaki ilişki (1-N)
            modelBuilder.Entity<Personel>()
                .HasMany(p => p.Bordrolar)
                .WithOne(b => b.Personel)
                .HasForeignKey(b => b.PersonelId);

            // Personel ile EgitimBilgisi arasındaki ilişki (1-1)
            modelBuilder.Entity<Personel>()
                .HasOne(p => p.EgitimBilgisi)
                .WithOne(e => e.Personel)
                .HasForeignKey<EgitimBilgisi>(e => e.PersonelId);

            // Personel ile IzinBilgisi arasındaki ilişki (1-N)
            modelBuilder.Entity<Personel>()
                .HasMany(p => p.IzinBilgileri)
                .WithOne(i => i.Personel)
                .HasForeignKey(i => i.PersonelId);

            // Personel ile MaasBilgisi arasındaki ilişki (1-1)
            modelBuilder.Entity<Personel>()
                .HasOne(p => p.MaasBilgisi)
                .WithOne(m => m.Personel)
                .HasForeignKey<MaasBilgisi>(m => m.PersonelId);

            modelBuilder.Entity<Yonetici>()
                .HasMany(y => y.Personeller)
                .WithOne(p => p.Yonetici)
                .HasForeignKey(p => p.YoneticiId);

            // Personel ile Yonetici arasındaki ilişki (n-1)
            modelBuilder.Entity<Personel>()
                .HasOne(p => p.Yonetici)
                .WithMany(y => y.Personeller)
                .HasForeignKey(p => p.YoneticiId);

            // EgitimBilgisi ile Okul arasındaki ilişki (1-N)
            modelBuilder.Entity<EgitimBilgisi>()
                .HasMany(e => e.Okullar)
                .WithOne(o => o.EgitimBilgisi)
                .HasForeignKey(o => o.EgitimBilgisiId);

            // EgitimBilgisi ile Sertifika arasındaki ilişki (1-N)
            modelBuilder.Entity<EgitimBilgisi>()
                .HasMany(e => e.Sertifikalar)
                .WithOne(s => s.EgitimBilgisi)
                .HasForeignKey(s => s.EgitimBilgisiId);

            // EgitimBilgisi ile SirketIciEgitim arasındaki ilişki (1-N)
            modelBuilder.Entity<EgitimBilgisi>()
                .HasMany(e => e.SirketIciEgitimler)
                .WithOne(s => s.EgitimBilgisi)
                .HasForeignKey(s => s.EgitimBilgisiId);
            // Personel ile OzlukBilgisi arasındaki ilişki (1-1)
            modelBuilder.Entity<Personel>()
                .HasOne(p => p.OzlukBilgisi)
                .WithOne(o => o.Personel)
                .HasForeignKey<OzlukBilgisi>(o => o.PersonelId);
            // Diğer ilişkileri ve özel ayarları da burada tanımlayabilirsiniz.
            modelBuilder.Entity<Personel>()
                .HasIndex(p => p.TcNo)
                .IsUnique();



        }
    }
}
