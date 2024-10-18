using AplikacioniSloj;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SlojPodataka;
using System.Data;

namespace PrezentacioniSloj.Controllers
{
    public class SluzbenikController : Controller
    {
        private readonly clsKorisnikServis _korisnikServis;
        private readonly clsZahtevServis _zahtevServis;
        private readonly clsPasosServis _pasosServis;
        private readonly clsTerminServis _terminServis;
        private readonly clsOstaleKlaseServis _ostaleKlaseServis;

        public SluzbenikController(clsKorisnikServis korisnikServis, clsZahtevServis zahtevServis, clsPasosServis pasosServis, clsOstaleKlaseServis ostaleKlaseServis, clsTerminServis terminServis)
        {
            _korisnikServis = korisnikServis;
            _zahtevServis = zahtevServis;
            _pasosServis = pasosServis;
            _ostaleKlaseServis = ostaleKlaseServis;
            _terminServis = terminServis;
        }

        public IActionResult SluzbenikPocetna()
        {
            string korisnickoIme = HttpContext.Session.GetString("KorisnickoIme");

            ViewData["KorisnickoIme"] = korisnickoIme;
            return View();
        }

        public IActionResult SluzbenikProfil()
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

        [HttpGet]
        public IActionResult SluzbenikPregledPasosa(string jmbg)
        {
            DataSet rezultat;
            if (string.IsNullOrEmpty(jmbg))
            {
                rezultat = _pasosServis.Prikazi();
            }
            else
            {
                rezultat = _pasosServis.PrikaziPoJMBG(jmbg);  
            }
            return View(rezultat);
        }

        public IActionResult SluzbenikPregledTermina()
        {
            var datumi = _terminServis.Izlistaj();

            ViewBag.ListaDatuma = datumi.Select(datum => new SelectListItem
            {
                Value = datum.ToString("yyyy-MM-dd"),
                Text = datum.ToString("dd/MM/yyyy")
            }).ToList();

            DataSet rezultat = _terminServis.Prikazi();
            return View(rezultat);
        }


        [HttpGet]
        public IActionResult SluzbenikPregledZahteva()
        {
             DataSet rezultat = _zahtevServis.Prikazi();
             return View(rezultat);
        }

        [HttpPost]
        public IActionResult UpravljajZahtevima(int IDZahteva, string action, string jmbg, int? pttMesto)
        {
            if (action == "odobri")
            {
                if(_zahtevServis.Odobri(IDZahteva, jmbg, (int)pttMesto)) TempData["SuccessMessage"] = "Успешно сте одобрили захтев!";
                else TempData["ErrorMessage"] = "Неуспешно одобравање захтева!";
            }
            else if (action == "odbij")
            {
                if (_zahtevServis.Odbij(IDZahteva)) TempData["SuccessMessage"] = "Успешно сте одбили захтев!";
                else TempData["ErrorMessage"] = "Неуспешно одбијање захтева!";
            }

            return RedirectToAction("SluzbenikPregledZahteva");
        }

        public IActionResult SluzbenikPasosDetalji()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IzmeniPasos(string? jmbg, string? action)
        {
            if (action == "izmeni")
            {
                DataSet pasos = _pasosServis.PrikaziPoJMBG(jmbg);
                var statusiDokumenta = _ostaleKlaseServis.StatusDokumentaIzlistaj();
                var statusiDokumentaTable = statusiDokumenta.Tables[0];
                var statusiDokumentaList = statusiDokumentaTable.AsEnumerable().Select(row => new
                {
                    ID = row.Field<int>("ID"),
                    Opis = row.Field<string>("Opis")
                }).ToList();
                ViewBag.StatusiDokumenta = statusiDokumentaList;

                return View("SluzbenikPasosDetalji", pasos);
            }
            else if (action == "stampaj")
            {
                DataSet pasos = _pasosServis.PrikaziPoJMBG(jmbg);
                return View("SluzbenikStampaPasos", pasos);
            }
            return RedirectToAction("SluzbenikPregledPasosa");
        }

        [HttpPost]
        public IActionResult IzmeniPodatke(int brojPasosa, int statusDokumenta)
        {
            if(_pasosServis.AzurirajStatusPasosa(brojPasosa, statusDokumenta)) TempData["SuccessMessage"] = "Успешно сте изменили податке!";
            else TempData["ErrorMessage"] = "Неуспешна промена података!";
            return RedirectToAction("SluzbenikPregledPasosa");
        }


        public IActionResult SluzbenikStampaPasos(string jmbg)
        {
            DataSet dataSet = _pasosServis.PrikaziPoJMBG(jmbg);
            if (dataSet != null)
            {
                return View("SluzbenikStampaPasos", dataSet);
            }
            else
            {
                TempData["ErrorMessage"] = "Немате пасош!";
                return RedirectToAction("SluzbenikPregledPasosa");
            }
        }

            [HttpPost]
        public IActionResult IzmeniKorisnickePodatke(RegistracijaModel model, string action)
        {
            if (action == "izmeni")
            {
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
                        return RedirectToAction("SluzbenikPocetna");
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
                    {
                        TempData["SUccessMessage"] = "Корисник је успешно обрисан!";
                        return RedirectToAction("Pocetna", "Home");
                    }
                        
                    return View();
                }
                return View();
            }
            return View();
        }
    }
}
