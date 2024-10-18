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
    public class clsZahtevServis
    {
        private IZahtevRepo _repoZahtev;
        private IPasosRepo _repoPasos;
        private clsPoslovnaPravila _poslovnaPravila;

        //konstruktor
        public clsZahtevServis(IZahtevRepo repo, IPasosRepo pasosRepo, clsPoslovnaPravila poslovnaPravila)
        {
            _repoZahtev = repo;
            _poslovnaPravila = poslovnaPravila;
            _repoPasos = pasosRepo;
        }

        public DataSet Prikazi()
        {
            return _repoZahtev.DajSveZahteve();
        }

        public DataSet Prikazi(int status)
        {
            return _repoZahtev.DajSveZahtevePoStatusu(status);

        }

        public (bool, string) Dodaj(string jmbg, int pttMesto)
        {
            if (!_poslovnaPravila.ProveraZahteva(jmbg)) { return (false, "Већ сте поднели захтев!"); } 
            if(!_poslovnaPravila.ProveraMogucnostiIzdavanjaPasosaPremaDrzavljanstvu(jmbg)) { return (false, "Не можете поднети захтев за пасош пошто нисте држављанин Републике Србије"); }
            
            DateOnly ocekivaniDatum = _poslovnaPravila.IzracunavanjeOcekivanogDatumaIzdavanja();
            return (_repoZahtev.NoviZahtev(jmbg, ocekivaniDatum, pttMesto), "Успешно сте поднели захтев!");
        }

        public bool Obrisi(int IDZahteva)
        {
           return _repoZahtev.ObrisiZahtev(IDZahteva);
        }

        public bool Odbij(int IDZahteva)
        {
            return _repoZahtev.OdbijZahtev(IDZahteva);
        }

        public bool Odobri(int IDZahteva, string jmbg, int pttMesto)
        {
            if (_repoZahtev.OdobriZahtev(IDZahteva))
            {
                DateTime datumIVreme = _poslovnaPravila.ZakazivanjeTermina(IDZahteva);
                DateOnly datum = DateOnly.FromDateTime(datumIVreme);
                TimeOnly vreme = TimeOnly.FromDateTime(datumIVreme);
                DateOnly datumIsteka = _poslovnaPravila.IzracunavanjeDatumaIsteka(jmbg, datum);
                int brojPasosa = _poslovnaPravila.GenerisanjeBrojaPasosa();
                if (_repoPasos.NoviPasosITermin(jmbg, datum, vreme, brojPasosa, pttMesto))
                { return true; }
                else return false; 
            }
            else
                return false; 
        }

        public DataSet PrikaziPoJMBG(string jmbg)
        {
            return _repoZahtev.DajSveZahtevePoJMBG(jmbg);
        }

        public bool Izmeni(int idZahteva, DateTime ocekivaniDatum, int mestoPU, int status)
        {
            return _repoZahtev.IzmeniZahtev(idZahteva, ocekivaniDatum, mestoPU, status);
        }
    }
}
