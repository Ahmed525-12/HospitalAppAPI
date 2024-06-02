using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDomain.Entites.Identity
{
    public class Employee : Account
    {
        public int Salary { get; set; }
        public DateTime ShiftOfWork { get; set; }
    }
}