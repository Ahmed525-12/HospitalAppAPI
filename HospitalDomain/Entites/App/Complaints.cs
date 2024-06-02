using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDomain.Entites.App
{
    public class Complaints : BaseEntity
    {
        public string Name { get; set; }
        public string EmployeeId { get; set; }
        public string Description { get; set; }
    }
}