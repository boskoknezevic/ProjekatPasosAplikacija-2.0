using SlojPodataka.Interfejsi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikacioniSloj
{
    public class clsOstaleKlaseServis
    {
        private IOstaleKlaseRepo _repo;
        public clsOstaleKlaseServis(IOstaleKlaseRepo repo) 
        {
            _repo = repo;
        }

        public DataSet TipKorisnikaIzlistaj()
        {
            return _repo.TipKorisnikaIzlistaj();
        }

        public DataSet TipStatusaIzlistaj()
        {
            return _repo.TipStatusaIzlistaj();
        }

        public DataSet MestoIzlistaj()
        {
            return _repo.MestoIzlistaj();
        }

        public DataSet DrzavaIzlistaj()
        {
            return _repo.DrzavaIzlistaj();
        }

        public DataSet PolKorisnikaIzlistaj()
        {
            return _repo.PolKorisnikaIzlistaj();
        }

        public DataSet StatusDokumentaIzlistaj()
        {
            return _repo.StatusDokumentaIzlistaj();
        }
    }
}
