using System.ComponentModel.DataAnnotations;

namespace HospitalDomain.DTOS
{
    public class ForgetPasswordDto
    {
        [Required(ErrorMessage = "Email is requird")]
        [EmailAddress(ErrorMessage = "Email invalid")]
        public string Email { get; set; }
    }
}