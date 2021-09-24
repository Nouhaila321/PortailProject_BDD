using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExportationApp.Entites
{
    class Collaborateur
    {
        public string Code { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public Collaborateur(string Code, string Nom, string Prenom)
        {
            this.Nom = Nom;
            this.Prenom = Prenom; 
            this.Code = Code; 

        }
    }
}
