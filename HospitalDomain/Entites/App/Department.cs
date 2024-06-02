using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDomain.Entites.App
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public string EmployeeId { get; set; }
        public Session Session { get; set; }

        [ForeignKey("Session")]
        public int SessionId { get; set; }

        public History History { get; set; }

        [ForeignKey("History")]
        public int HistoryId { get; set; }
    }
}