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
                return Ok(new { message = "Weapon Created Successfully" });
        }

        [HttpGet]
        [ Route("weapons")]
        public async Task<ActionResult<List<Weapon>>> GetWeapons()
        {

            return Ok(await _context.Weapons.ToListAsync());
        }

        [HttpGet]
        [Route("weapon/{id}")]
        public async Task<ActionResult<List<Weapon>>> GetWeapons(int id)
        {

            var dbWeapon = await _context.Weapons.FirstOrDefaultAsync(x => x.Id == id);
            if (dbWeapon != null)
            {
               
                return Ok(dbWeapon);
            }
            return BadRequest(new {message= "Weapon Not Found" });
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
                    return Ok(new { message="Update Successfully" });
                }
                return BadRequest(new { message = "Weapon Not Found" });
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
                    return Ok(new { message = "Weapon Deleted Successfully" });
                }
                return BadRequest(new {message= "Weapon Not Found" });
              return Unauthorized();
            
        }


    }
}
