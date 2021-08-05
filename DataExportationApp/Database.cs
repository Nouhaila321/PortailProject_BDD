using System.Data.SQLite;
using System.IO;
using DataExportationApp.Entites;

namespace DataExportationApp
{
    class Database
    {
        public SQLiteConnection Connection;

       public Database()
       {
            Connection = new SQLiteConnection(@"Data Source= C:\Portail_projects\BackendAPP\BackendAPP\Portail_projets.db");
            if (!File.Exists(@"C:\Portail_projects\BackendAPP\BackendAPP\Portail_projets.db"))
            {
                System.Console.WriteLine("DB NOT FOUND...  ");
            }
            else
            {
                System.Console.WriteLine("CONNECTED TO Portail_projets.db ... ");
            }
       }

        public void OpenConnection()
        {
            if(Connection.State != System.Data.ConnectionState.Open)
            {
                Connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (Connection.State != System.Data.ConnectionState.Closed)
            {
                Connection.Close();
            }
        }

        public void InsertIntoDb(Client client)
        {
            this.OpenConnection();

            //Vider les tables

            //TABLE CLIENT
            string query = "INSERT INTO Client (`id_client`,`Description`, `Nom_Client`) VALUES (@id_client, @presentation, @nom_client)";
            SQLiteCommand com = new SQLiteCommand(query, this.Connection);

            com.Parameters.AddWithValue("@id_client", client.id_client);
            com.Parameters.AddWithValue("@presentation", client.description);
            com.Parameters.AddWithValue("@nom_client", client.nom_client);

            com.ExecuteNonQuery();


            //TABLE ClientTHEMATIQUE
            foreach (string thematique in client.thematiques)
            {
                string query1 = "INSERT INTO ClientThematique (`Id_Thematique`, `Id_Client`, `Nom_Thematique`) VALUES (@id_thematique, @id_client, @nom_thematique)";
                SQLiteCommand com1 = new SQLiteCommand(query1, this.Connection);

                com1.Parameters.AddWithValue("@id_thematique", client.thematiques.FindIndex(a => a == thematique));
                com1.Parameters.AddWithValue("@id_client", client.id_client);
                com1.Parameters.AddWithValue("@nom_thematique",thematique);
                
               
                com1.ExecuteNonQuery();

            }

            //TABLE PROJET 
            foreach(Projet projet in client.projets)
            {
                string query1 = "INSERT INTO Projet (`Id_projet`, `Libelle`, `description`, `id_client`) VALUES (@id_projet, @libelle, @description, @id_client)";
                SQLiteCommand com1 = new SQLiteCommand(query1, this.Connection);

                com1.Parameters.AddWithValue("@id_projet", projet.id_projet );
                com1.Parameters.AddWithValue("@libelle", projet.libelle);
                com1.Parameters.AddWithValue("@description", projet.description);
                com1.Parameters.AddWithValue("@id_client", client.id_client);

                com1.ExecuteNonQuery();

                //TABLE ProjetTHEMATIQUES
                foreach (string thematique in projet.thematiquesProjet)
                {
                    string query2 = "INSERT INTO ProjetThematique (`Id_Thematique`, `Id_projet`, `id_client`, `Nom_Thematique`) VALUES (@id_thematique, @id_projet, @id_client, @nom_thematique)";
                    SQLiteCommand com2 = new SQLiteCommand(query2, this.Connection);

                    com2.Parameters.AddWithValue("@id_thematique", projet.thematiquesProjet.FindIndex(a => a == thematique));
                    com2.Parameters.AddWithValue("@id_projet", projet.id_projet);
                    com2.Parameters.AddWithValue("@id_client", client.id_client);
                    com2.Parameters.AddWithValue("@nom_thematique", thematique);


                    com2.ExecuteNonQuery();
                }

                //TABLE ProjetTECHNOLOGIES
                foreach (Image technologie in projet.technologies)
                {
                    string query2 = "INSERT INTO ProjetTechnologie (`Id_technologie`, `Id_projet`, `id_client`, `Nom_Technologie`, `chemin_technologie`) VALUES (@id_technologie, @id_projet, @id_client, @nom_technologie, @nom_technologie)";
                    SQLiteCommand com2 = new SQLiteCommand(query2, this.Connection);

                    com2.Parameters.AddWithValue("@id_technologie", projet.technologies.FindIndex(a => a == technologie));
                    com2.Parameters.AddWithValue("@id_projet", projet.id_projet);
                    com2.Parameters.AddWithValue("@id_client", client.id_client);
                    com2.Parameters.AddWithValue("@nom_technologie", technologie.titre);
                    com2.Parameters.AddWithValue("@chemin", technologie.chemin);


                    com2.ExecuteNonQuery();

                }

                //TABLE ProjetGalerie
                foreach (Image image in projet.technologies)
                {
                    string query2 = "INSERT INTO ProjetGalerie (`Id_image`, `Id_projet`, `id_client`, `titre`, `chemin`) VALUES (@id_image, @id_projet, @id_client, @titre, @chemin)";
                    SQLiteCommand com2 = new SQLiteCommand(query2, this.Connection);

                    com2.Parameters.AddWithValue("@id_image", projet.technologies.FindIndex(a => a == image));
                    com2.Parameters.AddWithValue("@id_projet", projet.id_projet);
                    com2.Parameters.AddWithValue("@id_client", client.id_client);
                    com2.Parameters.AddWithValue("@titre", image.titre);
                    com2.Parameters.AddWithValue("@chemin", image.chemin);
                    
                    com2.ExecuteNonQuery();

                }
            }

            this.CloseConnection();
        }
    }
}
