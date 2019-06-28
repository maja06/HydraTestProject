using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;

namespace HydraTestProject.Services
{
    public interface ISeedService : IApplicationService
    {
        void Create(int n);
    }
}
