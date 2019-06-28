using System.Threading.Tasks;
using Abp.Application.Services;
using HydraTestProject.Sessions.Dto;

namespace HydraTestProject.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
