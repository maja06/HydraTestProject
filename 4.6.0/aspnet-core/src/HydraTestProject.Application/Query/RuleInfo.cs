using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;

namespace HydraTestProject.Query
{
    public class RuleInfo : Entity
    {

        public string Property { get; set; }

        public string Operator { get; set; }

        public string Value { get; set; }

        public string Condition { get; set; }

        public List<RuleInfo> Rules = new List<RuleInfo>();
    }
}
