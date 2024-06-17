using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RVASIspit.Models
{
    public class Racun
    {
        [Key]
        public int RacunID { get; set; }

        public int KlijentID { get; set; }
        public Klijent Klijent { get; set; }

        public int ZaposleniID { get; set; }
        public Zaposleni Zaposleni { get; set; }

        public DateTime? DatumIzdavanja { get; set; }
        public decimal UkupnaCena { get; set; }

        public ICollection<StavkaRacuna> StavkeRacuna { get; set; }
    }
}