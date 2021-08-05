using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExportationApp.Entites
{
    class Image
    {
       public string titre { get; set; }
       public string chemin { get; set; }
       
        public Image(string titre, string chemin)
        {
            this.titre = titre;
            this.chemin = chemin;
        }
    }
}
