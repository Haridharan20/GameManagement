using System.IdentityModel.Tokens.Jwt;
using GameInventoryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameInventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponController : ControllerBase
    {
        private readonly gamemanagementContext _context;

        public WeaponController(gamemanagementContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("add"),Authorize(Roles = "1")]
        public async Task<ActionResult<Weapon>> AddWeapon([FromBody] Weapon weapon)
        {
           
                _context.Weapons.Add(weapon);
                await _context.SaveChangesAsync();
                return Ok("Weapon Created Successfully");
        }

        [HttpGet]
        [ Route("weapons")]
        public async Task<ActionResult<List<Weapon>>> GetWeapons()
        {

            return Ok();
        }

        [HttpPut]
        [Route("edit/{id}"), Authorize(Roles = "1")]
        public async Task<ActionResult<Weapon>> UpdateWeapon(int id, [FromBody] Weapon weapon)
        {
            
                var dbWeapon = await _context.Weapons.FirstOrDefaultAsync(x => x.Id == id);
                if(dbWeapon != null)
                {
                    dbWeapon.Name=weapon.Name;
                    dbWeapon.Price = weapon.Price;
                    await _context.SaveChangesAsync();
                    return Ok("Update Successfully");
                }
                return BadRequest("Weapon Not Found");
        }

        [HttpDelete]
        [Route("delete/{id}"), Authorize(Roles = "1")]
        public async Task<ActionResult<Weapon>> DeleteWeapon(int id)
        {
            
                var dbWeapon = await _context.Weapons.FirstOrDefaultAsync(x => x.Id == id);
                if (dbWeapon != null)
                {
                    _context.Weapons.Remove(dbWeapon);
                    await _context.SaveChangesAsync();
                    return Ok("Weapon Deleted Successfully");
                }
                return BadRequest("Weapon Not Found");
              return Unauthorized();
            
        }

        [HttpGet("buy/{wid}")]
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
                    return Ok("Purchased Successfully");
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
