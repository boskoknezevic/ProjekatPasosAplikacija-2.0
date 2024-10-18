
using SlojPodataka;
using SlojPodataka.Interfejsi;
using System;
using System.Data;
using System.Xml.Linq;

namespace DomenskiSloj
{
    public class clsPoslovnaPravila
    {
        private IKorisnikRepo _repoKorisnik;
        private IZahtevRepo _repoZahtev;
        private ITerminRepo _repoTermin;
        private IPasosRepo _repoPasos;

        //konstruktor
        //dobija se string konekcije pri pozivanju
        public clsPoslovnaPravila(IKorisnikRepo repoKorisnik, IZahtevRepo repoZahtev, ITerminRepo repoTermin, IPasosRepo repoPasos)
        {
            _repoKorisnik = repoKorisnik;
            _repoZahtev= repoZahtev;
            _repoTermin = repoTermin;
            _repoPasos = repoPasos;
        }

        //proverava se da li moze korisnik da se unapredi u admina,
        //maks moze biti 2 admina (prvobitni iz baze + 1 unapredjeni)
        public bool ProveraMogucnostiDodeljivanjaUlogeSluzbenika()
        {
            bool proveraUspesnosti = false;
            // ucitavanje XML fajla
            XDocument doc = XDocument.Load(@"C:\Users\bosko\source\repos\ProjekatPasosAplikacija\XMLOgranicenja\MaksimumBrojSluzbenika.xml");

            // pronalaženje maksimalnog broja korisnika za tip 1
            XElement tipKorisnikaElement = doc.Descendants("tipKorisnika").FirstOrDefault(e => e.Attribute("id")?.Value == "2");

            int maksimum = Convert.ToInt32(tipKorisnikaElement.Attribute("maksimum")?.Value);

            DataSet korisnici = _repoKorisnik.DajSveKorisnike();
            

            int? brojPostojecihSluzbenika = korisnici.Tables[0]
            .AsEnumerable()
            .Count(row => row.Field<int>("TipKorisnikaID") == 2);

            if (brojPostojecihSluzbenika != null && brojPostojecihSluzbenika < maksimum)
            {
                proveraUspesnosti = true;
            }

            return proveraUspesnosti;
        }

        //provera da li korisnik moze poslati zahtev,
        //jer u slucaju da jos nije poslao ili je odbijen on opet moze da posalje
        public bool ProveraZahteva(string jmbg)
        {
            bool proveraUspesnosti = false;
            DataSet dsPodaci = _repoZahtev.DajSveZahteve();
            if (dsPodaci != null)
            {var rezultat = from DataRow row in dsPodaci.Tables[0].AsEnumerable()
                               where row.Field<string>("JMBGKorisnika") == jmbg
                               select row;
                DataRow[] nizRedova = rezultat.ToArray();

                if (nizRedova.Length == 0)
                { proveraUspesnosti = true; }
                else
                {
                    DataRow najskorijiZahtev = nizRedova.OrderByDescending(row => row.Field<int>("IDZahteva")).First();
                    XDocument doc = XDocument.Load(@"C:\Users\bosko\source\repos\ProjekatPasosAplikacija\XMLOgranicenja\DozvoljeniStatusZahteva.xml");
                    int idOdbijenog = int.Parse(doc.Descendants("statusZahteva")
                        .FirstOrDefault(e => e.Attribute("opis")?.Value == "Odbijen")?.Attribute("id")?.Value);
                    int idNaCekanju = int.Parse(doc.Descendants("statusZahteva")
                        .FirstOrDefault(e => e.Attribute("opis")?.Value == "Na čekanju")?.Attribute("id")?.Value);
                    int idOdobrenog = int.Parse(doc.Descendants("statusZahteva")
                        .FirstOrDefault(e => e.Attribute("opis")?.Value == "Odobren")?.Attribute("id")?.Value);
                    int statusId = najskorijiZahtev.Field<int>("StatusID");
                    if (statusId == idOdbijenog)
                    { proveraUspesnosti = true; }
                    else if (statusId == idNaCekanju || statusId == idOdobrenog)
                    {proveraUspesnosti = false;}
                }
            }
            return proveraUspesnosti;
        }



        //kad se prihvati zahtev, treba da se izracuna kad moze da se zakaze termin
        public DateTime ZakazivanjeTermina(int IDZahteva)
        {
            DataSet sviZahtevi = _repoZahtev.DajSveZahteve();
            DataRow zahtev = sviZahtevi.Tables[0].AsEnumerable()
                .FirstOrDefault(row => row.Field<int>("IDZahteva") == IDZahteva);
            DateOnly ocekivaniDatumIzdavanja = DateOnly.FromDateTime(zahtev.Field<DateTime>("OcekivaniDatumIzdavanja"));
            DateOnly noviTerminDatum;
            TimeOnly noviTerminVreme;
            DataSet sviTermini = _repoTermin.DajSveTermine();
            if (sviTermini.Tables.Count > 0 && sviTermini.Tables[0].Rows.Count > 0)
            {
                DataRow naskorijiTermin = sviTermini.Tables[0].AsEnumerable()
                    .OrderByDescending(row => row.Field<int>("IDTermina"))
                    .First();
                TimeSpan vremeNajskorijegTermina = naskorijiTermin.Field<TimeSpan>("Vreme");
                TimeOnly timeOnlyVremeNajskorijegTermina = TimeOnly.FromTimeSpan(vremeNajskorijegTermina);
                DateTime datumNajskorijegTermina = naskorijiTermin.Field<DateTime>("Datum");
                DateOnly dateOnlyDatumNajskorijegTermina = DateOnly.FromDateTime(datumNajskorijegTermina.Date);
                DateTime trenutniDatumIVreme = DateTime.Now;
                DateTime poslednjiTerminDatumIVreme = dateOnlyDatumNajskorijegTermina.ToDateTime(timeOnlyVremeNajskorijegTermina);
                if (poslednjiTerminDatumIVreme < trenutniDatumIVreme)
                { noviTerminDatum = ocekivaniDatumIzdavanja;}
                else
                { if (timeOnlyVremeNajskorijegTermina == TimeOnly.Parse("13:45"))
                    {   noviTerminDatum = dateOnlyDatumNajskorijegTermina.AddDays(1);
                        if (noviTerminDatum.DayOfWeek == DayOfWeek.Saturday)
                        { noviTerminDatum = noviTerminDatum.AddDays(2);}
                        noviTerminVreme = new TimeOnly(8, 0); }
                    else
                    {   noviTerminDatum = dateOnlyDatumNajskorijegTermina;
                        noviTerminVreme = timeOnlyVremeNajskorijegTermina.AddMinutes(15);}
                }
            }
            else
            {
                noviTerminDatum = ocekivaniDatumIzdavanja;
                 if (noviTerminDatum.DayOfWeek == DayOfWeek.Saturday)
                {noviTerminDatum = noviTerminDatum.AddDays(2); }
                else if (noviTerminDatum.DayOfWeek == DayOfWeek.Sunday)
                { noviTerminDatum = noviTerminDatum.AddDays(1);}
                noviTerminVreme = new TimeOnly(8, 0);
            }
            DateTime datumIVreme = noviTerminDatum.ToDateTime(noviTerminVreme);
            return datumIVreme;
        }

        public bool ProveraMogucnostiIzdavanjaPasosaPremaDrzavljanstvu(string jmbg)
        {
            var gradjaninPoJmbg = _repoKorisnik.DajKorisnikaPoJMBG(jmbg);
            var gradjanin = gradjaninPoJmbg.Tables[0];
            var drzavljanin = gradjanin.Rows[0];
            var idDrzavaDrzavljanstva = drzavljanin["IDDrzavaDrzavljanstva"].ToString();
            return idDrzavaDrzavljanstva == "SRB";
        }

        public DateOnly IzracunavanjeOcekivanogDatumaIzdavanja() 
        {
            string xmlFilePath = @"C:\Users\bosko\source\repos\ProjekatPasosAplikacija\XMLOgranicenja\OcekivaniBrojDanaZaCekanje.xml";
            XDocument xmlDoc = XDocument.Load(xmlFilePath);

            var daniAttribute = xmlDoc.Root.Element("ocekivaniBrojDanaZaCekanje").Attribute("dani");
            int dani = int.Parse(daniAttribute.Value);

            DateOnly datum = DateOnly.FromDateTime(DateTime.Now);

            DateOnly noviDatum = datum.AddDays(dani);

            DayOfWeek danUNedelji = noviDatum.DayOfWeek;

            if (danUNedelji == DayOfWeek.Saturday)
            {
                noviDatum = noviDatum.AddDays(2);
            }
            else if (danUNedelji == DayOfWeek.Sunday)
            {
                noviDatum = noviDatum.AddDays(1);
            }
            return noviDatum; 
        }

        public int GenerisanjeBrojaPasosa()
        {
            DataSet dsPasosi = _repoPasos.DajSvePasose();
            List<int> postojeciBrojeviPasosa;
            if (dsPasosi == null || dsPasosi.Tables.Count == 0 || dsPasosi.Tables[0].Rows.Count == 0)
            { postojeciBrojeviPasosa = new List<int>();}
            else
            {
                postojeciBrojeviPasosa = new List<int>();
                var rezultat = from DataRow row in dsPasosi.Tables[0].AsEnumerable()
                               select row.Field<int>("BrojPasosa");
                postojeciBrojeviPasosa.AddRange(rezultat);
            }
            var random = new Random();
            int brojPasosa;
            do
            { brojPasosa = random.Next(100000000, 1000000000);
            } while (postojeciBrojeviPasosa.Contains(brojPasosa));
            return brojPasosa;
        }

        public DateOnly IzracunavanjeDatumaIsteka(string jmbg, DateOnly datum)
        {
            char cifra1;
            char cifra2 = jmbg[4];
            char cifra3 = jmbg[5];
            char cifra4 = jmbg[6];
            if (cifra2 == '9')
            { cifra1 = '1';}
            else
            { cifra1 = '2'; }
            string godisteUStringu = new string(new[] { cifra1, cifra2, cifra3, cifra4 });
            int godiste = int.Parse(godisteUStringu);
            int trenutnaGodina = DateTime.Now.Year;
            if ((trenutnaGodina - godiste) >= 18)
            { return datum.AddYears(10);}
            else
            { return datum.AddYears(5);}
        }

        public string KreirajKorisnickoIme(string email)
        {
            string[] delovi = email.Split('@');
            return delovi[0];
        }
    }
}