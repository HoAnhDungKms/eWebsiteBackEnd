using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceApp.Roles;
using System;
using System.Threading.Tasks;
using ViewModel.Role;

namespace eWebsiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetAll();
            return Ok(roles);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateCategory([FromForm] CreateRoleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _roleService.CreateRole(request);
            if (!result.IsSuccessed)
                return BadRequest("Can't create role");
            return Ok(result);
        }
        [HttpPut("{roleId}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateMealCategory([FromRoute] Guid roleId, [FromForm] UpdateRoleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = roleId;
            var result = await _roleService.UpdateRole(request);
            if (!result.IsSuccessed)
                return BadRequest("Can't update role");
            return Ok(result);
        }
        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteMealCategory(Guid roleId)
        {
            var result = await _roleService.DeleteRole(roleId);
            if (!result.IsSuccessed)
                return BadRequest("Can't delete role");
            return Ok(result);
        }
    }
}
