using Abp.Authorization;
using HydraTestProject.Authorization.Roles;
using HydraTestProject.Authorization.Users;

namespace HydraTestProject.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
