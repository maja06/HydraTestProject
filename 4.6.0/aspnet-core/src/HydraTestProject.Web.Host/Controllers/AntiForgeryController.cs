using Microsoft.AspNetCore.Antiforgery;
using HydraTestProject.Controllers;

namespace HydraTestProject.Web.Host.Controllers
{
    public class AntiForgeryController : HydraTestProjectControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
