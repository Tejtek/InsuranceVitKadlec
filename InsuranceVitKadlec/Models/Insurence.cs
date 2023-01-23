using System.ComponentModel;

namespace InsuranceVitKadlec.Models
{
    public class Insurence
    {
        public int Id { get; set; }
        [DisplayName("Název produktu")]
        public string Name { get; set; }
        [DisplayName("Popis produktu")]
        public string Description { get; set; }

        public ICollection<InsuredesInsurences> InsuredesInsurences { get; set; }
    }
}
