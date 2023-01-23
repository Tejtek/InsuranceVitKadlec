using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceVitKadlec.Models
{
    public class InsuredesInsurencesHistory
    {
        public int Id { get; set; }
        public int IdInsuredesInsurences { get; set; }
        public int InsuredId { get; set; }
        public int InsurenceId { get; set; }
        [DisplayName("Výše pojistky")]
        public int InsurenceValue { get; set; }
        [DisplayName("Popis")]
        public string InsurenceSubject { get; set; }
        [DisplayName("Platné od:")]
        public DateTime ValidFrom { get; set; }
        [DisplayName("Platné do:")]
        public DateTime ValidTo { get; set; }
        [DisplayName("Záznam vytvořen")]
        public DateTime CreateDate { get; set; }
        [DisplayName("Typ záznamu")]
        public string TypeOfChange { get; set; }

    }
}
