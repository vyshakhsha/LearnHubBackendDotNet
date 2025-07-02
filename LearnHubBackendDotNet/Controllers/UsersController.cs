using LearnHubBackendDotNet.Data;
using LearnHubBackendDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearnHubBackendDotNet.Helpers;
using LearnHubBackendDotNet.DTO;
using Microsoft.AspNetCore.Identity;

namespace LearnHubBackendDotNet.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;
        public UsersController(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(RegisterRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data provided");
            }

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (existingUser != null)
            {
                return BadRequest("Email already registered.");
            }

            var PasswordHash = PasswordHasher.HashPassword(request.PasswordHash);

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                usertype = request.usertype,
                PasswordHash = PasswordHash
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                message = "Registration successful",
                user = new
                {
                    user.Id,
                    user.Username,
                    user.usertype,
                    user.Email
                }
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest("User ID mismatch");
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            if (!PasswordHasher.VerifyPassword(user.PasswordHash, request.Password))
            {
                return Unauthorized("Invalid email or password.");
            }

            var token = _jwtService.GenerateToken(user.Id, user.Email,user.usertype);

            return Ok(new
            {
                message = "Login successful",
                token,
                user = new { user.Id, user.Username, user.Email, user.usertype }
            });
        }

    }
}
