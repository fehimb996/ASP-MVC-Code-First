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
            // Otklanjanje 's' 
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Kompozitni kljuc 
            modelBuilder.Entity<ProizvodSastojak>()
                .HasKey(ps => new { ps.ProizvodID, ps.SastojakID });

            modelBuilder.Entity<ProizvodSastojak>()
                .HasRequired(ps => ps.Proizvod)
                .WithMany(p => p.ProizvodSastojci)
                .HasForeignKey(ps => ps.ProizvodID);

            modelBuilder.Entity<ProizvodSastojak>()
                .HasRequired(ps => ps.Sastojak)
                .WithMany(s => s.ProizvodSastojci)
                .HasForeignKey(ps => ps.SastojakID);

            // Kompozitni kljucevi
            modelBuilder.Entity<StavkaRacuna>()
                .HasKey(sr => new { sr.RacunID, sr.ProizvodID });

            modelBuilder.Entity<StavkaRacuna>()
                .HasRequired(sr => sr.Racun)
                .WithMany(r => r.StavkeRacuna)
                .HasForeignKey(sr => sr.RacunID);

            modelBuilder.Entity<StavkaRacuna>()
                .HasRequired(sr => sr.Proizvod)
                .WithMany(p => p.StavkeRacuna)
                .HasForeignKey(sr => sr.ProizvodID);

            // Jedan na vise relacije
            modelBuilder.Entity<Racun>()
                .HasRequired(r => r.Klijent)
                .WithMany(k => k.Racuni)
                .HasForeignKey(r => r.KlijentID);

            modelBuilder.Entity<Racun>()
                .HasRequired(r => r.Zaposleni)
                .WithMany(z => z.Racuni)
                .HasForeignKey(r => r.ZaposleniID);

            modelBuilder.Entity<Proizvod>()
                .HasOptional(p => p.GrupaProizvoda)
                .WithMany(g => g.Proizvodi)
                .HasForeignKey(p => p.GrupaProizvodaID);

            modelBuilder.Entity<Proizvod>()
                .HasOptional(p => p.VrstaProizvoda)
                .WithMany(v => v.Proizvodi)
                .HasForeignKey(p => p.VrstaProizvodaID);

            base.OnModelCreating(modelBuilder);
        }
    }
}