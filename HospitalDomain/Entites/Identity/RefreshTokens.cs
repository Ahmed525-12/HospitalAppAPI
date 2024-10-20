using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDomain.Entites.Identity
{
    [Owned]
    public class RefreshTokens
    {
        public string Token { get; set; }
        public DateTime Expireson { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expireson;
        public DateTime CreatedAt { get; set; }
        public DateTime? RevokedOn { get; set; }

        public bool IsAvtive => RevokedOn == null && !IsExpired;
    }
}