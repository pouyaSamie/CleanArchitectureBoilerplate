using Infrastructure.Identity.Interfaces;
using System;

namespace Infrastructure.Identity.Jwt
{
    public class TokenModel : ITokenModel
    {
        public TokenModel(string token, DateTime expiration)
        {
            Token = token;
            Expiration = expiration;
        }
        public TokenModel() { }

        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
