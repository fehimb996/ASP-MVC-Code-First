using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RVASIspit.Models
{
    public class StavkaRacuna
    {
        [Key]
        public int RacunID { get; set; }

        public Racun Racun { get; set; }

        [Key]
        public int ProizvodID { get; set; }

        public Proizvod Proizvod { get; set; }

        public decimal Cena { get; set; }
    }
}