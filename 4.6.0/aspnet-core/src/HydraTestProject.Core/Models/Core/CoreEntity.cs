using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HydraTestProject.Models.Core
{
    public class CoreEntity : Entity<Guid>
    {
        [MaxLength(64)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int InsertUserId { get; set; }

        public DateTime InsertTime { get; set; }

        public string InsertIpAddress { get; set; }

        public int LastUpdateUserId { get; set; }

        public DateTime LastUpdateTime { get; set; }

        public string LastUpdateIpAddress { get; set; }

        public int EntityTypeId { get; set; }

        [ForeignKey("EntityTypeId")]
        public CoreEntityType EntityType { get; set; }

        public List<CoreEntityPropertyValue> EntityPropertyValues { get; set; } = new List<CoreEntityPropertyValue>();
    }
}
