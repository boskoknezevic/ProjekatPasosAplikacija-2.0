using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SlojPodataka
{
    // Klasa: clsKorisnik
    //
    // Odgovornosti:
    // - Čuva podatke o korisniku uključujući lične podatke i akreditive.
    // - Omogućava registraciju korisnika, prijavu i izmenu informacija o korisniku.
    // - Obezbeđuje informacije potrebne za autentifikaciju i autorizaciju korisnika.
    //
    // Saradnici:
    // - Zahtev: Razmenjuje podatke o korisniku za kreiranje i upravljanje zahtevima.
    // - Pasos: Razmenjuje podatke o korisniku za praćenje izdavanja pasoša.
    // - KorisnikController: Koristi clsKorisnik za implementaciju poslovne logike vezane za korisnike.

    [Table("KORISNIK")]
    public class clsKorisnik
    {
        [Key]
        [Required]
        [StringLength(13)]
        private string jmbg;

        [Required]
        [StringLength(20)]
        private string ime;

        [Required]
        [StringLength(40)]
        private string prezime;

        [Required]
        private DateOnly datumRodjenja;

        [Required]
        [StringLength(40)]
        private string adresa;

        [Required]
        [StringLength(15)]
        private string telefon;

        [Required]
        private int pttMesto;

        [Required]
        [StringLength(3)]
        private string idDrzavaRodjenja;

        [Required]
        [StringLength(3)]
        private string idDrzavaDrzavljanstva;

        [Required]
        [StringLength(20)]
        private string korisnickoIme;

        [Required]
        private int polKorisnika;

        [Required]
        [StringLength(40)]
        [EmailAddress]
        private string email;

        [Required]
        [StringLength(20)]
        private string lozinka;

        [Required]
        private int tipKorisnika;

        [NotMapped]
        public DateTime DatumRodjenjaDateTime
        {
            get => DatumRodjenja.ToDateTime(TimeOnly.MinValue);
            set => DatumRodjenja = DateOnly.FromDateTime(value);
        }

        // Properties
        public string Jmbg { get => jmbg; set => jmbg = value; }
        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public string Adresa { get => adresa; set => adresa = value; }
        public string Telefon { get => telefon; set => telefon = value; }
        public int PttMesto { get => pttMesto; set => pttMesto = value; }
        public string IdDrzavaRodjenja { get => idDrzavaRodjenja; set => idDrzavaRodjenja = value; }
        public string IdDrzavaDrzavljanstva { get => idDrzavaDrzavljanstva; set => idDrzavaDrzavljanstva = value; }
        public string KorisnickoIme { get => korisnickoIme; set => korisnickoIme = value; }
        public int PolKorisnika { get => polKorisnika; set => polKorisnika = value; }
        public string Email { get => email; set => email = value; }
        public string Lozinka { get => lozinka; set => lozinka = value; }
        public int TipKorisnika { get => tipKorisnika; set => tipKorisnika = value; }
        public DateOnly DatumRodjenja { get => datumRodjenja; set => datumRodjenja = value; }
    }
}
