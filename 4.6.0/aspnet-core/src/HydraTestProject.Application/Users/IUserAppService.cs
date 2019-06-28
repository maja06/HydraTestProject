using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HydraTestProject.Roles.Dto;
using HydraTestProject.Users.Dto;

namespace HydraTestProject.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
