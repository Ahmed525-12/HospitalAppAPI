namespace HospitalDomain.Entites.App
{
    public class Complaints : BaseEntity
    {
        public string Name { get; set; }
        public string EmployeeId { get; set; }
        public string Description { get; set; }
    }
}