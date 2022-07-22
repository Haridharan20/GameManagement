using System;
using System.Collections.Generic;

namespace GameInventoryManagement.Models
{
    public partial class InventoryTable
    {
        public int InventoryId { get; set; }
        public int UserId { get; set; }
        public int WeaponId { get; set; }
    }
}
