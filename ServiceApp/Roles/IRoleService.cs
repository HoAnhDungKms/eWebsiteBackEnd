using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Common;
using ViewModel.Role;

namespace ServiceApp.Roles
{
    public interface IRoleService
    {
        Task<List<RoleVm>> GetAll();
        Task<ApiResult<bool>> CreateRole(CreateRoleRequest roleVm);
        Task<ApiResult<bool>> UpdateRole(UpdateRoleRequest roleVm);
        Task<ApiResult<bool>> DeleteRole(Guid? roleId);
    }
}
