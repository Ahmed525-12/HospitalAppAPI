using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDomain.DTOS
{
    public class EmployeeDTOLogin
    {
        [Required(ErrorMessage = "Email is requird")]
        [EmailAddress(ErrorMessage = "Email invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "password is requird")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}