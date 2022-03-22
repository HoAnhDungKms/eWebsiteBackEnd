using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Cart
{
    public class UpdateCartRequest
    {
        public int productId { get; set; }
        public int Quantity { get; set; }
    }
}
