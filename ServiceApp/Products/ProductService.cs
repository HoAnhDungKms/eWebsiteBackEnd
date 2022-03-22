using Data.EF;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ServiceApp.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Common;
using ViewModel.Product;

namespace ServiceApp.Products
{
    public class ProductService :IProductService
    {
        private readonly eDbContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public ProductService(eDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }


        public async Task<List<ProductVm>> GetAll()
        {
            var product = await _context.Products.Select(x => new ProductVm()
            {
                Id = x.Id,
                Name = x.Name,
                CreatedDate = x.CreatedDate,
                Price = x.Price,
                Decription = x.Decription,
                ImagePath = x.ImagePath,
            }).ToListAsync();
            return product;
        }

        public async Task<ProductVm> GetByName(string productName)
        {
            var product = _context.Products.Where(x => x.Name == productName).Select(x => new ProductVm
            {
                Id = x.Id,
                Name = x.Name,
                CreatedDate = x.CreatedDate,
                Price = x.Price,
                Decription = x.Decription,
                ImagePath= x.ImagePath,
            }).FirstOrDefault();
            return product;
        }
        public async Task<ProductVm> GetById(int productid)
        {
            var product = _context.Products.Where(x => x.Id == productid).Select(x => new ProductVm
            {
                Id = x.Id,
                Name = x.Name,
                CreatedDate = x.CreatedDate,
                Price = x.Price,
                Decription = x.Decription,
                ImagePath = x.ImagePath,
            }).FirstOrDefault();
            return product;
        }
        public async Task<List<ProductVm>> GetByCategoryId(int categoryId)
        {
            var query = from p in _context.Products
                        join pic in _context.ProductCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pic };
            if (categoryId > 0)
            {
                query = query.Where(p => p.pic.CategoryId == categoryId);
            }
            var data = await query.Select(x => new ProductVm()
                {
                Id = x.p.Id,
                Name = x.p.Name,
                CreatedDate = x.p.CreatedDate,
                Price = x.p.Price,
                Decription = x.p.Decription,
                ImagePath = x.p.ImagePath,
            }).ToListAsync();
            return data;
        }

        public async Task<ApiResult<bool>> CreateProduct(CreateProductRequest request)
        {
            if (request == null)
                return new ApiErrorResult<bool>("Product can't null");
            var product = new Product()
            {
                Name = request.Name,
                CreatedDate = request.CreatedDate,
                Price= request.Price,
                Decription= request.Decription,
                ImagePath = await this.SaveFile(request.ThumbnailImage)
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

        public async Task<ApiResult<bool>> UpdateProduct(UpdateProductRequest request)
        {
            if (request == null)
                return new ApiErrorResult<bool>("Request can't null");
            var product = await _context.Products.FindAsync(request.Id);
            product.Name = request.Name;
            product.CreatedDate = request.CreatedDate;
            product.Price = request.Price;
            product.Decription = request.Decription;
            product.ImagePath = await this.SaveFile(request.ThumbnailImage);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> DeleteProduct(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
                return new ApiErrorResult<bool>("Can't find Product");
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> CategoryAssign(int productId, CategoryAssignRequest request)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return new ApiErrorResult<bool>("Product isn't exists");
            }
            foreach (var Category in request.ProductCategories)
            {
                var productCategory = await _context.ProductCategories
                    .FirstOrDefaultAsync(x => x.CategoryId == int.Parse(Category.Id)
                    && x.ProductId == productId);
                if (productCategory != null && Category.Selected == false)
                {
                    _context.ProductCategories.Remove(productCategory);
                }
                else if (productCategory == null && Category.Selected)
                {
                    await _context.ProductCategories.AddAsync(new ProductCategory()
                    {
                        CategoryId = int.Parse(Category.Id),
                        ProductId = productId
                    });
                }
            }
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }
    }

}

