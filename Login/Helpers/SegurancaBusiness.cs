using JWT.Algorithms;
using JWT.Builder;
using Login.Domain;
using Login.Domain.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace Login.Helpers
{
    public static class SegurancaBusiness
    {
        public static string CriarHashSenha(string senha)
        {
            var md5 = MD5.Create();
            byte[] bytes = Encoding.ASCII.GetBytes(senha);
            byte[] hash = md5.ComputeHash(bytes);

            var str = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                str.Append(hash[i].ToString("X2"));
            }

            return str.ToString();
        }
        public static string GerarToken(TokenConfiguration tokenConfiguration, UserLoginDomain user)
        {
            var token = new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret(tokenConfiguration.Hash)
                    .AddClaim("exp", DateTimeOffset.Now.AddMinutes(tokenConfiguration.ExpirationInMinuts).ToUnixTimeSeconds())
                    .AddClaim("id", user.UserId)
                    .AddClaim("Name", user.FirstName)
                    .AddClaim("LastName", user.LastName)
                    .AddClaim("iss", tokenConfiguration.Issuer)
                    .AddClaim("aud", tokenConfiguration.Audience)
                    .Encode();
            return token;
        }

        public static void VerificarToken(string Token, TokenConfiguration tokenConfiguration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(Token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfiguration.Hash)),
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = tokenConfiguration.Issuer,
                ValidAudience = tokenConfiguration.Audience,
            }, out SecurityToken validatedToken);
        }

        public static string GeraSenhaRandon()
        {
            string chars = "@!abcdefghjkmnpqrstuvwxyz0123456789";
            string novaSenha = "";
            Random random = new Random();
            for (int f = 0; f < 10; f++)
            {
                novaSenha += chars.Substring(random.Next(0, chars.Length - 1), 1);
            }

            return novaSenha;
        }
    }
}
