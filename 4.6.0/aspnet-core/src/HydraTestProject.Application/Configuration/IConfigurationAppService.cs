using System.Threading.Tasks;
using HydraTestProject.Configuration.Dto;

namespace HydraTestProject.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
