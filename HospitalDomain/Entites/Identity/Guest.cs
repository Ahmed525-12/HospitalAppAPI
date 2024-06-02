using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDomain.Entites.Identity
{
    public class Guest : Account
    {
        public int IdentityCardNumber { get; set; }
    }
}