using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Policy;
using System.Text;
using Abp.Domain.Entities;

namespace HydraTestProject.Models
{
    public class TestClass : Entity
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public bool Bool { get; set; }

        public string IpAddress { get; set; }

        public string Path { get; set; }


    }
}
