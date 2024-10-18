using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SlojPodataka
{
    // Klasa: clsPasos
    //
    // Odgovornosti:
    // - Prati podatke o pasošu, uključujući informacije o korisniku i terminu.
    // - Obezbeđuje podatke o pasošu kao što su broj pasoša, datum važenja itd.
    //
    // Saradnici:
    // - Korisnik: Povezuje se sa JMBG korisnika.
    // - Termin: Povezuje se sa ID termina.

    [Table("PASOS")]
    public class clsPasos
    {
        [Key]
        private int brojPasosa;

        [Required]
        [StringLength(13)]
        private string jmbgKorisnika;

        [Required]
        [StringLength(20)]
        private string ime;

        [Required]
        [StringLength(40)]
        private string prezime;

        [Required]
        private int pttMesto;

        [Required]
        [StringLength(3)]
        private string idDrzavaRodjenja;

        [Required]
        [StringLength(3)]
        private string idDrzavaDrzavljanstva;

        [Required]
        private int pttMestaPU;

        [Required]
        private DateTime? datumPocetkaVazenja;

        [Required]
        private DateTime? datumIsteka;

        // Properties
        public int BrojPasosa { get => brojPasosa; set => brojPasosa = value; }
        public string JmbgKorisnika { get => jmbgKorisnika; set => jmbgKorisnika = value; }
        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public int PttMesto { get => pttMesto; set => pttMesto = value; }
        public string IdDrzavaRodjenja { get => idDrzavaRodjenja; set => idDrzavaRodjenja = value; }
        public string IdDrzavaDrzavljanstva { get => idDrzavaDrzavljanstva; set => idDrzavaDrzavljanstva = value; }
        public int PttMestaPU { get => pttMestaPU; set => pttMestaPU = value; }
        public DateTime? DatumPocetkaVazenja { get => datumPocetkaVazenja; set => datumPocetkaVazenja = value; }
        public DateTime? DatumIsteka { get => datumIsteka; set => datumIsteka = value; }
    }
}
