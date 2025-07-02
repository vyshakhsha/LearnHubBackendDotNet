using Microsoft.AspNetCore.Mvc;
using LearnHubBackendDotNet.Data;
using LearnHubBackendDotNet.Models;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace LearnHubBackendDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var expiry = GetTokenExpiry(token);

                if (expiry == null)
                {
                    return BadRequest(new { message = "Invalid token." });
                }

                var blacklistEntry = new TokenBlacklist
                {
                    Token = token,
                    ExpiryDate = expiry.Value
                };

                _context.TokenBlacklists.Add(blacklistEntry);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Logout successful" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        private DateTime? GetTokenExpiry(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            return jwtToken?.ValidTo;
        }
    }
}
