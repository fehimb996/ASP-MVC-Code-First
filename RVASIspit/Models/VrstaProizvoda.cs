using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RVASIspit.Models
{
    public class VrstaProizvoda
    {
        [Key]
        public int VrstaProizvodaID { get; set; }

        public string NazivVrste { get; set; }

        public ICollection<Proizvod> Proizvodi { get; set; }
    }
}