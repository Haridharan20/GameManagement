using GameInventoryManagement.Models;
using GameInventoryManagement.Services;
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
        private readonly WeaponService _weaponService;

        public WeaponController(WeaponService weaponService)
        {
            _weaponService = weaponService;
        }

        [HttpPost]
        [Route("add"), Authorize(Roles = "1")]
        public async Task<ActionResult<Weapon>> Add([FromBody] Weapon weapon)
        {

            return Ok(_weaponService.AddWeapon(weapon));
        }

        [HttpGet]
        [Route("weapons")]
        public async Task<ActionResult<List<Weapon>>> GetAll()
        {

            return Ok(_weaponService.GetAllWeapons());
        }

        [HttpGet]
        [Route("weapon/{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var dbWeapon = await _weaponService.GetWeapon(id);
            if (dbWeapon != null)
            {
                return Ok(dbWeapon);
            }
            return BadRequest(new { message = "Weapon Not Found" });
        }
        [HttpPut]
        [Route("edit/{id}"), Authorize(Roles = "1")]
        public async Task<ActionResult<Weapon>> Update(int id, [FromBody] Weapon weapon)
        {

            var dbWeapon = await _weaponService.UpdateWeapon(id, weapon);
            if (dbWeapon != null)
            {

                return Ok(dbWeapon);
            }
            return BadRequest(new { message = "Weapon Not Found" });
        }

        [HttpDelete]
        [Route("delete/{id}"), Authorize(Roles = "1")]
        public async Task<IActionResult> Delete(int id)
        {

            var dbWeapon = await _weaponService.DeleteWeapon(id);
            if (dbWeapon != null)
            {

                return Ok(dbWeapon);
            }
            return BadRequest(new { message = "Weapon Not Found" });

        }


    }
}
