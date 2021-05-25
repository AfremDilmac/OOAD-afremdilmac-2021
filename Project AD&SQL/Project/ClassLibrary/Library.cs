using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Library
    {
        //variable
        private static string connString = ConfigurationManager.AppSettings["connString"];

        //properities
        public string Lidnummer {get; set;}
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public DateTime? Geboortedatum { get; set; }
        public string Straat { get; set; }
        public string Nummer { get; set; }
        public string Postcode { get; set; }
        public string Gemeente { get; set; }
        public DateTime? vervaldatum_lidkaart { get; set; }
        public string Gsm { get; set; }

        //methodes
        public static List<Library> GetAll() {
            List<Library> mwrs = new List<Library>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT lidnummer, voornaam, achternaam, geboortedatum, straat, nummer, postcode, gemeente, vervaldatum_lidkaart, gsm FROM Lid", conn);
                SqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    string lidnummer = Convert.ToString(reader["lidnummer"]);
                    string firstname = Convert.ToString(reader["voornaam"]);
                    string lastname = Convert.ToString(reader["achternaam"]);
                    DateTime geboortedatum = Convert.ToDateTime(reader["geboortedatum"]);
                    string straat = Convert.ToString(reader["straat"]);
                    string nummer = Convert.ToString(reader["nummer"]);
                    string postcode = Convert.ToString(reader["postcode"]);
                    string gemeente = Convert.ToString(reader["gemeente"]);
                    DateTime vervaldatum_lidkaart = Convert.ToDateTime(reader["vervaldatum_lidkaart"]);
                    string gsm = Convert.ToString(reader["gsm"]);

                    mwrs.Add(new Library(lidnummer, firstname, lastname, geboortedatum, straat, nummer, postcode, gemeente, vervaldatum_lidkaart, gsm));
                }

            }
            return mwrs;
        }

        public static Library FindById(int empId)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                // open connectie
                conn.Open();

                // voer SQL commando uit
                SqlCommand comm = new SqlCommand("SELECT lidnummer, voornaam, achternaam, geboortedatum, straat, nummer, postcode, gemeente, vervaldatum_lidkaart, gsm FROM Lid WHERE lidnummer = @par1", conn);
                comm.Parameters.AddWithValue("@par1", empId);
                SqlDataReader reader = comm.ExecuteReader();

                // lees en verwerk resultaten
                if (!reader.Read()) return null;
                string lidnummer = Convert.ToString(reader["lidnummer"]); ;
                string firstname = Convert.ToString(reader["voornaam"]);
                string lastname = Convert.ToString(reader["achternaam"]);
                DateTime geboortedatum = Convert.ToDateTime(reader["geboortedatum"]);
                string straat = Convert.ToString(reader["straat"]);
                string nummer = Convert.ToString(reader["nummer"]);
                string postcode = Convert.ToString(reader["postcode"]);
                string gemeente = Convert.ToString(reader["gemeente"]);
                DateTime vervaldatum_lidkaart = Convert.ToDateTime(reader["vervaldatum_lidkaart"]);
                string gsm = Convert.ToString(reader["gsm"]);
                return new Library(lidnummer, firstname, lastname, geboortedatum, straat, nummer, postcode, gemeente, vervaldatum_lidkaart, gsm);
            }
        }

        public static Library GetById(string lidnummer) {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT lidnummer, voornaam, achternaam, geboortedatum, straat, nummer, postcode, gemeente, vervaldatum_lidkaart, gsm FROM Lid WHERE lidnummer = @par1", conn);
                comm.Parameters.AddWithValue("@par1", lidnummer);
                SqlDataReader reader = comm.ExecuteReader();
                reader.Read();
                string firstname = Convert.ToString(reader["voornaam"]);
                string lastname = Convert.ToString(reader["achternaam"]);
                DateTime geboortedatum = Convert.ToDateTime(reader["geboortedatum"]);
                string straat = Convert.ToString(reader["straat"]);
                string nummer = Convert.ToString(reader["nummer"]);
                string postcode = Convert.ToString(reader["postcode"]);
                string gemeente = Convert.ToString(reader["gemeente"]);
                DateTime vervaldatum_lidkaart = Convert.ToDateTime(reader["vervaldatum_lidkaart"]);
                string gsm = Convert.ToString(reader["gsm"]);
                return new Library(lidnummer, firstname, lastname, geboortedatum, straat, nummer, postcode, gemeente, vervaldatum_lidkaart, gsm);
            }
        }

        public void DeleteFromDb() {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("DELETE FROM Lid WHERE lidnummer = @par1", conn);
                comm.Parameters.AddWithValue("@par1", Lidnummer);
                comm.ExecuteNonQuery();
            }
        }

        public void UpdateInDb()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(
                    @"UPDATE Lid
                        SET voornaam=@par1, achternaam=@par2, geboortedatum=@par3, straat=@par4, nummer=@par5, postcode=@par6, gemeente=@par7, vervaldatum_lidkaart=@par8, gsm=@par9
                        WHERE lidnummer = @parID"
                    , conn);
                comm.Parameters.AddWithValue("@par1", Voornaam);
                comm.Parameters.AddWithValue("@par2", Achternaam);
                if (Geboortedatum == null)
                {
                    comm.Parameters.AddWithValue("@par3", DBNull.Value);

                }
                else
                {
                    comm.Parameters.AddWithValue("@par3", Geboortedatum);
                }

                if (vervaldatum_lidkaart == null)
                {
                    comm.Parameters.AddWithValue("@par8", DBNull.Value);
                }

                else
                {
                    comm.Parameters.AddWithValue("@par8", vervaldatum_lidkaart);
                }

                comm.Parameters.AddWithValue("@par4", Straat);
                comm.Parameters.AddWithValue("@par5", Nummer);
                comm.Parameters.AddWithValue("@par6", Postcode);
                comm.Parameters.AddWithValue("@par7", Gemeente);
                comm.Parameters.AddWithValue("@par9", Gsm);
                comm.Parameters.AddWithValue("@par10", Lidnummer);
                comm.Parameters.AddWithValue("@parID", Lidnummer);
                comm.ExecuteNonQuery();
            }
        }

        public void InsertInDb() {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(
                  "INSERT INTO Lid(voornaam,achternaam,geboortedatum,straat,nummer,postcode,gemeente,vervaldatum_lidkaart,gsm, lidnummer) VALUES(@par1,@par2,@par3,@par4,@par5,@par6,@par7,@par8, @par9, @par10)", conn);
                comm.Parameters.AddWithValue("@par1", Voornaam);
                comm.Parameters.AddWithValue("@par2", Achternaam);
                if (Geboortedatum == null)
                {
                    comm.Parameters.AddWithValue("@par3", DBNull.Value);

                }
                else {
                    comm.Parameters.AddWithValue("@par3", Geboortedatum);
                }

                if (vervaldatum_lidkaart == null)
                {
                    comm.Parameters.AddWithValue("@par8", DBNull.Value);
                }

                else
                {
                    comm.Parameters.AddWithValue("@par8", vervaldatum_lidkaart);
                }

                comm.Parameters.AddWithValue("@par4", Straat);
                comm.Parameters.AddWithValue("@par5", Nummer);
                comm.Parameters.AddWithValue("@par6", Postcode);
                comm.Parameters.AddWithValue("@par7", Gemeente);
                comm.Parameters.AddWithValue("@par9", Gsm);
                comm.Parameters.AddWithValue("@par10", Lidnummer);
                comm.ExecuteNonQuery();
            }
        }

        //constructors
        public Library()
        { 
        }
        //Medewerkers
        public Library(string lidnummer, string vn, string an)
        {
            Lidnummer = lidnummer;
            Voornaam = vn;
            Achternaam = an;
        }

        public Library(string lidnummer, string vn, string an, DateTime? geboortedatum, string straat, string nummer, string postcode, string gemeente, DateTime? vervaldatum_lidkaart, string gsm) : this(lidnummer, vn, an)
        {
            Geboortedatum = geboortedatum;
            Straat = straat;
            Nummer = nummer;
            Postcode = postcode;
            Gemeente = gemeente;
            this.vervaldatum_lidkaart = vervaldatum_lidkaart;
            Gsm = gsm;
        }

        public Library(string firstname, string lastname, DateTime? geboortedatum, DateTime? vervaldatum_lidkaart, string straat, string nummer, string postcode, string gemeente, string gsm)
        {
            this.Voornaam = firstname;
            this.Achternaam = lastname;
            Geboortedatum = geboortedatum;
            this.vervaldatum_lidkaart = vervaldatum_lidkaart;
            Straat = straat;
            this.Nummer = nummer;
            this.Postcode = postcode;
            Gemeente = gemeente;
            Gsm = gsm;
        }

        public override string ToString()
        {
            return $"{Lidnummer}: {Voornaam} {Achternaam}";

        }

    }
}
