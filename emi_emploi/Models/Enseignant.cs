using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace emi_emploi.Models
{
    public class Enseignant
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string email { get; set; }
        public int IdMatiere { get; set; }
    }
}