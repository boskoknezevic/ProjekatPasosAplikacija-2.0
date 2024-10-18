using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlojPodataka.Interfejsi;

namespace SlojPodataka.Repozitorijumi
{
    public class clsTerminRepo : ITerminRepo
    {
        //polje konekcije
        private string _stringKonekcije;

        //konstruktor
        //dobija se string konekcije pri pozivanju
        public clsTerminRepo(string stringKonekcije)
        {
            _stringKonekcije = stringKonekcije;
        }

        public DataSet DajSveTermine()
        {
            DataSet dsPodaci = new DataSet("Termini");

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajSveTermine", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        //izlistava sve pasose i daje njihov datum isteka,
        //ime i prezime korisnika kao i datum i vreme termina
        public List<DateOnly> IzlistajDatume()
        {
            List<DateOnly> datumi = new List<DateOnly>();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("IzlistajDatumeTermina", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            using (SqlDataReader reader = Komanda.ExecuteReader())
            {
                while (reader.Read())
                {

                    if (reader["Datum"] != DBNull.Value)
                    {
                        DateTime datum = (DateTime)reader["Datum"];
                        DateOnly datumDateOnly = DateOnly.FromDateTime(datum);
                        datumi.Add(datumDateOnly);
                    }


                }
            }
            return datumi;
        }

        public bool IzmeniTermin(int idTermina, DateOnly datum, TimeOnly vreme)
        {
            int proveraUnosa = 0;
            using (SqlConnection veza = new SqlConnection(_stringKonekcije))
            {
                veza.Open();
                using (SqlCommand komanda = new SqlCommand("IzmeniTermin", veza))
                {
                    komanda.CommandType = CommandType.StoredProcedure;
                    komanda.Parameters.Add("@IDTermina", SqlDbType.Int).Value = idTermina;

                    DateTime datumDateTime = datum.ToDateTime(TimeOnly.MinValue);
                    komanda.Parameters.Add("@Datum", SqlDbType.Date).Value = datumDateTime;

                    TimeSpan vremeTimeSpan = vreme.ToTimeSpan();
                    komanda.Parameters.Add("@Vreme", SqlDbType.Time).Value = vremeTimeSpan;

                    proveraUnosa = komanda.ExecuteNonQuery();
                }
            }
            return (proveraUnosa > 0);
        }

        public DataSet DajTerminPoID(int idTermina)
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajTerminPoID", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IDTermina", SqlDbType.Int).Value = idTermina;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

    }
}
