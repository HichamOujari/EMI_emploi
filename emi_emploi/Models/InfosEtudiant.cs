using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace emi_emploi.Models
{
    public class InfosEtudiant
    {
        public int id { get; set; }
        public String nom { get; set; }
        public String prenom { get; set; }
        public String email { get; set; }
        public DateTime date_naissance { get; set; }
        public String Groupe { get; set; }
        public String filiere { get; set; }
        public String promotion { get; set; }
    }
}