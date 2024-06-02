using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDomain.Entites.App
{
    public class Session : BaseEntity
    {
        public Session()
        {
            Department = new HashSet<Department>();
        }

        public string GuestId { get; set; }
        public string EmployeeId { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string PromoCode { get; set; }
        public DateTime DatePick { get; set; }
        public bool IsPay { get; set; }
        public ICollection<Department> Department { get; set; }
        public History History { get; set; }

        [ForeignKey("History")]
        public int HistoryId { get; set; }
    }
}