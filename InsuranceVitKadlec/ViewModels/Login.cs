using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace InsuranceVitKadlec.ViewModels
{
    public class Login
    {
        [EmailAddress]
        [DisplayName("Emailová adresa")]
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// [PasswordPropertyText]
        /// </summary>
        [DisplayName("Heslo")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

    }
}
