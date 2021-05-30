using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace emi_emploi.Models
{
    public class Etudiant
    {

        public int id { get; set; }
        public String nom { get; set; }
        public String prenom { get; set; }
        public String email { get; set; }
        public DateTime date_naissance { get; set; }
        public int IdGroupe { get; set; }
    }
}
