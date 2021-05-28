using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Data;



namespace ItemsLibrary
{
    public class BibItems
    {
        private static string connString = ConfigurationManager.AppSettings["connString"];
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Beschrijving { get; set; }
        public Byte[] Coverfoto { get; set; }
        public string Uitgeverij { get; set; }
        public int? Leeftijd_van { get; set; }
        public int? Leeftijd_tot { get; set; }
        public string Taal { get; set; }



        public BibItems()
        {
        }
        public BibItems(int id, string titel, string beschrijving)
        {
            Id = id;
            Titel = titel;
            Beschrijving = beschrijving;
        }
        public BibItems(int id, string titel, string beschrijving, string uitgeverij, int? leeftijd_van, int? leeftijd_tot, string taal) : this(id, titel, beschrijving)
        {
            Uitgeverij = uitgeverij;
            Leeftijd_van = leeftijd_van;
            Leeftijd_tot = leeftijd_tot;
            Taal = taal;

        }

        public BibItems(string titel, string beschrijving, string uitgeverij, int? leeftijd_van, int? leeftijd_tot, string taal)
        {
            Titel = titel;
            Beschrijving = beschrijving;
            Uitgeverij = uitgeverij;
            Leeftijd_van = leeftijd_van;
            Leeftijd_tot = leeftijd_tot;
            Taal = taal;
        }

        public static List<BibItems> GetAll()
        {
            List<BibItems> emps = new List<BibItems>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                // open connectie
                conn.Open();

                // voer SQL commando uit
                SqlCommand comm = new SqlCommand("SELECT id, titel, beschrijving, uitgeverij, leeftijd_van, leeftijd_tot, taal FROM Item", conn);
                SqlDataReader reader = comm.ExecuteReader();

                // lees en verwerk resultaten
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string titel = Convert.ToString(reader["titel"]);
                    string beschrijving = Convert.ToString(reader["beschrijving"]);
                    string uitgeverij = Convert.ToString(reader["uitgeverij"]);
                    int? leeftijd_van = reader["leeftijd_van"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["leeftijd_van"]);
                    int? leeftijd_tot = reader["leeftijd_tot"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["leeftijd_tot"]);
                    string taal = Convert.ToString(reader["taal"]);
                    emps.Add(new BibItems(id, titel, beschrijving, uitgeverij, leeftijd_van, leeftijd_tot, taal));
                }
            }
            return emps;
        }
        public static BibItems FindById(int empId)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                // open connectie
                conn.Open();

                // voer SQL commando uit
                SqlCommand comm = new SqlCommand("SELECT titel, beschrijving, coverfoto, uitgeverij, leeftijd_van, leeftijd_tot, taal FROM Item WHERE ID = @parID", conn);
                comm.Parameters.AddWithValue("@parID", empId);
                SqlDataReader reader = comm.ExecuteReader();

                // lees en verwerk resultaten
                if (!reader.Read()) return null;
                string titel = Convert.ToString(reader["titel"]);
                string beschrijving = Convert.ToString(reader["beschrijving"]);
                string uitgeverij = Convert.ToString(reader["uitgeverij"]);
                int? leeftijd_van = reader["leeftijd_van"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["leeftijd_van"]);
                int? leeftijd_tot = reader["leeftijd_tot"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["leeftijd_tot"]);
                string taal = Convert.ToString(reader["taal"]);
                return new BibItems(titel, beschrijving, uitgeverij, leeftijd_van, leeftijd_tot, taal);
            }
        }

        public static BibItems GetById(int id) {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                //Hier krijgt men gegevens via id
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT titel, beschrijving, coverfoto, uitgeverij, leeftijd_van, leeftijd_tot, taal FROM Item WHERE ID = @parID", conn);
                comm.Parameters.AddWithValue("@parID", id);
                SqlDataReader reader = comm.ExecuteReader();
                reader.Read();
                string titel = Convert.ToString(reader["titel"]);
                string beschrijving = Convert.ToString(reader["beschrijving"]);
                string uitgeverij = Convert.ToString(reader["uitgeverij"]);
                int? leeftijd_van = reader["leeftijd_van"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["leeftijd_van"]);
                int? leeftijd_tot = reader["leeftijd_tot"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["leeftijd_tot"]);
                string taal = Convert.ToString(reader["taal"]);
                return new BibItems(titel, beschrijving, uitgeverij, leeftijd_van, leeftijd_tot, taal);

            }
        }

        public void InsertInDb()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                //Nieuw insert 
                conn.Open();
                SqlCommand comm = new SqlCommand(
                  "INSERT INTO Item(titel,beschrijving,uitgeverij,leeftijd_van,leeftijd_tot,taal) VALUES(@par1,@par2,@par3,@par4,@par5,@par6)", conn);
                comm.Parameters.AddWithValue("@par1", Titel);
                comm.Parameters.AddWithValue("@par2", Beschrijving);
                if (Leeftijd_van == null)
                {
                    comm.Parameters.AddWithValue("@par4", DBNull.Value);

                }
                else
                {
                    comm.Parameters.AddWithValue("@par4", Leeftijd_van);
                }

                if (Leeftijd_tot == null)
                {
                    comm.Parameters.AddWithValue("@par5", DBNull.Value);
                }

                else
                {
                    comm.Parameters.AddWithValue("@par5", Leeftijd_tot);
                }

                comm.Parameters.AddWithValue("@par3", Uitgeverij);
                comm.Parameters.AddWithValue("@par6", Taal);
                comm.ExecuteNonQuery();
            }
        }
        public void DeleteFromDb()
        {                //Verwijder methode
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("DELETE FROM Item WHERE id = @par1", conn);
                comm.Parameters.AddWithValue("@par1", Id);
                comm.ExecuteNonQuery();
            }
        }

        public override string ToString()
        {
        return $"{Id}: {Titel}";

        }

    }
}
