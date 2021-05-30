using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace emi_emploi.Models
{
    public class Groupe
    {
        public int id { get; set; }
        public string nom { get; set; }
        public int IdFiliere { get; set; }
    }
}
