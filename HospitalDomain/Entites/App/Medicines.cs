using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDomain.Entites.App
{
    public class Medicines : BaseEntity
    {
        public Medicines()
        {
            Category = new HashSet<Category>();
        }

        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime ExpDate { get; set; }
        public ICollection<Category> Category { get; set; }
        public Orders Orders { get; set; }

        [ForeignKey("Orders")]
        public int OrdersId { get; set; }

        public Pharmacy Pharmacy { get; set; }

        [ForeignKey("Pharmacy")]
        public int PharmacyId { get; set; }

        public Reports Reports { get; set; }

        [ForeignKey("Reports")]
        public int ReportsId { get; set; }
    }
}