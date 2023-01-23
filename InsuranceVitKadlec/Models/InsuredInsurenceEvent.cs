using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceVitKadlec.Models
{
    public class InsuredInsurenceEvent
    {
        public int Id { get; set; }
        [ForeignKey(nameof(InsuredesInsurences))]
        public int InsuredesInsurencesId { get; set; }

        public InsuredesInsurences InsuredesInsurences { get; set; }
        [DisplayName("Oznámení škodní události")]
        public string Name { get; set; }
        [DisplayName("Popis škodní události")]
        public string Description { get; set; }
        [DisplayName("Odhadovaná výše škody")]
        public double InsurenceDamage { get; set; }
        [DisplayName("Vznik škodní události")]
        public DateTime DamageDate { get; set; }


    }
}
