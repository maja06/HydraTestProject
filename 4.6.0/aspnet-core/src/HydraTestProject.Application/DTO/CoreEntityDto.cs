using System;
using System.Collections.Generic;
using System.Text;
using Abp.AutoMapper;
using HydraTestProject.Models.Core;

namespace HydraTestProject.DTO
{
    [AutoMapFrom(typeof(CoreEntity))]
    public class CoreEntityDto
    {
        public string Name { get; set; }
    }
}
