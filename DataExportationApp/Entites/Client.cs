using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExportationApp.Entites
{
    class Client
    {
        public int id_client { get; set; }
        public string nom_client { get; set; }
        public List<Projet> projets { get; set; }
        public string description { get; set; } 
        public List<string> thematiques { get; set; }

        public Client()
        {
            this.thematiques = new List<string>();
            this.projets = new List<Projet>();
        }



    }


}
