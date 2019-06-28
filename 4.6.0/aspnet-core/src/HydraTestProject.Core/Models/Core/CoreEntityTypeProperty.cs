using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using System.Text;
using Abp.Domain.Entities;
using HydraTestProject.Models.Tabels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HydraTestProject.Models.Core
{
    public class CoreEntityTypeProperty : Entity
    {
        [MaxLength(64)]
        public string Name { get; set; }

        public string Descriptopn { get; set; }

        [MaxLength(64)]
        public string DbType { get; set; }

        [MaxLength(12)]
        public string DbPrecision { get; set; }

        public enum ReferenceType
        {
            NoReference = 0,

            InternalReference = 1,

            ExternalReference = 2
        }

        public int ReferenceTableId { get; set; }

        [ForeignKey("ReferenceTableId")]
        public TableMetadata Metadata { get; set; }

        public int PropertyOrder { get; set; }

        public string DefaultValue { get; set; }

        public bool IsRequired { get; set; }

        public bool IsCustom { get; set; }

        public bool IsTranslatableValue { get; set; }

        public bool IsProtected { get; set; }
        
        public int InsertUseId { get; set; }
        
        public DateTime InsertTime { get; set; }

        public string InsertIpAddress { get; set; }

        public int LastUpdateUserId { get; set; }

        public DateTime LastUpdateTime { get; set; }

        public string LastUpdateIpAddress { get; set; }
        
        public int EntityTypeId { get; set; }

        [ForeignKey( "EntityTypeId")]
        public CoreEntityType EntityType { get; set; }












    }
}
