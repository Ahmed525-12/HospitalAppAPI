namespace HospitalDomain.Entites.App
{
    public class Notes : BaseEntity
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
    }
}