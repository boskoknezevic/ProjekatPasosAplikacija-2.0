using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Interfejsi
{
    public interface IPasosRepo
    {
        DataSet DajSvePasose();
        DataSet DajPasosPoDatumu(DateOnly datum);
        bool NoviPasosITermin(string jmbg, DateOnly datum, TimeOnly vreme, int brojPasosa, int pttMesto);
        bool AžurirajStatusDokumentaPasosa(int brojPasosa, int status);
        bool ObrisiPasosITermin(int IDTermina);
        public DataSet DajPasosPoJMBG(string jmbg);
        bool DodajDatumePasosu(DateOnly datumPocetkaVazenja, DateOnly datumIsteka);
        bool IzmeniPasos(int brojPasosa, string IdDrzavaDrzavljanstva, string IdDrzavaRodjenja, int statusDokumentaId);
    }
}
