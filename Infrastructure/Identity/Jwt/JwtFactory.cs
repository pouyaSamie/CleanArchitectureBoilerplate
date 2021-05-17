using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Application.Common.Models;
using Infrastructure.Identity.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Jwt
{
    public class JwtFactory : IJwtFactory
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtTokenConfig _tokenConfig;
        public JwtFactory(UserManager<ApplicationUser> userManager, JwtTokenConfig jwtTokenConfig)
        {
            _userManager = userManager;
            _tokenConfig = jwtTokenConfig;
        }
        public async Task<Result<ITokenModel>> GetTokenAsync(string userName, string Password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user != null && await _userManager.CheckPasswordAsync(user, Password))
            {
                var authClaims = new[]
             {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig.Secret));

                var token = new JwtSecurityToken(
                    issuer: _tokenConfig.ValidIssuer,
                    audience: _tokenConfig.ValidAudience,
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Result<ITokenModel>.Success(new TokenModel(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo));

            }

            return Result<ITokenModel>.Failure("Incorrect UserName or Password");


        }
    }
}
