using System;
using System.Data.SQLite;
using System.IO;
using DataExportationApp.Entites;
using System.Text;
using System.Collections.Generic;


namespace DataExportationApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string cheminOrig = @"C:\Users\nessahih\Desktop\test_répertoire";

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

                //Récupération des dossier Projets
                foreach (var dossierprojet in Directory.GetDirectories(chemin))
                {

                    projet = new Projet();
                    numP++;

                    projet.libelle = new DirectoryInfo(dossierprojet).Name;
                    projet.id_projet = numP;

                    var cheminProj = chemin + @"\" + projet.libelle;
                    
                    //Recuperation de description du projet
                    projet.description = File.ReadAllText(cheminProj + @"\Description.txt");

                    //Recuperation de code_WTR du projet
                    projet.code_wtr = File.ReadAllText(cheminProj + @"\code_wtr.txt");

                    //Récupération des thematiques du projet
                    foreach (string thematique in File.ReadLines(cheminProj + @"\Thematiques.txt"))
                    {
                        projet.thematiquesProjet.Add(thematique);
                    }

                    //Récupération des technologies
                    foreach (var technologies in Directory.GetFiles(cheminProj + @"\technologies"))
                    {
                        var path = Path.GetFullPath(technologies);
                        var name = Path.GetFileNameWithoutExtension(technologies);
                        Image technologie = new Image(name, path);
                        projet.technologies.Add(technologie);

                        Console.WriteLine(path);
                        Console.WriteLine(name);
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

                //Insertiont dans BD 
                Database db = new Database();
                db.InsertIntoDb(client);
            }

        }
    }
}




