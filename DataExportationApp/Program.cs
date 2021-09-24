using System;
using System.Data.SQLite;
using System.IO;
using DataExportationApp.Entites;
using System.Text;
using System.Collections.Generic;
using ExcelDataReader;

namespace DataExportationApp
{
    class Program
    {
        static void Main(string[] args)
        {

            //Récupération dossier client-projet (A MODIFIER L EMPLACEMENT)
            string cheminOrig = @"C:\Users\nessahih\Desktop\client-projet";
            Database db = new Database();

            int numC = 0;
            Client client;
            Projet projet;

            //Recuperation des dossier clients
            foreach (var dossierclient in Directory.GetDirectories(cheminOrig))
            {
                client = new Client();
                int numP = 0;
                numC++;

                client.nom_client = new DirectoryInfo(dossierclient).Name;
                client.id_client = numC;
                string chemin = cheminOrig + @"\" + client.nom_client;

                //Récupération de description client
                client.description = File.ReadAllText(chemin + @"\Description.txt");            

                //Récupération des thematiques du client
                foreach (string thematique in File.ReadLines(chemin + @"\Thematiques.txt"))
                {
                   client.thematiques.Add(thematique);
                }

                //Insertiont Client dans BD 
                db.InsertClient(client);

                //Récupération des dossier Projets
                foreach (var dossierprojet in Directory.GetDirectories(chemin))
                {

                    projet = new Projet();
                    numP++;

                    projet.libelle = new DirectoryInfo(dossierprojet).Name;
                    projet.id_projet = numP;

                    var cheminProj = chemin + @"\" + projet.libelle;
                    
                    //Recuperation de description du projet
                    projet.description = File.ReadAllText(cheminProj + @"\description.txt");
                    

                    //Récuperation fichier excel d'info géneral 

                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    FileStream streamA = File.Open(cheminProj + @"\informationGeneral.xlsx", FileMode.Open, FileAccess.Read);
                    IExcelDataReader excelReaderA = ExcelReaderFactory.CreateOpenXmlReader(streamA);
                    var resultA = excelReaderA.AsDataSet();
                    var dataTableA = resultA.Tables[0];


                    for (var i = 0; i < dataTableA.Rows.Count; i++)
                    {
                      
                        projet.domaine = (string) dataTableA.Rows[i][0];
                        projet.code_wtr = (string) dataTableA.Rows[i][1];
                        projet.statut = (string)dataTableA.Rows[i][2];

                    }

                    //Récupération des thematiques du projet
                    foreach (string thematique in File.ReadLines(cheminProj + @"\thematiques.txt"))
                    {
                        projet.thematiquesProjet.Add(thematique);
                    }

                    //Récupération des technologies
                    foreach (string technologies in File.ReadLines(cheminProj + @"\Technologies.txt"))
                    {
                        projet.technologiesProjet.Add(technologies);
                    }

                    //Récupération du galerie du projet
                    foreach (var imagePath in Directory.GetFiles(cheminProj + @"\galerie"))
                    {
                        var imageName = Path.GetFileNameWithoutExtension(imagePath);
                        Image image = new Image(imageName, imagePath);
                        projet.galerie.Add(image);
                    }

                    client.projets.Add(projet);
                }
                db.InsertIntoDb(client);

            }

            //Recuperation des fichiers SUPERBO

            Collaborateur collaborateur;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            FileStream stream = File.Open(@"C:\Users\nessahih\Desktop\collaborateurs\SUPERBO PERO 032021.xlsx", FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            var result = excelReader.AsDataSet();
            var dataTable = result.Tables[0];

           
            for (var i = 10; i < dataTable.Rows.Count; i++)
            {
                var codeCollab = (string) dataTable.Rows[i][0];
                var codeProjet = (string) dataTable.Rows[i][12];
                var Days = (double) dataTable.Rows[i][19];
                //Console.WriteLine(dataTable.Rows[i][19]);
                var NomCollab = (string)dataTable.Rows[i][2];
                string[] Nom = NomCollab.Split(",");
                
                collaborateur = new Collaborateur(codeCollab, Nom[0], Nom[1]);
                
               db.InsertColab(collaborateur, codeProjet, Days);

            }

        }
    }
}




//Récupération des technologies Images
/*foreach (var technologies in Directory.GetFiles(cheminProj + @"\technologies"))
{
    var path = Path.GetFullPath(technologies);
    var name = Path.GetFileNameWithoutExtension(technologies);
    Image technologie = new Image(name, path);
    projet.technologiesProjet.Add(technologies);

    Console.WriteLine(path);
    Console.WriteLine(name);


----------------------
//Recuperation de code_WTR du projet
                    projet.code_wtr = File.ReadAllText(cheminProj + @"\code_wtr.txt");

                    //Recuperation du domaine du projet
                    projet.domaine = File.ReadAllText(cheminProj + @"\domaine.txt");
}*/















//foreach (var dosiser in Directory.GetDirectories(cheminOrig)) {
//Récupération des fichiers BO 

//Récupération dossier client-projet (ICI) 
// }