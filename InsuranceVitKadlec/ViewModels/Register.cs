using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InsuranceVitKadlec.ViewModels
{
    public class Register : InsuredBase
    {
        [PasswordPropertyText]
        [DisplayName("Heslo")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [PasswordPropertyText]
        [DisplayName("Potvrzení hesla")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [DisplayName("Nahrát foto")]
        public IFormFile Picture { get; set; }


    }
}
