using GameInventoryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameInventoryManagement.Services
{
    public class WeaponService
    {
        private readonly gamemanagementContext _context;

        public WeaponService()
        {

        }
        public WeaponService(gamemanagementContext context)
        {
            _context = context;
        }
        public virtual async Task<object> AddWeapon(Weapon weapon)
        {
            _context.Weapons.Add(weapon);
            await _context.SaveChangesAsync();
            //return Ok(new { message = "Weapon Created Successfully" });
            return new { message = "Weapon Created Successfully" };
        }

        public virtual async Task<List<Weapon>> GetAllWeapons()
        {

            return await _context.Weapons.ToListAsync();
        }

        public virtual async Task<object> GetWeapon(int id)
        {

            var dbWeapon = await _context.Weapons.FirstOrDefaultAsync(x => x.Id == id);
            if (dbWeapon != null)
            {
                return dbWeapon;
            }
            return null;
        }

        public virtual async Task<object> UpdateWeapon(int id, [FromBody] Weapon weapon)
        {

            var dbWeapon = await _context.Weapons.FirstOrDefaultAsync(x => x.Id == id);
            if (dbWeapon != null)
            {
                dbWeapon.Name = weapon.Name;
                dbWeapon.Price = weapon.Price;
                await _context.SaveChangesAsync();
                return new { message = "Update Successfully" };
            }

            return null;

        }

        public virtual async Task<object> DeleteWeapon(int id)
        {

            var dbWeapon = await _context.Weapons.FirstOrDefaultAsync(x => x.Id == id);
            if (dbWeapon != null)
            {
                _context.Weapons.Remove(dbWeapon);
                await _context.SaveChangesAsync();
                return new { message = "Weapon Deleted Successfully" };
            }
            return null;

        }

    }

}
