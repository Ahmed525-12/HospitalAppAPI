namespace HospitalDomain.Entites.App
{
    public class History : BaseEntity
    {
        public History()
        {
            Department = new HashSet<Department>();
            Session = new HashSet<Session>();
        }

        public string UserId { get; set; }
        public string Title { get; set; }
        public ICollection<Department> Department { get; set; }
        public ICollection<Session> Session { get; set; }
    }
}