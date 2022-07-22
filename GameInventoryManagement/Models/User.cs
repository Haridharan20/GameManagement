using System;
using System.Collections.Generic;

namespace GameInventoryManagement.Models
{
    public partial class User
    {
        public User()
        {
            InventoryTables = new HashSet<InventoryTable>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public sbyte Isadmin { get; set; }

        public virtual ICollection<InventoryTable> InventoryTables { get; set; }
    }
}
