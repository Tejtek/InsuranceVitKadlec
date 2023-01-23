using InsuranceVitKadlec.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;

namespace InsuranceVitKadlec.Models
{
    public class Insured :InsuredBase
    {
        public int Id { get; set; }
        
        [HiddenInput]
        public string LoginId { get; set; }

        public ICollection<InsuredesInsurences> InsurenceInsuredesInsurences { get; set; }   

    }
}
