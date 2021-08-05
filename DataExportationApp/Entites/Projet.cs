using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExportationApp.Entites
{
    class Projet
    {
        public int id_projet { get; set; }
        public string libelle { get; set;}
        public string description { get; set;}
        public string code_wtr { get; set; }
       
        public List<Image> technologies { get; set; }
        public List<Image> galerie { get; set; }
        public List<string> thematiquesProjet { get; set; }

        public Projet()
        {
            this.thematiquesProjet = new List<string>();
            this.technologies = new List<Image>();
            this.galerie = new List<Image>();
        }

  
    }
}
