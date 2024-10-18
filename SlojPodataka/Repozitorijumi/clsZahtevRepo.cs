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
    public class clsZahtevRepo : IZahtevRepo
    {
        //polje konekcije
        private string _stringKonekcije;

        //konstruktor
        //dobija se string konekcije pri pozivanju
        public clsZahtevRepo(string stringKonekcije)
        {
            _stringKonekcije = stringKonekcije;
        }

        //izlistava sve zahteve i daje catum i vreme podnosenja zahteva,
        //jmbg korisnika kao i status zahteva
        public DataSet DajSveZahteve()
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajSveZahteveSaStatusom", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }


        //filtrira zahteve po statusu - 1-odbijen; 2-na cekanju; 3-odobren
        public DataSet DajSveZahtevePoStatusu(int status)
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajSveZahtevePoStatusu", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@Status", SqlDbType.Int).Value = status;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public DataSet DajSveZahtevePoJMBG(string jmbg)
        {
            DataSet dsPodaci = new DataSet();
            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajSveZahtevePoJMBG", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@JMBGKorisnika", SqlDbType.NVarChar).Value = jmbg;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public bool NoviZahtev(string jmbg, DateOnly ocekivaniDatumIzdavanja, int pttMesta)
        {
            //promenljiva za proveru uspesnosti unosa 
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("NoviZahtev", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@DatumIVremeZahteva", SqlDbType.DateTime).Value = DateTime.Now;
            Komanda.Parameters.Add("@JMBGKorisnika", SqlDbType.NVarChar).Value = jmbg;
            DateTime ocekivaniDatumIzdavanjaSaVremenom = ocekivaniDatumIzdavanja.ToDateTime(TimeOnly.MinValue);
            Komanda.Parameters.Add("@OcekivaniDatumIzdavanja", SqlDbType.Date).Value = ocekivaniDatumIzdavanjaSaVremenom;
            Komanda.Parameters.Add("@PTTMestaPU", SqlDbType.Int).Value = pttMesta;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            //vraca true ako je uspesno
            return (proveraUnosa > 0);
        }

        public bool ObrisiZahtev(int IDZahteva)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();

            SqlCommand Komanda = new SqlCommand("ObrisiZahtev", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IDZahteva", SqlDbType.Int).Value = IDZahteva;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);
        }

        //status zahteva stavlja na 1 - odbijeno
        public bool OdbijZahtev(int IDZahteva)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();

            SqlCommand Komanda = new SqlCommand("OdbijZahtev", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IDZahteva", SqlDbType.Int).Value = IDZahteva;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);
        }

        //status zahteva stavlja na 3 - odobreno
        public bool OdobriZahtev(int IDZahteva)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();

            SqlCommand Komanda = new SqlCommand("OdobriZahtev", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IDZahteva", SqlDbType.Int).Value = IDZahteva;

            proveraUnosa = Komanda.ExecuteNonQuery();

            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);
        }

        public bool IzmeniZahtev(int idZahteva, DateTime ocekivaniDatum, int mestoPU, int status)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();

            SqlCommand Komanda = new SqlCommand("IzmeniZahtev", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IDZahteva", SqlDbType.Int).Value = idZahteva;
            Komanda.Parameters.Add("@OcekivaniDatum", SqlDbType.Date).Value = ocekivaniDatum;
            Komanda.Parameters.Add("@MestoPU", SqlDbType.Int).Value = mestoPU;
            Komanda.Parameters.Add("@Status", SqlDbType.Int).Value = status;

            proveraUnosa = Komanda.ExecuteNonQuery();

            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);
        }
    }
}
