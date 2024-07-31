namespace HospitalDomain.Entites.App
{
    public class Pharmacy : BaseEntity
    {
        public Pharmacy()
        {
            Orders = new HashSet<Orders>();
            Medicines = new HashSet<Medicines>();
        }

        public string EmployeeId { get; set; }
        public ICollection<Orders> Orders { get; set; }
        public ICollection<Medicines> Medicines { get; set; }
    }
}