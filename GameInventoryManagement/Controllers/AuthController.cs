using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GameInventoryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using bcrypt = BCrypt.Net.BCrypt;

namespace GameInventoryManagement.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly gamemanagementContext _context;

        public AuthController(IConfiguration configuration,gamemanagementContext context)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if(dbUser != null)
            {
                return BadRequest("User Already exists");
            }
            user.Password = bcrypt.HashPassword(user.Password,12);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("User Created Successfully");
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<User>> Login([FromBody] Login user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if (dbUser != null)
            {
                if (bcrypt.Verify(user.Password,dbUser.Password))
                {
                    string role = dbUser.Isadmin.ToString();
                    string token = CreateToken(dbUser, role);
                    return Ok(token);
                }
                else
                {
                    return BadRequest("Password is Wrong");
                }
            }
            return BadRequest("User Not Found");
        }

        [HttpGet]
        [Route("viewdata"), Authorize(Roles = "1")]
        public async Task<ActionResult<List<User>>> GetAllDetails()
        {
           
                return Ok(await _context.Users.ToListAsync());
        }

        [HttpPut]
        [Route("edit/{id}"), Authorize(Roles = "1")]
        public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] string isAdmin)
        {
                var val = Convert.ToInt16(isAdmin);
                if (val == 0 || val == 1)
                {
                    var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                    if (dbUser != null)
                    {
                        dbUser.Isadmin = Convert.ToSByte(val);
                        await _context.SaveChangesAsync();
                        return Ok("Update Successfully");
                    }
                return BadRequest("User Not Found");
                }
                return BadRequest("Enter only 0 or 1");

        }

        private string CreateToken(User user, string role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Email", user.Email),
                new Claim("Id",user.Id.ToString()),
                new Claim(ClaimTypes.Role,role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}