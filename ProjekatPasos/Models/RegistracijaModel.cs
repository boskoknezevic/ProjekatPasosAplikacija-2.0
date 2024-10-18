using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class RegistracijaModel
{
    [Required(ErrorMessage = "ЈМБГ је обавезан.")]
    [StringLength(13, ErrorMessage = "ЈМБГ не сме бити дужи од 13 бројева.")]
    [RegularExpression(@"^[0-9]{13}$", ErrorMessage = "ЈМБГ мора да се састоји од 13 цифара.")]
    public string JMBG { get; set; }

    [Required(ErrorMessage = "Име је обавезно.")]
    [StringLength(40, ErrorMessage = "Име не сме бити дуже од 40 карактера.")]
    public string Ime { get; set; }

    [Required(ErrorMessage = "Презиме је обавезно.")]
    [StringLength(40, ErrorMessage = "Презиме не сме бити дуже од 40 карактера.")]
    public string Prezime { get; set; }

    [Required(ErrorMessage = "Имејл адреса је обавезна.")]
    [EmailAddress(ErrorMessage = "Неисправна имејл адреса.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Лозинка је обавезна.")]
    [DataType(DataType.Password)]
    public string Lozinka { get; set; }

    [Required(ErrorMessage = "Дата рођења је обавезна.")]
    public DateOnly DatumRodjenja { get; set; }

    [Required(ErrorMessage = "Адреса је обавезна.")]
    [StringLength(40, ErrorMessage = "Адреса не сме бити дуже од 40 карактера.")]
    public string Adresa { get; set; }

    [Required(ErrorMessage = "Телефон је обавезан.")]
    [StringLength(15, ErrorMessage = "Телефон не сме бити дужи од 15 карактера.")]
    public string Telefon { get; set; }

    [Required(ErrorMessage = "ПТТ место је обавезно.")]
    public int PTTMesto { get; set; }

    [Required(ErrorMessage = "Пол корисника је обавезно.")]
    public int PolKorisnika { get; set; }

    [Required(ErrorMessage = "ID држава рођења је обавезан.")]
    [StringLength(3, ErrorMessage = "ID држава рођења не сме бити дужи од 3 карактера.")]
    public string IDDrzavaRodjenja { get; set; }

    [Required(ErrorMessage = "ID држава држављанства је обавезан.")]
    [StringLength(3, ErrorMessage = "ID држава држављанства не сме бити дужи од 3 карактера.")]
    public string IDDrzavaDrzavljanstva { get; set; }

    public string? KorisnickoIme { get; set; }

    [NotMapped]
    public DateTime DatumRodjenjaDateTime
    {
        get => DatumRodjenja.ToDateTime(TimeOnly.MinValue); 
        set => DatumRodjenja = DateOnly.FromDateTime(value);
    }
}
