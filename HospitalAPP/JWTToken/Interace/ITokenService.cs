using HospitalDomain.Entites.Identity;

namespace HospitalAPP.JWTToken.Interace
{
    public interface ITokenService
    {
        public string CreateTokenAsync(Account user);
    }
}