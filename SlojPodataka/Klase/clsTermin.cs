using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SlojPodataka
{
    // Class: clsTermin
    //
    // Odgovornosti:
    // - Prati podatke o terminu (ID, datum i vreme).
    // - Omogućava pregled i odabir dostupnih termina za preuzimanje pasoša.
    //
    // Saradnici:
    // - Sa klasom Pasos (povezivanje sa brojem pasoša).

    [Table("TERMIN")]
    public class clsTermin
    {
        [Key]
        [Required]
        private int idTermina;

        [Required]
        private int brojPasosa;

        [Required]
        private DateOnly datum;

        [Required]
        private TimeOnly vreme;

        // Properties
        public int IdTermina { get => idTermina; set => idTermina = value; }
        public int BrojPasosa { get => brojPasosa; set => brojPasosa = value; }
        public DateOnly Datum { get => datum; set => datum = value; }
        public TimeOnly Vreme { get => vreme; set => vreme = value; }
    }
}
