using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Kiwifruit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly static string SecurityKey = "12345678900987654321";

        [AllowAnonymous]
        [HttpGet, Route("Login")]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return BadRequest(new { message = "username or password is incorrect." });

            var token = GetUserToken(username, "admin");

            return Ok(new { token });
        }

        [HttpGet,Route("RefreshToken")]
        public IActionResult RefreshToken(string token, string refreshToken)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(refreshToken))
                return BadRequest(new { message = "token or refreshToken is incorrect." });

            var handle = new JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = "http://localhost:5000",
                ValidIssuer = "http://localhost:5000",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("12345678900987654321"))
            };

            try
            {
                var claims = handle.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
                var username = claims.Identity.Name;
                var newToken = GetUserToken(username, "admin");
                var newRefreshToken = "654321";
                return Ok(new[] { token, newRefreshToken });
            }
            catch (Exception e)
            {

                return BadRequest(e.ToString());
            }
        }

        private string GetUserToken(string username, string role)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds()}"),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
