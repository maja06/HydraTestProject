using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HydraTestProject.MultiTenancy.Dto;

namespace HydraTestProject.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

