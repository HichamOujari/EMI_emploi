using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace emi_emploi.Models
{
    public class Disponibilite
    {
        public int id { get; set; }
        public int IdMatiere { get; set; }
        public int IdGroupe { get; set; }
        public int IdEnseignant { get; set; }
        public int IdSalle { get; set; }
        public int jour { get; set; }
        public int IdSemaine_depart { get; set; }
        public int IdSemaine_fin { get; set; }
        public int heure_debut { get; set; }
        public int heure_fin { get; set; }
    }
}
