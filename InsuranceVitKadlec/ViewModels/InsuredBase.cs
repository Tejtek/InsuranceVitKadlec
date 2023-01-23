using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace InsuranceVitKadlec.ViewModels
{
    public abstract class InsuredBase
    {
        [DisplayName("Jméno")]
        public string Name { get; set; }
        [DisplayName("Příjmení")]
        public string Surname { get; set; }
        [DisplayName("Datum narození")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [DisplayName("Telefonní číslo")]
        public string PhoneNumber { get; set; }
        [DisplayName("Ulice")]
        public string Street { get; set; }
        [DisplayName("Město")]
        public string City { get; set; }
        [DisplayName("PSČ")]
        public int PostCode { get; set; }
        [DisplayName("Kuřák ano/ne")]
        public bool Smoker { get; set; }
        [DisplayName("Pohlaví (muž)")]
        public bool IsMan { get; set; }
        [EmailAddress]
        [DisplayName("el.adresa")]
        public string Email { get; set; }
        [DisplayName("Místo uložení profilové fotky")]
        public string PhotoName { get; set; }
     


    }
}
