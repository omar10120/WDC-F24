﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WDC_F24.Domain.Entities
{
    public class Product :BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
