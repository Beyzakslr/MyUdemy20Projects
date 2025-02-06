
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project12_JwtToken.JWT
{
    public class TokenGenerator
    {
        public string GeneratorJwtToken(string username,string email,string name,string surname)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("20Derste20ProjeToken+-*/1234tokenJWT"));
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claimsExample = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,username),
                new Claim(JwtRegisteredClaimNames.Email,email),
                new Claim(JwtRegisteredClaimNames.GivenName,name),
                new Claim(JwtRegisteredClaimNames.FamilyName,surname),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: claimsExample,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credential);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GeneratorJwtToken2(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("20Derste20ProjeToken+-*/1234tokenJWT"));
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claimsExample = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,username),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: claimsExample,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credential);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
