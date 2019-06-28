using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities;

namespace HydraTestProject.Models.Tabels
{
    [Table("Tables", Schema = "meta")]
    public class TableMetadata : Entity
    {
        public string Name { get; set; }
        
    }
}
