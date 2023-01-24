using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceVitKadlec.Models
{
    public class InsuredInsurenceEvent
    {
        public int Id { get; set; }
        [ForeignKey(nameof(InsuredesInsurences))]
        public int InsuredesInsurencesId { get; set; }
        [DisplayName("Služba")]
        public InsuredesInsurences InsuredesInsurences { get; set; }
        [DisplayName("Oznámení pojistné události")]
        public string Name { get; set; }
        [DisplayName("Popis pojistné události")]
        public string Description { get; set; }
        [DisplayName("Odhadované pojistné plnění")]
        public double InsurenceDamage { get; set; }
        [DisplayName("Vznik pojistné události")]
        public DateTime DamageDate { get; set; }


    }
}
