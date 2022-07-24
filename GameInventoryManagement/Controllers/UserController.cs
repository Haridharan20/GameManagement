using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using GameInventoryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameInventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly gamemanagementContext _context;
        public UserController(gamemanagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("getUsers")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet]
        [Route("myWeapons/{id}")]
        public async Task<ActionResult<List<Weapon>>> GetAllDetails(int id)
        {
            var query = from Weapon in _context.Weapons
                        join InventoryTable in _context.InventoryTables on Weapon.Id equals InventoryTable.WeaponId 
                        join User in _context.Users on InventoryTable.UserId equals User.Id 
                        where User.Id==id
                        select new
                        {
                            Weapon.Id,
                            Weapon.Name,
                            Weapon.Price
                        };
            return Ok(new {message=query});
        }  

        [HttpPut]
        [Route("edit/{id}"), Authorize(Roles = "1")]
        public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] int isAdmin)
        {
            //var val = Convert.ToInt16(isAdmin);
            if (isAdmin == 0 || isAdmin == 1)
            {
                var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (dbUser != null)
                {
                    dbUser.Isadmin = Convert.ToSByte(isAdmin);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Update Successfully" });
                }
                return BadRequest("User Not Found");
            }
            return BadRequest("Enter only 0 or 1");

        }

        [HttpGet("buyweapon/{wid}")]
        public async Task<ActionResult<string>> BuyWeapon(int wid, string price)
        {
            var uid = decode();
            var amount = Convert.ToInt32(price);
            var dbWeapon = await _context.Weapons.FirstOrDefaultAsync(x => x.Id == wid);
            if (dbWeapon != null)
            {
                if (amount == dbWeapon.Price)
                {
                    _context.InventoryTables.Add(new InventoryTable
                    {
                        UserId = uid,
                        WeaponId = wid
                    });
                    await _context.SaveChangesAsync();
                    return Ok(new {message="Purchased Successfully" });
                }
                else if (amount > dbWeapon.Price)
                {
                    return BadRequest("Enter correct amount");
                }
                else
                {
                    return BadRequest("Amount is Not sufficient");
                }
            }
            else
            {
                return BadRequest("Weapon not found");
            }
        }

        private int decode()
        {
            var handler = new JwtSecurityTokenHandler();
            string authHeader = Request.Headers["Authorization"];
            authHeader = authHeader.Replace("bearer ", "");
            var jsonToken = handler.ReadToken(authHeader);
            var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
            var id = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            return Convert.ToInt32(id);
        }

    }
}
