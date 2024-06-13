using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDomain.DTOS
{
    public class EmployeeDTORegister
    {
        [Required(ErrorMessage = "Email is requird")]
        [EmailAddress(ErrorMessage = "Email invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "name is requird")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Salary is requird")]
        public int Salary { get; set; }

        [Required(ErrorMessage = "ShiftOfWork is requird")]
        public DateTime ShiftOfWork { get; set; }

        [Required(ErrorMessage = "password is requird")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}