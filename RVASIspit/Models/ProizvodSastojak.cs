using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RVASIspit.Models
{
    public class ProizvodSastojak
    {
        [Key]
        public int ProizvodID { get; set; }

        public Proizvod Proizvod { get; set; }

        [Key]
        public int SastojakID { get; set; }

        public Sastojak Sastojak { get; set; }
    }
}