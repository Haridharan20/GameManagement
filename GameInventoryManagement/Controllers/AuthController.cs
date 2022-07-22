using GameInventoryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bcrypt = BCrypt.Net.BCrypt;

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
            string isAdmin = null;
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if (dbUser != null)
            {
                if (bcrypt.Verify(user.Password,dbUser.Password))
                {
                    switch (dbUser.Isadmin)
                    {
                        case 0:
                            isAdmin = "user";
                            break;
                        case 1:
                            isAdmin = "admin";
                            break;

                    }
                    return Ok(isAdmin);
                }
                else
                {
                    return BadRequest("Password is Wrong");
                }
            }
            return BadRequest("User Not Found");
        }

        [HttpGet]
        [Route("viewdata")]
        public async Task<ActionResult<List<User>>> GetAllDetails()
        {
            if (isAuthorize())
            {
                return Ok(await _context.Users.ToListAsync());
            }
            else
            {
                return BadRequest("Not Allowed");
            }
        }

        [HttpPut]
        [Route("edit/{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] string isAdmin)
        {
           
            if (isAuthorize())
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
            else
            {
                return Unauthorized();
            }
        }

        private bool isAuthorize()
        {
            var re = Request;
            var headers = re.Headers;
            if (headers.ContainsKey("Authorization"))
            {
                var token = headers["Authorization"];
                if (token == "admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}