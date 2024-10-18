using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlojPodataka.Interfejsi;
using DomenskiSloj;

namespace AplikacioniSloj
{
    public class clsPasosServis
    {
        private IPasosRepo _repo;
        private clsPoslovnaPravila _poslovnaPravila;

        //konstruktor
        public clsPasosServis(IPasosRepo repo, clsPoslovnaPravila poslovnaPravila)
        {
            _repo = repo;
            _poslovnaPravila = poslovnaPravila;
        }


        //izlistava sve pasose i daje njihov datum isteka,
        //ime i prezime korisnika kao i datum i vreme termina
        public DataSet Prikazi()
        {
            return _repo.DajSvePasose();
        }

        public DataSet PrikaziPoJMBG(string jmbg)
        {
            return _repo.DajPasosPoJMBG(jmbg);
        }

        //filtrira pasose po datumu
        public DataSet PrikaziPoDatumu(DateOnly datum)
        {
            return _repo.DajPasosPoDatumu(datum);
        }

        public bool AzurirajStatusPasosa(int brojPasosa, int status)
        {
            if (status == 4)
            {
                var pasosi = _repo.DajSvePasose();
                var pasos = pasosi.Tables[0];
                DataRow[] pronadjenPasos = pasos.Select($"BrojPasosa = '{brojPasosa}'");
                DataRow jmbg = pronadjenPasos[0];  // Prvi red koji odgovara kriterijumu
                string pronadjenJmbg = jmbg["JMBGKorisnika"].ToString();
                DateOnly datumPocetkaVazenja = DateOnly.FromDateTime(DateTime.Now);
                DateOnly datumIsteka = _poslovnaPravila.IzracunavanjeDatumaIsteka(pronadjenJmbg, datumPocetkaVazenja);
                return (_repo.DodajDatumePasosu(datumPocetkaVazenja, datumIsteka) && _repo.AžurirajStatusDokumentaPasosa(brojPasosa, status));
            }
            else return _repo.AžurirajStatusDokumentaPasosa(brojPasosa, status);
        }

        public bool Obrisi(int IDTermina)
        {
           return _repo.ObrisiPasosITermin(IDTermina);
        }

        public bool Izmeni(int brojPasosa, string IdDrzavaDrzavljanstva, string IdDrzavaRodjenja, int statusDokumentaID)
        {
            return _repo.IzmeniPasos(brojPasosa, IdDrzavaDrzavljanstva, IdDrzavaRodjenja, statusDokumentaID);
        }
    }
}
