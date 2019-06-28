using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text;
using Abp.Domain.Entities;

namespace HydraTestProject.Models.Core
{
    public class CoreEntityPropertyValue : Entity
    {
        public string TextValue { get; set; }

        public int IntValue { get; set; }

        public DateTime DataTimeValue { get; set; }

        public decimal DecimalValue { get; set; }

        public Guid GuidValue { get; set; }

        public int InsertUserId { get; set; }

        public DateTime InsertTime { get; set; }

        public string InsertIpAddress { get; set; }

        public int LastUpdateUserId { get; set; }

        public DateTime LastUpdateTime { get; set; }

        public string LastUpdateIpAddress { get; set; }

        public Guid EntityId { get; set; }

       [ForeignKey("EntityId")]
       public CoreEntity Entity { get; set; }

       public int EntityTypePropertyId { get; set; }

       [ForeignKey("EntityTypePropertyId")]
       public CoreEntityTypeProperty EntityTypeProperty { get; set; }



    }
}
