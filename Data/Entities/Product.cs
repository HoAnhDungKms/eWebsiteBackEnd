using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Decription { get; set; }
        public string ImagePath { get; set; }

        //Navigation properties
        public List<ProductCategory> ProductCategories { get; set; }
        public List<Cart> Carts { get; set; }


    }
}
