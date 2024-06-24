using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace RVASIspit.Models
{
    public class CodeFirstBaza : DbContext
    {
        public DbSet<Proizvod> Proizvodi { get; set; }
        public DbSet<Klijent> Klijenti { get; set; }
        public DbSet<Zaposleni> Zaposleni { get; set; }
        public DbSet<VrstaProizvoda> VrsteProizvoda { get; set; }
        public DbSet<GrupaProizvoda> GrupeProizvoda { get; set; }
        public DbSet<Racun> Racuni { get; set; }
        public DbSet<StavkaRacuna> StavkeRacuna { get; set; }
        public DbSet<Sastojak> Sastojci { get; set; }
        public DbSet<ProizvodSastojak> SastojciProizvoda {get; set;}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Otklanjanje 's' iz imena tabela
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Kompozitni kljuc za tabelu ProizvodSastojak (many-to-many relacija između Proizvod i Sastojak)
            modelBuilder.Entity<ProizvodSastojak>()
                .HasKey(ps => new { ps.ProizvodID, ps.SastojakID });

            // Definisanje veze za tabelu ProizvodSastojak - jedan Proizvod može imati više ProizvodSastojak (many-to-many relacija)
            modelBuilder.Entity<ProizvodSastojak>()
                .HasRequired(ps => ps.Proizvod)
                .WithMany(p => p.ProizvodSastojci)
                .HasForeignKey(ps => ps.ProizvodID);

            // Definisanje veze za tabelu ProizvodSastojak - jedan Sastojak može imati više ProizvodSastojak (many-to-many relacija)
            modelBuilder.Entity<ProizvodSastojak>()
                .HasRequired(ps => ps.Sastojak)
                .WithMany(s => s.ProizvodSastojci)
                .HasForeignKey(ps => ps.SastojakID);

            // Kompozitni ključ za tabelu StavkaRacuna (many-to-many relacija između Racun i Proizvod)
            modelBuilder.Entity<StavkaRacuna>()
                .HasKey(sr => new { sr.RacunID, sr.ProizvodID });

            // Definisanje veze za tabelu StavkaRacuna - jedan Racun može imati više StavkaRacuna (many-to-many relacija)
            modelBuilder.Entity<StavkaRacuna>()
                .HasRequired(sr => sr.Racun)
                .WithMany(r => r.StavkeRacuna)
                .HasForeignKey(sr => sr.RacunID);

            // Definisanje veze za tabelu StavkaRacuna - jedan Proizvod može imati više StavkaRacuna (many-to-many relacija)
            modelBuilder.Entity<StavkaRacuna>()
                .HasRequired(sr => sr.Proizvod)
                .WithMany(p => p.StavkeRacuna)
                .HasForeignKey(sr => sr.ProizvodID);

            // Jedan na više relacija - jedan Klijent može imati više Racun entiteta
            modelBuilder.Entity<Racun>()
                .HasRequired(r => r.Klijent)
                .WithMany(k => k.Racuni)
                .HasForeignKey(r => r.KlijentID);

            // Jedan na više relacija - jedan Zaposleni može imati više Racun entiteta
            modelBuilder.Entity<Racun>()
                .HasRequired(r => r.Zaposleni)
                .WithMany(z => z.Racuni)
                .HasForeignKey(r => r.ZaposleniID);

            // Jedan na više relacija - jedna GrupaProizvoda može imati više Proizvod entiteta
            modelBuilder.Entity<Proizvod>()
                .HasOptional(p => p.GrupaProizvoda)
                .WithMany(g => g.Proizvodi)
                .HasForeignKey(p => p.GrupaProizvodaID);

            // Jedan na više relacija - jedna VrstaProizvoda može imati više Proizvod entiteta
            modelBuilder.Entity<Proizvod>()
                .HasOptional(p => p.VrstaProizvoda)
                .WithMany(v => v.Proizvodi)
                .HasForeignKey(p => p.VrstaProizvodaID);

            base.OnModelCreating(modelBuilder);
        }
    }
}