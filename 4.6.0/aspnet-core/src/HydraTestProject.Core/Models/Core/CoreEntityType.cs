using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HydraTestProject.Models.Core
{
    public class CoreEntityType : Entity
    {
        [MaxLength(64)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int Level { get; set; }

        public int Order { get; set; }

        [MaxLength(124)]
        public string Path { get; set; }

        [MaxLength(64)]
        public string IconName { get; set; }

        public bool IsCustom { get; set; }

        public bool IsActive { get; set; }

        public int InsertUserId { get; set; }

        public DateTime InsertTime { get; set; }

        public string InsertIpAddress { get; set; }

        public int LastUpdateUserId { get; set; }

        public DateTime LastUpdateTime { get; set; }

        public string LastUpdateIpAddress { get; set; }

        public int? ParentEntityTypeId { get; set; }

        [ForeignKey("ParentEntityTypeId")]
        public CoreEntityType ParentEntityType { get; set; }

        public IList<CoreEntity> Entities { get; set; } = new List<CoreEntity>();

        public IList<CoreEntityTypeProperty> EntityTypeProperties { get; set; } = new List<CoreEntityTypeProperty>();
            




    }
}
