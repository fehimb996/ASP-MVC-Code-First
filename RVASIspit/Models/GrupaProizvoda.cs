using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RVASIspit.Models
{
    public class GrupaProizvoda
    {
        [Key]
        public int GrupaProizvodaID { get; set; }

        public string NazivGrupe { get; set; }

        public ICollection<Proizvod> Proizvodi { get; set; }
    }
}