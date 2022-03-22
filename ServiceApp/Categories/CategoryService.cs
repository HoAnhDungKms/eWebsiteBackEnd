using Data.EF;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Category;
using ViewModel.Common;

namespace ServiceApp.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly eDbContext _context;

        public CategoryService(eDbContext context)
        {
            _context = context;
        }


        public async Task<List<CategoryVm>> GetAll()
        {
            var category = await _context.Categories.Select(x => new CategoryVm()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
            return category;           
        }

        public async Task<CategoryVm> GetById(int categoryId)
        {
            var category = _context.Categories.Where(x=>x.Id == categoryId).Select(x=> new CategoryVm
            {
                Id=x.Id,
                Name=x.Name,
            }).FirstOrDefault();
            return category;
        }

        public async Task<ApiResult<bool>> CreateCategory(CreateCategoryRequest request)
        {
            if (request == null)
                return new ApiErrorResult<bool>("Category can't null");
            var category = new Category()
            {
                Name = request.Name,
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> UpdateCategory(UpdateCategoryRequest request)
        {
            if(request == null)
                return new ApiErrorResult<bool>("Request can't null");
            var category = await _context.Categories.FindAsync(request.Id);
            category.Name = request.Name;
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> DeleteCategory(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
                return new ApiErrorResult<bool>("Can't find Category");
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }
    }
}
