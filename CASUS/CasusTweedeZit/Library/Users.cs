using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Users
    {
        private static string connString = ConfigurationManager.AppSettings["connString"];

        public int Id { get; set; }
        public string voornaam { get; set; }
        public string achternaam { get; set; }
        public string email { get; set; }
        public string nummer { get; set; }
        public string gemeente { get; set; }
        public string postcode { get; set; }
        public string telefoonnummer { get; set; }
        public string geslacht { get; set; }


        public Users()
        {

        }
        public Users(int id, string vn)
        {
            Id = id;
            voornaam = vn;
        }

        public Users(int id, string vn, string an, string el, string nr, string gem, string ptc, string tel, string ges) : this(id, vn)
        {
            Id = id;
            voornaam = vn;
            achternaam = an;
            email = el;
            nummer = nr;
            gemeente = gem;
            postcode = ptc;
            telefoonnummer = tel;
            geslacht = ges;
        }

        public override string ToString()
        {
            return $"{Id}: {voornaam} {achternaam}";
        }

        public static List<Users> GetAll()
        {
            List<Users> usrs = new List<Users>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("SELECT id, voornaam, achternaam, email, nummer, gemeente, postcode, telefoonnummer, geslacht FROM Klant", conn);
                SqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string voornaam = Convert.ToString(reader["voornaam"]);
                    string achternaam = Convert.ToString(reader["achternaam"]);
                    string email = Convert.ToString(reader["email"]);
                    string nummer = Convert.ToString(reader["nummer"]);
                    string gemeente = Convert.ToString(reader["gemeente"]);
                    string postcode = Convert.ToString(reader["postcode"]);
                    string telefoonnummer = Convert.ToString(reader["telefoonnummer"]);
                    string geslacht = Convert.ToString(reader["geslacht"]);
                    usrs.Add(new Users(id, voornaam, achternaam, email, nummer, gemeente, postcode, telefoonnummer, geslacht));
                }
            }
            return usrs;
        }

        public static Users FindById(int empId)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                // open connectie
                conn.Open();

                // voer SQL commando uit
                SqlCommand comm = new SqlCommand("SELECT voornaam, achternaam, email, nummer, gemeente, postcode, telefoonnummer, geslacht FROM Klant WHERE ID = @parID", conn);
                comm.Parameters.AddWithValue("@parID", empId);
                SqlDataReader reader = comm.ExecuteReader();

                // lees en verwerk resultaten
                if (!reader.Read()) return null;
                string voornaam = Convert.ToString(reader["voornaam"]);
                string achternaam = Convert.ToString(reader["achternaam"]);
                string email = Convert.ToString(reader["email"]);
                string nummer = Convert.ToString(reader["nummer"]);
                string gemeente = Convert.ToString(reader["gemeente"]);
                string postcode = Convert.ToString(reader["postcode"]);
                string telefoonnummer = Convert.ToString(reader["telefoonnummer"]);
                string geslacht = Convert.ToString(reader["geslacht"]);
                return new Users(empId, voornaam, achternaam, email, nummer, gemeente, postcode, telefoonnummer, geslacht);
            }
        }

        public void DeleteFromDb()
        {
            // verwijder werknemer
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("DELETE FROM Klant WHERE ID = @parID", conn);
                comm.Parameters.AddWithValue("@parID", Id);
                comm.ExecuteNonQuery();
            }

        }
        public int InsertToDb()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(
                  "INSERT INTO Klant(voornaam, achternaam, email, nummer, gemeente, postcode, telefoonnummer, geslacht) output INSERTED.ID VALUES(@par1,@par2,@par3,@par4,@par5,@par6,@par7,@par8)", conn);
                comm.Parameters.AddWithValue("@par1", voornaam);
                comm.Parameters.AddWithValue("@par2", achternaam);
                comm.Parameters.AddWithValue("@par3", email);
                comm.Parameters.AddWithValue("@par4", nummer);
                comm.Parameters.AddWithValue("@par5", gemeente);
                comm.Parameters.AddWithValue("@par6", postcode);
                comm.Parameters.AddWithValue("@par7", telefoonnummer);
                comm.Parameters.AddWithValue("@par8", geslacht);
                return (int)comm.ExecuteScalar();
            }
        }
        public void UpdateInDb()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                // STANDARD QUERY VERSION
                SqlCommand comm = new SqlCommand(
                    @"UPDATE Klant
                        SET voornaam=@par1, achternaam=@par2, email=@par3, nummer=@par4, gemeente=@par5, postcode=@par6, telefoonnummer=@par7, geslacht=@par8 
                        WHERE id = @parID"
                    , conn);
                comm.Parameters.AddWithValue("@par1", voornaam);
                comm.Parameters.AddWithValue("@par2", achternaam);
                comm.Parameters.AddWithValue("@par3", email);
                comm.Parameters.AddWithValue("@par4", nummer);
                comm.Parameters.AddWithValue("@par5", gemeente);
                comm.Parameters.AddWithValue("@par6", postcode);
                comm.Parameters.AddWithValue("@par7", telefoonnummer);
                comm.Parameters.AddWithValue("@par8", geslacht);
                comm.Parameters.AddWithValue("@parID", Id);
                comm.ExecuteNonQuery();


            }

        }

    }
}
