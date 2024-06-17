namespace RVASIspit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GrupaProizvoda",
                c => new
                    {
                        GrupaProizvodaID = c.Int(nullable: false, identity: true),
                        NazivGrupe = c.String(),
                    })
                .PrimaryKey(t => t.GrupaProizvodaID);
            
            CreateTable(
                "dbo.Proizvod",
                c => new
                    {
                        ProizvodID = c.Int(nullable: false, identity: true),
                        Naziv = c.String(nullable: false),
                        Cena = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GrupaProizvodaID = c.Int(),
                        VrstaProizvodaID = c.Int(),
                    })
                .PrimaryKey(t => t.ProizvodID)
                .ForeignKey("dbo.GrupaProizvoda", t => t.GrupaProizvodaID)
                .ForeignKey("dbo.VrstaProizvoda", t => t.VrstaProizvodaID)
                .Index(t => t.GrupaProizvodaID)
                .Index(t => t.VrstaProizvodaID);
            
            CreateTable(
                "dbo.ProizvodSastojak",
                c => new
                    {
                        ProizvodID = c.Int(nullable: false),
                        SastojakID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProizvodID, t.SastojakID })
                .ForeignKey("dbo.Proizvod", t => t.ProizvodID, cascadeDelete: true)
                .ForeignKey("dbo.Sastojak", t => t.SastojakID, cascadeDelete: true)
                .Index(t => t.ProizvodID)
                .Index(t => t.SastojakID);
            
            CreateTable(
                "dbo.Sastojak",
                c => new
                    {
                        SastojakID = c.Int(nullable: false, identity: true),
                        NazivSastojka = c.String(),
                    })
                .PrimaryKey(t => t.SastojakID);
            
            CreateTable(
                "dbo.StavkaRacuna",
                c => new
                    {
                        RacunID = c.Int(nullable: false),
                        ProizvodID = c.Int(nullable: false),
                        Cena = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.RacunID, t.ProizvodID })
                .ForeignKey("dbo.Proizvod", t => t.ProizvodID, cascadeDelete: true)
                .ForeignKey("dbo.Racun", t => t.RacunID, cascadeDelete: true)
                .Index(t => t.RacunID)
                .Index(t => t.ProizvodID);
            
            CreateTable(
                "dbo.Racun",
                c => new
                    {
                        RacunID = c.Int(nullable: false, identity: true),
                        KlijentID = c.Int(nullable: false),
                        ZaposleniID = c.Int(nullable: false),
                        DatumIzdavanja = c.DateTime(),
                        UkupnaCena = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.RacunID)
                .ForeignKey("dbo.Klijent", t => t.KlijentID, cascadeDelete: true)
                .ForeignKey("dbo.Zaposleni", t => t.ZaposleniID, cascadeDelete: true)
                .Index(t => t.KlijentID)
                .Index(t => t.ZaposleniID);
            
            CreateTable(
                "dbo.Klijent",
                c => new
                    {
                        KlijentID = c.Int(nullable: false, identity: true),
                        Ime = c.String(nullable: false),
                        Prezime = c.String(nullable: false),
                        Telefon = c.String(),
                    })
                .PrimaryKey(t => t.KlijentID);
            
            CreateTable(
                "dbo.Zaposleni",
                c => new
                    {
                        ZaposleniID = c.Int(nullable: false, identity: true),
                        Ime = c.String(),
                        Prezime = c.String(),
                    })
                .PrimaryKey(t => t.ZaposleniID);
            
            CreateTable(
                "dbo.VrstaProizvoda",
                c => new
                    {
                        VrstaProizvodaID = c.Int(nullable: false, identity: true),
                        NazivVrste = c.String(),
                    })
                .PrimaryKey(t => t.VrstaProizvodaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Proizvod", "VrstaProizvodaID", "dbo.VrstaProizvoda");
            DropForeignKey("dbo.StavkaRacuna", "RacunID", "dbo.Racun");
            DropForeignKey("dbo.Racun", "ZaposleniID", "dbo.Zaposleni");
            DropForeignKey("dbo.Racun", "KlijentID", "dbo.Klijent");
            DropForeignKey("dbo.StavkaRacuna", "ProizvodID", "dbo.Proizvod");
            DropForeignKey("dbo.ProizvodSastojak", "SastojakID", "dbo.Sastojak");
            DropForeignKey("dbo.ProizvodSastojak", "ProizvodID", "dbo.Proizvod");
            DropForeignKey("dbo.Proizvod", "GrupaProizvodaID", "dbo.GrupaProizvoda");
            DropIndex("dbo.Racun", new[] { "ZaposleniID" });
            DropIndex("dbo.Racun", new[] { "KlijentID" });
            DropIndex("dbo.StavkaRacuna", new[] { "ProizvodID" });
            DropIndex("dbo.StavkaRacuna", new[] { "RacunID" });
            DropIndex("dbo.ProizvodSastojak", new[] { "SastojakID" });
            DropIndex("dbo.ProizvodSastojak", new[] { "ProizvodID" });
            DropIndex("dbo.Proizvod", new[] { "VrstaProizvodaID" });
            DropIndex("dbo.Proizvod", new[] { "GrupaProizvodaID" });
            DropTable("dbo.VrstaProizvoda");
            DropTable("dbo.Zaposleni");
            DropTable("dbo.Klijent");
            DropTable("dbo.Racun");
            DropTable("dbo.StavkaRacuna");
            DropTable("dbo.Sastojak");
            DropTable("dbo.ProizvodSastojak");
            DropTable("dbo.Proizvod");
            DropTable("dbo.GrupaProizvoda");
        }
    }
}
