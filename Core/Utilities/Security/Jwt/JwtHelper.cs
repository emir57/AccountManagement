using Core.Entity.Concrete;
using Core.Extensions.Jwt;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Core.Utilities.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        private readonly TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }
        public AccessToken CreateToken(Account account)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            JwtSecurityToken jwt = createJwtSecurityToken(account, _tokenOptions, _accessTokenExpiration, signingCredentials);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string token = handler.WriteToken(jwt);
            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        private JwtSecurityToken createJwtSecurityToken(Account account, TokenOptions tokenOptions, DateTime accessTokenExpiration, SigningCredentials signing)
        {
            return new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                notBefore: DateTime.Now,
                expires: accessTokenExpiration,
                claims: setClaims(account),
                signingCredentials: signing);
        }

        private IEnumerable<Claim> setClaims(Account account)
        {
            List<Claim> claims = new List<Claim>();
            claims.AddEmail(account.Email);
            claims.AddNameIdentifier(account.Id.ToString());
            claims.AddName(account.Name);
            return claims;
        }
    }
}
