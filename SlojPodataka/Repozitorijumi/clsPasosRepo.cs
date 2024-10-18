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
    public class clsPasosRepo : IPasosRepo
    {
        //polje konekcije
        private string _stringKonekcije;

        //konstruktor
        //dobija se string konekcije pri pozivanju
        public clsPasosRepo(string stringKonekcije)
        {
            _stringKonekcije = stringKonekcije;
        }

        //izlistava sve pasose i daje njihov datum isteka,
        //ime i prezime korisnika kao i datum i vreme termina
        public DataSet DajSvePasose()
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajPasosIzPogleda", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }


        //filtrira pasose po datumu
        public DataSet DajPasosPoDatumu(DateOnly datum)
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajPasosSaDatumom", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IzabraniDatum", SqlDbType.NVarChar).Value = datum;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public DataSet DajPasosPoJMBG(string jmbg)
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajPasosPoJMBG", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@JMBGKorisnika", SqlDbType.NVarChar).Value = jmbg;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public bool NoviPasosITermin(string jmbg, DateOnly datum, TimeOnly vreme, int brojPasosa, int pttMesto)
        {
            //promenljiva za proveru uspesnosti unosa 
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("NoviPasosITermin", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            DateTime datumSaVremenom = datum.ToDateTime(TimeOnly.MinValue);
            Komanda.Parameters.Add("@Datum", SqlDbType.Date).Value = datumSaVremenom;
            Komanda.Parameters.Add("@Vreme", SqlDbType.Time).Value = vreme.ToTimeSpan();
            Komanda.Parameters.Add("@JMBGKorisnika", SqlDbType.NVarChar).Value = jmbg;
            Komanda.Parameters.Add("@BrojPasosa", SqlDbType.Int).Value = brojPasosa;
            Komanda.Parameters.Add("@PTTMesto", SqlDbType.Int).Value = pttMesto;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            //vraca true ako je uspesno
            return (proveraUnosa > 0);
        }

        public bool AžurirajStatusDokumentaPasosa(int brojPasosa, int status)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();

            SqlCommand Komanda = new SqlCommand("AžurirajStatusDokumentaPasosa", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@BrojPasosa", SqlDbType.Int).Value = brojPasosa;
            Komanda.Parameters.Add("@Status", SqlDbType.Int).Value = status;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);
        }
        public bool ObrisiPasosITermin(int IDTermina)
        {
            //promenljiva za proveru uspesnosti unosa 
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("ObrisiPasosITermin", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IDTermina", SqlDbType.NVarChar).Value = IDTermina;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            //vraca true ako je uspesno
            return (proveraUnosa > 0);
        }

        public bool DodajDatumePasosu(DateOnly datumPocetkaVazenja, DateOnly datumIsteka)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DodajDatumePasosu", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            DateTime datumPocetkaVazenjaSaVremenom = datumPocetkaVazenja.ToDateTime(TimeOnly.MinValue);
            Komanda.Parameters.Add("@DatumPocetkaVazenja", SqlDbType.Date).Value = datumPocetkaVazenjaSaVremenom;
            DateTime datumIstekaSaVremenom = datumIsteka.ToDateTime(TimeOnly.MinValue);
            Komanda.Parameters.Add("@DatumIsteka", SqlDbType.Date).Value = datumIstekaSaVremenom;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            //vraca true ako je uspesno
            return (proveraUnosa > 0);
        }

        public bool IzmeniPasos(int brojPasosa, string IdDrzavaDrzavljanstva, string IdDrzavaRodjenja, int statusDokumentaId)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("IzmeniPasos", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@BrojPasosa", SqlDbType.Int).Value = brojPasosa;
            Komanda.Parameters.Add("@IDDrzavaDrzavljanstva", SqlDbType.NVarChar).Value = IdDrzavaDrzavljanstva;
            Komanda.Parameters.Add("@IDDrzavaRodjenja", SqlDbType.NVarChar).Value = IdDrzavaRodjenja;
            Komanda.Parameters.Add("@StatusDokumentaID", SqlDbType.Int).Value = statusDokumentaId;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            //vraca true ako je uspesno
            return (proveraUnosa > 0);
        }
    }
}
