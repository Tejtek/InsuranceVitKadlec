using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceVitKadlec.Models
{
    public class InsuredesInsurences
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Insured))]
        public int InsuredId { get; set; }
        
        public Insured Insured { get; set; }

        [ForeignKey(nameof(Insurence))]
        [DisplayName("Název služby")]
        public int InsurenceId { get; set; }
        [DisplayName("Název služby")]
        public Insurence Insurence { get; set; }
        [DisplayName ("Výše pojistky")]
        public int InsurenceValue { get; set; }
        [DisplayName("Popis")]
        public string InsurenceSubject { get; set; }
        [DisplayName("Platné od:")]
        public DateTime ValidFrom { get; set; }
        [DisplayName("Platné do:")]
        public DateTime ValidTo { get; set; }
        [DisplayName("Důvod ukončení")]
        public string TerminationReason { get; set; }

    }
}
