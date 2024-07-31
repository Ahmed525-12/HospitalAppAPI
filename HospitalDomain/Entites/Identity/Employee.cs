namespace HospitalDomain.Entites.Identity
{
    public class Employee : Account
    {
        public int Salary { get; set; }
        public DateTime ShiftOfWork { get; set; }
    }
}