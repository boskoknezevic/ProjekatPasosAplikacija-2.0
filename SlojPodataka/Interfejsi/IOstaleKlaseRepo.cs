using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Interfejsi
{
    public interface IOstaleKlaseRepo
    {
        DataSet TipKorisnikaIzlistaj();
        DataSet TipStatusaIzlistaj();
        DataSet MestoIzlistaj();
        DataSet DrzavaIzlistaj();
        DataSet StatusDokumentaIzlistaj();
        DataSet PolKorisnikaIzlistaj();
    }
}
