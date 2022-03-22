using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Common;
using ViewModel.Product;

namespace ServiceApp.Products
{
    public interface IProductService
    {
        Task<List<ProductVm>> GetAll();
        Task<ProductVm> GetByName(string productName);
        Task<ProductVm> GetById(int productid);
        Task<List<ProductVm>> GetByCategoryId(int categoryId);
        Task<ApiResult<bool>> CreateProduct(CreateProductRequest request);
        Task<ApiResult<bool>> UpdateProduct(UpdateProductRequest request);
        Task<ApiResult<bool>> DeleteProduct(int productId);
        Task<ApiResult<bool>> CategoryAssign(int productId, CategoryAssignRequest request);
    }
}
