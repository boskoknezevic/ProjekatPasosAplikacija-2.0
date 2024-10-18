using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SlojPodataka
{
    // Klasa: clsZahtevZaIzdavanjePasosa
    //
    // Odgovornosti:
    // - Prati podatke o zahtevu, uključujući datum i vreme zahteva.
    // - Proverava i prati status zahteva.
    // - Povezuje se sa korisnikom (JMBG korisnika).

    [Table("ZAHTEVZAIZDAVANJEPASOSA")]
    public class clsZahtevZaIzdavanjePasosa
    {
        [Key]
        private int idZahteva;

        [Required]
        private DateTime datumiVremeZahteva;

        [Required]
        [StringLength(13)]
        private string jmbgKorisnika;

        [Required]
        private DateTime ocekivaniDatumIzdavanja;

        [Required]
        private int pttMestaPU;

        [Required]
        private int status;

        // Properties
        public int IdZahteva { get => idZahteva; set => idZahteva = value; }
        public DateTime DatumiVremeZahteva { get => datumiVremeZahteva; set => datumiVremeZahteva = value; }
        public string JmbgKorisnika { get => jmbgKorisnika; set => jmbgKorisnika = value; }
        public DateTime OcekivaniDatumIzdavanja { get => ocekivaniDatumIzdavanja; set => ocekivaniDatumIzdavanja = value; }
        public int PttMestaPU { get => pttMestaPU; set => pttMestaPU = value; }
        public int Status { get => status; set => status = value; }
    }
}
