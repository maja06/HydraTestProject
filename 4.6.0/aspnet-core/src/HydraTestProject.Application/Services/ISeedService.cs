using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;

namespace HydraTestProject.Services
{
    public interface ISeedService : IApplicationService
    {
        void CreateEntityType(int n);
        void CreateEntityProperty(int n);
        int GetId();


    }
}
