using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Cart
{
    public class CartRequest
    {
        public int Id { get; set; }      
        public List<ItemViewModel> CartItems { get; set; } = new List<ItemViewModel>();
    }
}
