using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDomain.DTOS
{
    public class EmailDTO
    {
        public int Id { get; set; } = 1;

        public string To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}