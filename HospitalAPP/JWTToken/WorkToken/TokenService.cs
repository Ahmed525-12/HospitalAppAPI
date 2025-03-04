﻿using HospitalAPP.JWTToken.Interace;
using HospitalDomain.Entites.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HospitalAPP.JWTToken.WorkToken
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public RefreshTokens CreateRefreshTokenAsync()
        {
            return new RefreshTokens
            {
                Token = GenerateSecureToken(),
                Expireson = DateTime.UtcNow.AddDays(10),
                CreatedAt = DateTime.UtcNow
            };
        }

        private string GenerateSecureToken()
        {
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public string CreateTokenAsync(Account user)
        {
            // Claims
            var UserClaim = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.DisplayName),
            };

            // Security Key
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            // Create Token Object
            var Token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:Expire"])),
                claims: UserClaim,
                signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature)

                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}