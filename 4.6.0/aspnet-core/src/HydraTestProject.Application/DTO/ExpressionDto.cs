using System;
using System.Collections.Generic;
using System.Text;
using HydraTestProject.Models.Core;
using HydraTestProject.Models.Tabels;

namespace HydraTestProject.DTO
{
    public class ExpressionDto
    {
        public CoreEntity __Entity { get; set; }

        public CoreEntityTypeProperty __Property { get; set; }

        public CoreEntityPropertyValue __Value { get; set; }

        public CoreEntity Lorry { get; set; }

        public CoreEntity Pearla { get; set; }

        public CoreEntity Kurt { get; set; }

        public TableA Kessia { get; set; }
    }
}
