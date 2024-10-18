using AplikacioniSloj;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SlojPodataka;
using System.Data;

public class NalogController : Controller
{
    private readonly clsKorisnikServis _korisnikServis;
    private readonly clsOstaleKlaseServis _ostaleKlaseServis;

    public NalogController(clsKorisnikServis korisnikServis, clsOstaleKlaseServis ostaleKlaseServis)
    {
        _korisnikServis = korisnikServis;
        _ostaleKlaseServis = ostaleKlaseServis;
    }

    [HttpGet]
    public IActionResult Registracija()
    {
        // Fetch data
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

        return View();
    }



    [HttpPost]
    public ActionResult Registracija(RegistracijaModel model)
    {
        string korisnickoIme = _korisnikServis.KreirajKorisnickoIme(model.Email);
        model.KorisnickoIme = korisnickoIme;
        if (ModelState.IsValid)
        {
            bool uspesnaRegistracija = _korisnikServis.Dodaj(new clsKorisnik
            {
                Jmbg = model.JMBG,
                Ime = model.Ime,
                Prezime = model.Prezime,
                Email = model.Email,
                Lozinka = model.Lozinka,
                Telefon = model.Telefon,
                Adresa = model.Adresa,
                IdDrzavaDrzavljanstva = model.IDDrzavaDrzavljanstva,
                IdDrzavaRodjenja = model.IDDrzavaRodjenja,
                PolKorisnika = model.PolKorisnika,
                PttMesto = model.PTTMesto,
                DatumRodjenja = model.DatumRodjenja,
                KorisnickoIme = korisnickoIme
            });

            if (uspesnaRegistracija)
            {
                TempData["SuccessMessage"] = "Успешна регистрација!";
                return RedirectToAction("Prijava");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Регистрација није успешна. Молимо покушајте поново.");
            }
        }
        return View(model);
    }

    [HttpGet]
    public ActionResult Prijava()
    {
        return View();
    }
    [HttpPost]
    public ActionResult Prijava(PrijavaModel model)
    {
        if (ModelState.IsValid)
        {
            // Pozovite metodu iz servisa koja proverava korisničke podatke
            var prijavljeniKorisnik = _korisnikServis.PronadjiPoEmail(model.Email);

            if (prijavljeniKorisnik != null)
            {
                // Ako je pronađen korisnik sa datom e-poštom, proverite lozinku
                if (prijavljeniKorisnik.Lozinka == model.Lozinka)
                {
                    // Lozinka je ispravna, postavite korisničke podatke u sesiju
                    HttpContext.Session.SetString("JMBG", prijavljeniKorisnik.Jmbg);
                    HttpContext.Session.SetString("Ime", prijavljeniKorisnik.Ime);
                    HttpContext.Session.SetString("Prezime", prijavljeniKorisnik.Prezime);
                    HttpContext.Session.SetString("Email", prijavljeniKorisnik.Email);
                    HttpContext.Session.SetString("Lozinka", prijavljeniKorisnik.Lozinka);
                    HttpContext.Session.SetString("Telefon", prijavljeniKorisnik.Telefon);
                    HttpContext.Session.SetInt32("TipKorisnika", prijavljeniKorisnik.TipKorisnika);
                    HttpContext.Session.SetString("Adresa", prijavljeniKorisnik.Adresa);
                    HttpContext.Session.SetString("IDDrzavaDrzavljanstva", prijavljeniKorisnik.IdDrzavaDrzavljanstva);
                    HttpContext.Session.SetString("IDDrzavaRodjenja", prijavljeniKorisnik.IdDrzavaRodjenja);
                    HttpContext.Session.SetInt32("PolKorisnika", prijavljeniKorisnik.PolKorisnika);
                    HttpContext.Session.SetInt32("PTTMesto", prijavljeniKorisnik.PttMesto);
                    HttpContext.Session.SetString("DatumRodjenja", prijavljeniKorisnik.DatumRodjenja.ToString());
                    HttpContext.Session.SetString("KorisnickoIme", prijavljeniKorisnik.KorisnickoIme);

                    // Redirekcija na odgovarajući view u zavisnosti od tipa korisnika
                    if (prijavljeniKorisnik.TipKorisnika == 1)
                    {
                        return RedirectToAction("AdminPocetna", "Admin");
                    }
                    else if (prijavljeniKorisnik.TipKorisnika == 2)
                    {
                        return RedirectToAction("SluzbenikPocetna", "Sluzbenik");
                    }
                    else if (prijavljeniKorisnik.TipKorisnika == 3)
                    {
                        return RedirectToAction("KorisnikPocetna", "Korisnik");
                    }
                }
                else
                {
                    // Pogrešna lozinka
                    ModelState.AddModelError(string.Empty, "Погрешна лозинка");
                }
            }
            else
            {
                // Nema korisnika sa datom e-poštom
                ModelState.AddModelError(string.Empty, "Нема корисника у бази података са тим имејлом!");
            }
        }

        // Ako ModelState nije validan, vraća se isti view sa postojećim podacima
        return View(model);
    }

}
