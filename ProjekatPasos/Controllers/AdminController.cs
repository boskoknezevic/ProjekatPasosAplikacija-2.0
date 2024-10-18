using AplikacioniSloj;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SlojPodataka;
using System.Data;

namespace PrezentacioniSloj.Controllers
{
    public class AdminController : Controller
    {
        private readonly clsKorisnikServis _korisnikServis;
        private readonly clsPasosServis _pasosServis;
        private readonly clsTerminServis _terminServis;
        private readonly clsZahtevServis _zahtevServis;
        private readonly clsOstaleKlaseServis _ostaleKlaseServis;

        public AdminController(clsKorisnikServis korisnikServis, clsPasosServis pasosServis, clsTerminServis terminServis, clsZahtevServis zahtevServis, clsOstaleKlaseServis ostaleKlaseServis)
        {
            _korisnikServis = korisnikServis;
            _pasosServis = pasosServis;
            _terminServis = terminServis;
            _zahtevServis = zahtevServis;
            _ostaleKlaseServis = ostaleKlaseServis;
        }

        public IActionResult AdminPocetna()
        {
            string korisnickoIme = HttpContext.Session.GetString("KorisnickoIme");
            ViewData["KorisnickoIme"] = korisnickoIme;
            return View();
        }

        [HttpGet]
        public IActionResult AdminPregledKorisnika(string prezime)
        {
            DataSet rezultat;

            if (!string.IsNullOrEmpty(prezime))
            {
                rezultat = _korisnikServis.Prikazi(prezime);
            }
            else
            {
                rezultat = _korisnikServis.Prikazi();
            }

            return View(rezultat);
        }

        [HttpPost]
        public IActionResult IzmeniKorisnika(string? email, string? jmbg, string? action)
        {
            if (action == "izmeni")
            {
                clsKorisnik korisnik = _korisnikServis.PronadjiPoEmail(email);
                var mesta = _ostaleKlaseServis.MestoIzlistaj();
                var drzave = _ostaleKlaseServis.DrzavaIzlistaj();
                var polKorisnika = _ostaleKlaseServis.PolKorisnikaIzlistaj();

                // Check if tables are not null and have data
                if (mesta.Tables.Count > 0 && drzave.Tables.Count > 0 && polKorisnika.Tables.Count > 0)
                {
                    var mestaTable = mesta.Tables[0];
                    var drzaveTable = drzave.Tables[0];
                    var polKorisnikaTable = polKorisnika.Tables[0];

                    var mestaList = mestaTable.AsEnumerable().Select(row => new
                    {
                        PTT = row.Field<int>("PTT"),
                        Naziv = row.Field<string>("Naziv")
                    }).ToList();

                    var drzaveList = drzaveTable.AsEnumerable().Select(row => new
                    {
                        SifraDrzave = row.Field<string>("SifraDrzave"),
                        Naziv = row.Field<string>("Naziv")
                    }).ToList();

                    var polKorisnikaList = polKorisnikaTable.AsEnumerable().Select(row => new
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

                return View("AdminPregledKorisnikaDetalji", korisnik);
            }
            else if (action == "obrisi")
            {
                _korisnikServis.Obrisi(jmbg);
                TempData["SUccessMessage"] = "Корисник је успешно обрисан!";
            }
            else if (action == "dodeliSluzbenikUlogu")
            {
                clsKorisnik korisnik = _korisnikServis.PronadjiPoEmail(email);
                _korisnikServis.DodeliSluzbenikUlogu(korisnik);
                TempData["SUccessMessage"] = "Кориснику је успешно измењена улога!";
            }

            return RedirectToAction("AdminPregledKorisnika");
        }

        public IActionResult AdminPregledKorisnikaDetalji()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IzmeniPodatke(string action, clsKorisnik model, string stariJmbg)
        {
            if (action == "izmeni")
            {
                _korisnikServis.Izmeni(stariJmbg, model);
                TempData["SUccessMessage"] = "Корисник је успешно измењен!";

                return RedirectToAction("AdminPocetna");
            }
            return View();
        }

        [HttpGet]
        public IActionResult AdminPregledPasosa(string jmbg)
        {
            if (string.IsNullOrEmpty(jmbg))
            {
                DataSet rezultat = _pasosServis.Prikazi();
                return View(rezultat);
            }
            else
            {
                DataSet rezultat = _pasosServis.PrikaziPoJMBG(jmbg);
                return View(rezultat);
            }
        }

        [HttpPost]
        public IActionResult IzmeniPasos(string? email, string? jmbgKorisnika, string? action, int? brojPasosa)
        {
            if (action == "izmeni")
            {
                DataSet pasos = _pasosServis.PrikaziPoJMBG(jmbgKorisnika);
                var drzave = _ostaleKlaseServis.DrzavaIzlistaj();
                var statusiDokumenta = _ostaleKlaseServis.StatusDokumentaIzlistaj();

                // Check if tables are not null and have data
                if (drzave.Tables.Count > 0 && statusiDokumenta.Tables.Count > 0)
                {
                    var drzaveTable = drzave.Tables[0];
                    var statusiDokumentaTable = statusiDokumenta.Tables[0];

                    var drzaveList = drzaveTable.AsEnumerable().Select(row => new
                    {
                        SifraDrzave = row.Field<string>("SifraDrzave"),
                        Naziv = row.Field<string>("Naziv")
                    }).ToList();

                    var statusiDokumentaList = statusiDokumentaTable.AsEnumerable().Select(row => new
                    {
                        ID = row.Field<int>("ID"),
                        Opis = row.Field<string>("Opis")
                    }).ToList();

                    ViewBag.ListaDrzava = drzaveList;
                    ViewBag.StatusiDokumenta = statusiDokumentaList;
                }
                else
                {
                    // Handle the case where data is not available
                    ViewBag.ListaDrzava = new List<SelectListItem>();
                    ViewBag.StatusiDokumenta = new List<SelectListItem>();
                }

                return View("AdminPregledPasosaDetalji", pasos);
            }
            else if (action == "obrisi")
            {
                var termini = _terminServis.Prikazi().Tables[0];
                DataRow[] terminiRows = termini.Select($"BrojPasosa = {brojPasosa}");

                DataRow terminRow = terminiRows.FirstOrDefault();
                int idTermina = terminRow.Field<int>("IDTermina");
                _pasosServis.Obrisi(idTermina);
                TempData["SUccessMessage"] = "Пасош је успешно обрисан!";
            }

            return RedirectToAction("AdminPregledPasosa");
        }

        public IActionResult AdminPregledPasosaDetalji()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IzmeniPodatkePasosa(int brojPasosa, string drzavaRodjenja, string drzavaDrzavljanstva, int statusDokumenta)
        {
            _pasosServis.Izmeni(brojPasosa, drzavaDrzavljanstva, drzavaRodjenja, statusDokumenta);
            TempData["SUccessMessage"] = "Пасош је успешно измењен!";
            return RedirectToAction("AdminPocetna");
        }



        public IActionResult AdminPregledTermina(string datum)
        {
            DataSet rezultat = _terminServis.Prikazi();

            ViewBag.ListaDatuma = new SelectList(_terminServis.Izlistaj());

            if (string.IsNullOrEmpty(datum))
            {
                return View(rezultat);
            }
            else if (DateOnly.TryParse(datum, out DateOnly izabraniDatum))
            {
                DataTable filteredTable = rezultat.Tables[0].Clone();

                foreach (DataRow row in rezultat.Tables[0].Rows)
                {
                    if (row["Datum"] is DateTime rowDatum && rowDatum.Date == izabraniDatum.ToDateTime(TimeOnly.MinValue).Date)
                    {
                        filteredTable.ImportRow(row);
                    }
                }

                ViewBag.IzabraniDatum = izabraniDatum;
                rezultat.Tables.Remove(rezultat.Tables[0]);
                rezultat.Tables.Add(filteredTable);

                return View(rezultat);
            }
            else
            {
                // Handle nevalidan datum
                return BadRequest("Невалидан датум!");
            }
        }

        [HttpPost]
        public IActionResult IzmeniTermin(int? idTermina, string? action, int? brojPasosa)
        {
            if (action == "izmeni")
            {
                var termin = _terminServis.Prikazi((int)idTermina);
                return View("AdminPregledTerminaDetalji", termin);
            }
            else if (action == "obrisi")
            {
                _pasosServis.Obrisi((int)idTermina);
                TempData["SUccessMessage"] = "Термин је успешно обрисан!";
            }

            return RedirectToAction("AdminPregledTermina");
        }

        public IActionResult AdminPregledTerminaDetalji()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IzmeniPodatkeTermina(int idTermina, DateTime datum, DateTime vreme)
        {
            DateOnly datumBezVremena = DateOnly.FromDateTime(datum);
            TimeOnly vremeBezDatuma = TimeOnly.FromDateTime(vreme);
            _terminServis.Izmeni(idTermina, datumBezVremena, vremeBezDatuma);
            TempData["SUccessMessage"] = "Термин је успешно измењен!";
            return RedirectToAction("AdminPocetna");
        }


        [HttpGet]
        public IActionResult AdminPregledZahteva(int status)
        {
            var statusi = _ostaleKlaseServis.TipStatusaIzlistaj();

            if (statusi.Tables.Count > 0)
            {
                var statusiTable = statusi.Tables[0];

                var statusiList = statusiTable.AsEnumerable().Select(row => new
                {
                    ID = row.Field<int>("ID"),
                    Opis = row.Field<string>("Opis")
                }).ToList();

                ViewBag.TipoviStatusa = statusiList;
            }
            else
            {
                ViewBag.TipoviStatusa = new List<SelectListItem>();
            }
            DataSet rezultat;
            if (status == 0)
            {
                rezultat = _zahtevServis.Prikazi();
            } 
            else 
            {
                rezultat = _zahtevServis.Prikazi(status);
                
            }
            return View(rezultat);
        }

        [HttpPost]
        public IActionResult IzmeniZahtev(string? jmbg, string? action, int? idZahteva)
        {
            if (action == "izmeni")
            {
                var statusiZahteva = _ostaleKlaseServis.TipStatusaIzlistaj();
                var mesta = _ostaleKlaseServis.MestoIzlistaj();

                // Check if tables are not null and have data
                if (statusiZahteva.Tables.Count > 0 && mesta.Tables.Count > 0)
                {
                    var statusiZahtevaTable = statusiZahteva.Tables[0];
                    var mestaTable = mesta.Tables[0];

                    var statusiZahtevaList = statusiZahtevaTable.AsEnumerable().Select(row => new
                    {
                        ID = row.Field<int>("ID"),
                        Opis = row.Field<string>("Opis")
                    }).ToList();

                    var mestaList = mestaTable.AsEnumerable().Select(row => new
                    {
                        PTT = row.Field<int>("PTT"),
                        Naziv = row.Field<string>("Naziv")
                    }).ToList();

                    ViewBag.StatusiZahteva = statusiZahtevaList;
                    ViewBag.ListaMesta = mestaList;
                }
                else
                {
                    // Handle the case where data is not available
                    ViewBag.StatusiZahteva = new List<SelectListItem>();
                    ViewBag.ListaMesta = new List<SelectListItem>();
                }

                var zahtev = _zahtevServis.PrikaziPoJMBG(jmbg);
                return View("AdminPregledZahtevaDetalji", zahtev);
            }
            else if (action == "obrisi")
            {
                _zahtevServis.Obrisi((int)idZahteva);
                TempData["SUccessMessage"] = "Захтев је успешно обрисан!";
            }

            return RedirectToAction("AdminPregledTermina");
        }

        public IActionResult AdminPregledZahtevaDetalji()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IzmeniPodatkeZahteva(int idZahteva, DateTime? ocekivaniDatum, int? mestoPU, int? status)
        {
            _zahtevServis.Izmeni(idZahteva, (DateTime)ocekivaniDatum, (int)mestoPU, (int)status);
            TempData["SUccessMessage"] = "Захтев је успешно измењен!";
            return RedirectToAction("AdminPocetna");
        }

    }
}
