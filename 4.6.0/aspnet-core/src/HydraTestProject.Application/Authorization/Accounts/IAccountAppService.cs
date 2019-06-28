using System.Threading.Tasks;
using Abp.Application.Services;
using HydraTestProject.Authorization.Accounts.Dto;

namespace HydraTestProject.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
