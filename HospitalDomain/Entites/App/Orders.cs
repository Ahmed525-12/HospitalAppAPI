using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalDomain.Entites.App
{
    public class Orders : BaseEntity
    {
        public Orders()
        {
            Medicines = new HashSet<Medicines>();
        }

        public string GuestId { get; set; }
        public int TotalPrice { get; set; }
        public string PromoCode { get; set; }
        public DateTime DatePick { get; set; }
        public bool IsPay { get; set; }
        public ICollection<Medicines> Medicines { get; set; }
        public Pharmacy Pharmacy { get; set; }

        [ForeignKey("Pharmacy")]
        public int PharmacyId { get; set; }
    }
}