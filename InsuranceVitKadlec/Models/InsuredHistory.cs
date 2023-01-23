using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceVitKadlec.Models
{
    public class InsuredHistory
    {
        public int Id { get; set; }

        public int IdInsured { get; set; }
        [DisplayName("Jméno")]
        public string Name { get; set; }
        [DisplayName("Příjmení")]
        public string Surname { get; set; }
        [DisplayName("Datum narození")]
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
        [DisplayName("el.adresa")]
        public string Email { get; set; }

        public string PhotoName { get; set; }
        public DateTime CreateDate { get; set; }
        public string TypeOfChange { get; set; }
    }
}
