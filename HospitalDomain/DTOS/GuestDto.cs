namespace HospitalDomain.DTOS
{
    public class GuestDto
    {
        public string Email { get; set; }

        public long IdentityCardNumber { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
    }
}