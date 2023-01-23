using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceVitKadlec.ViewModels
{
    public abstract class InsuredBase
    {
        [DisplayName("Jméno")]
        [Required]
        public string Name { get; set; }
        [DisplayName("Příjmení")]
        [Required]
        public string Surname { get; set; }
        [DisplayName("Datum narození")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [DisplayName("Telefonní číslo")]
        public string PhoneNumber { get; set; }
        [DisplayName("Ulice")]
        [Required]
        public string Street { get; set; }
        [DisplayName("Město")]
        [Required]
        public string City { get; set; }
        [DisplayName("PSČ")]
        [Required]
        public int PostCode { get; set; }
        [DisplayName("Kuřák ano/ne")]
        [Required]
        public bool Smoker { get; set; }
        [DisplayName("Pohlaví (muž)")]
        [Required]
        public bool IsMan { get; set; }
        [EmailAddress]
        [DisplayName("el.adresa")]
        [Required]
        public string Email { get; set; }
        [DisplayName("Místo uložení profilové fotky")]
        public string PhotoName { get; set; }
        
        
        // proměnná pouze ke spojení - nesmí se aktualizovat do DB
        [NotMapped]
        public string FullName => Name + " " + Surname;


    }
}
