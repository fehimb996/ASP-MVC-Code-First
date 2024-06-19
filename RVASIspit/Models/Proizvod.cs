using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RVASIspit.Models
{
    public class Proizvod
    {
        [Key]
        public int ProizvodID { get; set; }

        [Required(ErrorMessage = "Naziv proizvoda ne sme da bude prazan.")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Cena proizvoda ne sme da bude prazna.")]
        [Range(0, double.MaxValue, ErrorMessage = "Cena mora biti veća od 0.")]
        public decimal Cena { get; set; }

        public int? GrupaProizvodaID { get; set; }
        public GrupaProizvoda GrupaProizvoda { get; set; }

        public int? VrstaProizvodaID { get; set; }
        public VrstaProizvoda VrstaProizvoda { get; set; }

        public ICollection<ProizvodSastojak> ProizvodSastojci { get; set; }
        public ICollection<StavkaRacuna> StavkeRacuna { get; set; }
    }
}