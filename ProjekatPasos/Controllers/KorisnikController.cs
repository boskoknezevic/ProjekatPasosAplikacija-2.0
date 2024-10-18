using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System;
using AplikacioniSloj;
using SlojPodataka;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PrezentacioniSloj.Controllers
{
    public class KorisnikController : Controller
    {

        private readonly clsKorisnikServis _korisnikServis;
        private readonly clsZahtevServis _zahtevServis;
        private readonly clsPasosServis _pasosServis;
        private readonly clsOstaleKlaseServis _ostaleKlaseServis;

        public KorisnikController(clsKorisnikServis korisnikServis, clsZahtevServis zahtevServis, clsPasosServis pasosServis, clsOstaleKlaseServis ostaleKlaseServis)
        {
            _korisnikServis = korisnikServis;
            _zahtevServis = zahtevServis;
            _pasosServis = pasosServis;
            _ostaleKlaseServis = ostaleKlaseServis;
        }
        public IActionResult KorisnikPocetna()
        {
            string korisnickoIme = HttpContext.Session.GetString("KorisnickoIme");

            ViewData["KorisnickoIme"] = korisnickoIme;
            return View();
        }

        public IActionResult KorisnikProfil()
        {
            // Dobijanje podataka iz sesije
            var jmbg = HttpContext.Session.GetString("JMBG");
            var ime = HttpContext.Session.GetString("Ime");
            var prezime = HttpContext.Session.GetString("Prezime");
            var lozinka = HttpContext.Session.GetString("Lozinka");
            var email = HttpContext.Session.GetString("Email");
            var telefon = HttpContext.Session.GetString("Telefon");
            var adresa = HttpContext.Session.GetString("Adresa");
            var idDrzavaDrzavljanstva = HttpContext.Session.GetString("IDDrzavaDrzavljanstva");
            var idDrzavaRodjenja = HttpContext.Session.GetString("IDDrzavaRodjenja");
            var polKorisnika = HttpContext.Session.GetInt32("PolKorisnika");
            var pttMesto = HttpContext.Session.GetInt32("PTTMesto");
            var datumRodjenja = HttpContext.Session.GetString("DatumRodjenja");
            var korisnickoIme = HttpContext.Session.GetString("KorisnickoIme");

            // Kreiranje modela sa podacima iz sesije
            var model = new RegistracijaModel
            {
                JMBG = jmbg,
                Ime = ime,
                Prezime = prezime,
                Email = email,
                Lozinka = lozinka,
                Telefon = telefon,
                Adresa = adresa,
                IDDrzavaDrzavljanstva = idDrzavaDrzavljanstva,
                IDDrzavaRodjenja = idDrzavaRodjenja,
                PolKorisnika = (int)polKorisnika,
                PTTMesto = (int)pttMesto,
                DatumRodjenjaDateTime = DateTime.Parse(datumRodjenja),
                KorisnickoIme = korisnickoIme
            };

            // Fetch data
            var mesta = _ostaleKlaseServis.MestoIzlistaj();
            var drzave = _ostaleKlaseServis.DrzavaIzlistaj();
            var polovi = _ostaleKlaseServis.PolKorisnikaIzlistaj();

            // Check if tables are not null and have data
            if (mesta.Tables.Count > 0 && drzave.Tables.Count > 0 && polovi.Tables.Count > 0)
            {
                var mestaTable = mesta.Tables[0];
                var drzaveTable = drzave.Tables[0];
                var polTable = polovi.Tables[0];

                var mestaList = mestaTable.AsEnumerable().Select(row => new
                {
                    ID = row.Field<int>("PTT"),
                    Naziv = row.Field<string>("Naziv")
                }).ToList();

                var drzaveList = drzaveTable.AsEnumerable().Select(row => new
                {
                    SifraDrzave = row.Field<string>("SifraDrzave"),
                    Naziv = row.Field<string>("Naziv")
                }).ToList();

                var polKorisnikaList = polTable.AsEnumerable().Select(row => new
                {
                    ID = row.Field<int>("ID"),
                    Opis = row.Field<string>("Opis")
                }).ToList();

                // Assign lists directly to ViewBag
                ViewBag.Mesta = mestaList;
                ViewBag.Drzave = drzaveList;
                ViewBag.PolKorisnika = polKorisnikaList;
            }
            else
            {
                // Handle the case where data is not available
                ViewBag.Mesta = new List<SelectListItem>();
                ViewBag.Drzave = new List<SelectListItem>();
                ViewBag.PolKorisnika = new List<SelectListItem>();
            }

            return View(model);
        }


        public IActionResult KorisnikZahtev()
        {
            var mesta = _ostaleKlaseServis.MestoIzlistaj();

            if (mesta.Tables.Count > 0)
            {
                var mestaTable = mesta.Tables[0];

                var mestaList = mestaTable.AsEnumerable().Select(row => new SelectListItem
                {
                    Value = row.Field<int>("PTT").ToString(),
                    Text = row.Field<string>("Naziv")
                }).ToList();

                ViewBag.Mesta = mestaList;
            }
            else
            {
                ViewBag.Mesta = new List<SelectListItem>();
            }

            var jmbg = HttpContext.Session.GetString("JMBG");
            DataSet rezultat = _zahtevServis.PrikaziPoJMBG(jmbg);

            return View(rezultat);
        }


        [HttpPost]
        public IActionResult DodajZahtev(string action, int? pttMesta)
        {
            var jmbg = HttpContext.Session.GetString("JMBG");

            if (pttMesta == null) { pttMesta = HttpContext.Session.GetInt32("PTTMesto"); }
            (bool, string) odgovor = _zahtevServis.Dodaj(jmbg, (int)pttMesta);
            if (odgovor.Item1)
            {
                TempData["SuccessMessage"] = odgovor.Item2;
                return RedirectToAction("KorisnikPocetna");
            }
            else
            {
                TempData["ErrorMessage"] = odgovor.Item2;
                return RedirectToAction("KorisnikZahtev");
            }
        }

        [HttpPost]
        public IActionResult IzmeniPodatke(RegistracijaModel model, string action)
        {

            if (action == "izmeni")
            {
                // Dobavi JMBG korisnika iz sesije
                var jmbgIzSesije = HttpContext.Session.GetString("JMBG");

                if (!string.IsNullOrEmpty(jmbgIzSesije))
                {
                    clsKorisnik korisnik = new clsKorisnik();
                    korisnik.Jmbg = model.JMBG;
                    korisnik.Ime = model.Ime;
                    korisnik.Prezime = model.Prezime;
                    korisnik.Email = model.Email;
                    korisnik.Lozinka = model.Lozinka;
                    korisnik.Telefon = model.Telefon;
                    korisnik.Adresa = model.Adresa;
                    korisnik.IdDrzavaDrzavljanstva = model.IDDrzavaDrzavljanstva;
                    korisnik.IdDrzavaRodjenja = model.IDDrzavaRodjenja;
                    korisnik.PolKorisnika = model.PolKorisnika;
                    korisnik.PttMesto = model.PTTMesto;
                    korisnik.DatumRodjenja = model.DatumRodjenja;

                    if (_korisnikServis.Izmeni(jmbgIzSesije, korisnik))
                    {
                        TempData["SuccessMessage"] = "Корисник је успешно измењен!";
                        return RedirectToAction("KorisnikPocetna");
                    }
                    return View();
                }
                return View();

            }

            else if (action == "obrisi")
            {   
                var jmbg = HttpContext.Session.GetString("JMBG");

                if (!string.IsNullOrEmpty(jmbg))
                {
                    if (_korisnikServis.Obrisi(jmbg))
                        return RedirectToAction("Pocetna", "Home");
                    return View();
                }
                return View();
            }
            return View();
        }

        public IActionResult KorisnikDokumentStampa()
        {
            var jmbg = HttpContext.Session.GetString("JMBG");
            if (!string.IsNullOrEmpty(jmbg))
                { DataSet dataSet = _zahtevServis.PrikaziPoJMBG(jmbg);
                if (dataSet != null)
                {
                    var zahtevi = dataSet.Tables[0];
                    DataRow[] zahtev = zahtevi.Select($"StatusID = 3");
                    if(zahtev.Length == 0) 
                    {
                        TempData["ErrorMessage"] = "Немате документ за штампу";
                        return RedirectToAction("KorisnikPocetna"); 
                    }
                    DataRow zahtevZaView = zahtev[0];
                    return View("KorisnikDokumentStampa", zahtevZaView);
                }
                else return RedirectToAction("KorisnikProfil");
            }
            return RedirectToAction("KorisnikZahtev");
        }

        public IActionResult KorisnikPasos()
        {
            var jmbg = HttpContext.Session.GetString("JMBG");
            if (!string.IsNullOrEmpty(jmbg))
            {
                DataSet dataSet = _pasosServis.PrikaziPoJMBG(jmbg);
                if (dataSet != null)
                    return View("KorisnikPasos", dataSet);
                else return RedirectToAction("KorisnikProfil");
            }
            return RedirectToAction("KorisnikZahtev");
        }
    }
}
