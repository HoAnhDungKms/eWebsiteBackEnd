using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Common;
using ViewModel.Role;

namespace ServiceApp.Roles
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<RoleVm>> GetAll()
        {
            var roles = await _roleManager.Roles
                .Select(x => new RoleVm()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToListAsync();

            return roles;
        }

        public async Task<ApiResult<bool>> CreateRole(CreateRoleRequest request)
        {
            if (request == null)
                return new ApiErrorResult<bool>("Role can't null");
            var role = new Role()
            {
                Name = request.Name,
                Description= request.Description,
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Create Unsuccessfull");
        }

        public async Task<ApiResult<bool>> UpdateRole (UpdateRoleRequest request)
        {
            if (request == null)
                return new ApiErrorResult<bool>("Request can't null");
            var role = await _roleManager.FindByIdAsync((request.Id).ToString());
            role.Name = request.Name;
            role.Description = request.Description;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Update Unsuccessfull");
        }

        public async Task<ApiResult<bool>> DeleteRole(Guid? roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
                return new ApiErrorResult<bool>("Role doesn'i exits in database");
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Delete Unsuccessfull");
        }

    }
}
