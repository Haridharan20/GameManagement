using GameInventoryManagement.Models;
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
        [Route("add")]
        public async Task<ActionResult<Weapon>> AddWeapon([FromBody] Weapon weapon)
        {
           if(isAuthorize())
            {
                _context.Weapons.Add(weapon);
                await _context.SaveChangesAsync();
                return Ok("Weapon Created Successfully");
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [ Route("weapons")]
        public async Task<ActionResult<List<Weapon>>> GetWeapons()
        {

            return Ok();
        }

        [HttpPut]
        [Route("edit/{id}")]
        public async Task<ActionResult<Weapon>> UpdateWeapon(int id, [FromBody] Weapon weapon)
        {
            if(isAuthorize())
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
            else
            {
                return Unauthorized();
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult<Weapon>> DeleteWeapon(int id)
        {
            if (isAuthorize())
            {
                var dbWeapon = await _context.Weapons.FirstOrDefaultAsync(x => x.Id == id);
                if (dbWeapon != null)
                {
                    _context.Weapons.Remove(dbWeapon);
                    await _context.SaveChangesAsync();
                    return Ok("Weapon Deleted Successfully");
                }
                return BadRequest("Weapon Not Found");
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
