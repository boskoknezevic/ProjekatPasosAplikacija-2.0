using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Interfejsi
{
    public interface IZahtevRepo
    {
        DataSet DajSveZahteve();
        DataSet DajSveZahtevePoStatusu(int status);
        bool NoviZahtev(string jmbg, DateOnly ocekivaniDatumIzdavanja, int pttMesta);
        bool ObrisiZahtev(int IDZahteva);
        bool OdbijZahtev(int IDZahteva);
        bool OdobriZahtev(int IDZahteva);
        public DataSet DajSveZahtevePoJMBG(string jmbg);
        bool IzmeniZahtev(int idZahteva, DateTime ocekivaniDatum, int mestoPU, int status);
    }
}
