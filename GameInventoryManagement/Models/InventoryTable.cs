using System;
using System.Collections.Generic;

namespace GameInventoryManagement.Models
{
    public partial class InventoryTable
    {
        public int InventoryId { get; set; }
        public int UserId { get; set; }
        public int WeaponId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Weapon Weapon { get; set; } = null!;
    }
}
