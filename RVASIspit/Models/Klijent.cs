using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RVASIspit.Models
{
    public class Klijent
    {
        [Key]
        public int KlijentID { get; set; }

        [Required(ErrorMessage = "Ime klijenta ne sme da bude prazan.")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime klijenta ne sme da bude prazan.")]
        public string Prezime { get; set; }


        public string Telefon { get; set; }

        public ICollection<Racun> Racuni { get; set; }
    }
}