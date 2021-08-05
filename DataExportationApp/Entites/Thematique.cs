using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExportationApp.Entites
{
    class Thematique
    {
        public int id_thematique { get; set; }
        public string libelle { get; set; }
        public Thematique(string libelle)
        {
            this.libelle = libelle;

        }
    }
}
//no used 