using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace InsuranceVitKadlec.ViewModels
{
    public class Login
    {
        [EmailAddress]
        [DisplayName("Emailová adresa")]
        public string Email { get; set; }

        /// <summary>
        /// [PasswordPropertyText]
        /// </summary>
        [DisplayName("Heslo")]
        [DataType(DataType.Password)]   
        public string Password { get; set; }

    }
}
