using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.Common;

namespace ViewModel.Product
{
    public class CategoryAssignRequest
    {
        public int Id { get; set; }
        public List<SelectItem> ProductCategories { get; set; } = new List<SelectItem>();
    }
}