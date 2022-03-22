using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceApp.Categories;
using System.Threading.Tasks;
using ViewModel.Category;

namespace eWebsiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(
            ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _categoryService.GetAll();
            return Ok(products);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetById( int categoryId)
        {
            var category = await _categoryService.GetById(categoryId);
            if (category == null)
                return BadRequest("Can't find category");
            return Ok(category);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]

        public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _categoryService.CreateCategory(request);
            if (!result.IsSuccessed)
                return BadRequest("Can't create category");
            return Ok(result);
        }
        [HttpPut("{categoryId}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateMealCategory([FromRoute] int categoryId, [FromForm] UpdateCategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = categoryId;
            var result = await _categoryService.UpdateCategory(request);
            if (!result.IsSuccessed)
                return BadRequest("Can't update category");
            return Ok(result);
        }
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteMealCategory(int categoryId)
        {
            var result = await _categoryService.DeleteCategory(categoryId);
            if (!result.IsSuccessed)
                return BadRequest("Can't delete category");
            return Ok(result);
        }
    }
}
