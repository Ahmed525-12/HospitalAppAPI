using Microsoft.AspNetCore.Identity;

namespace HospitalDomain.Entites.Identity
{
    public class Account : IdentityUser
    {
        public string DisplayName { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<RefreshTokens>? RefreshToken { get; set; }
    }
}