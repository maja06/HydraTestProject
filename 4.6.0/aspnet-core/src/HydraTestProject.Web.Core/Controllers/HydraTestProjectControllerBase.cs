using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace HydraTestProject.Controllers
{
    public abstract class HydraTestProjectControllerBase: AbpController
    {
        protected HydraTestProjectControllerBase()
        {
            LocalizationSourceName = HydraTestProjectConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
