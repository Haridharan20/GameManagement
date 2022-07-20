using GameInventoryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameInventoryManagement.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly gamemanagementContext _context;

        public AuthController(gamemanagementContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("User Created Successfully");
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<User>> Login([FromBody] Login user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if (dbUser == null)
            {
                return BadRequest("User Not Found");
            }
            return Ok(dbUser);
        }
    }
}