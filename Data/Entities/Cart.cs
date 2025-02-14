﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int Quantity { get; set; }   
        public decimal Price { get; set; }

        // Navigation properties
        public Guid UserId { get; set; }    
        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
