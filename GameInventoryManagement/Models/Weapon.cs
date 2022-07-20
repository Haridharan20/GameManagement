﻿using System;
using System.Collections.Generic;

namespace GameInventoryManagement.Models
{
    public partial class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Price { get; set; } = null!;
    }
}