using System;
using System.Collections.Generic;

namespace GameInventoryManagement.Models
{
    public partial class Weapon
    {
        public Weapon()
        {
            InventoryTables = new HashSet<InventoryTable>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }

        public virtual ICollection<InventoryTable> InventoryTables { get; set; }
    }
}
