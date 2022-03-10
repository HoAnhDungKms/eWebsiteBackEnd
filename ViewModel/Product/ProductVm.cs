using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Product
{
    public class ProductVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Decription { get; set; }
        public string ImagePath { get; set; }
    }
}
