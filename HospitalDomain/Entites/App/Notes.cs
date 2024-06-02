using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDomain.Entites.App
{
    public class Notes : BaseEntity
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
    }
}