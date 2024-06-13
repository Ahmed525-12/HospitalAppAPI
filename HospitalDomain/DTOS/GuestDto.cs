using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDomain.DTOS
{
    public class GuestDto
    {
        public string Email { get; set; }

        public long IdentityCardNumber { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
    }
}