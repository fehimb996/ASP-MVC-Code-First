using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RVASIspit.Models
{
    public class Zaposleni
    {
        [Key]
        public int ZaposleniID { get; set; }

        public string Ime { get; set; }

        public string Prezime { get; set; }

        public ICollection<Racun> Racuni { get; set; }
    }
}