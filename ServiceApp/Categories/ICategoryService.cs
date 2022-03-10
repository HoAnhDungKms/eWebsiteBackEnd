using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Category;
using ViewModel.Common;

namespace ServiceApp.Categories
{
    public interface ICategoryService
    {
        Task<List<CategoryVm>> GetAll();
        Task<CategoryVm> GetById(int categoryId);
        Task<ApiResult<bool>> CreateCategory(CreateCategoryRequest request);
        Task<ApiResult<bool>> UpdateCategory(UpdateCategoryRequest request);
        Task<ApiResult<bool>> DeleteCategory(int categoryId);
    }
}
