using HospitalDomain.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPP.JWTToken.Interace
{
    public interface ITokenService
    {
        public string CreateTokenAsync(Account user);
    }
}