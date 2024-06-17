using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RVASIspit.Models
{
    public class Sastojak
    {
        [Key]
        public int SastojakID { get; set; }

        public string NazivSastojka { get; set; }

        public ICollection<ProizvodSastojak> ProizvodSastojci { get; set; }
    }
}