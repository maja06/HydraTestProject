using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HydraTestProject.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoreEntityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Path = table.Column<string>(maxLength: 124, nullable: true),
                    IconName = table.Column<string>(maxLength: 64, nullable: true),
                    IsCustom = table.Column<bool>(nullable: false),
                    IsaActive = table.Column<bool>(nullable: false),
                    InsertUserId = table.Column<int>(nullable: false),
                    InsertTime = table.Column<DateTime>(nullable: false),
                    InsertIpAddress = table.Column<string>(nullable: true),
                    LastUpdateUserId = table.Column<int>(nullable: false),
                    LastUpdateTime = table.Column<DateTime>(nullable: false),
                    LastUpdateIpAddress = table.Column<string>(nullable: true),
                    ParentEntityTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoreEntityTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoreEntityTypes_CoreEntityTypes_ParentEntityTypeId",
                        column: x => x.ParentEntityTypeId,
                        principalTable: "CoreEntityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoreEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    InsertUserId = table.Column<int>(nullable: false),
                    InsertTime = table.Column<DateTime>(nullable: false),
                    InsertIpAddress = table.Column<string>(nullable: true),
                    LastUpdateUserId = table.Column<int>(nullable: false),
                    LastUpdateTime = table.Column<DateTime>(nullable: false),
                    LastUpdateIpAddress = table.Column<string>(nullable: true),
                    EntityTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoreEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoreEntities_CoreEntityTypes_EntityTypeId",
                        column: x => x.EntityTypeId,
                        principalTable: "CoreEntityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoreEntityTypeProperties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    Descriptopn = table.Column<string>(nullable: true),
                    DbType = table.Column<string>(maxLength: 64, nullable: true),
                    DbPrecision = table.Column<string>(maxLength: 12, nullable: true),
                    ReferenceTableId = table.Column<int>(nullable: false),
                    PropertyOrder = table.Column<int>(nullable: false),
                    DefaultValue = table.Column<string>(nullable: true),
                    IsRequired = table.Column<bool>(nullable: false),
                    IsCustom = table.Column<bool>(nullable: false),
                    IsTranslatableValue = table.Column<bool>(nullable: false),
                    IsProtected = table.Column<bool>(nullable: false),
                    InsertUseId = table.Column<int>(nullable: false),
                    InserTime = table.Column<DateTime>(nullable: false),
                    InsertIpAddress = table.Column<string>(nullable: true),
                    LastUpdateUserId = table.Column<int>(nullable: false),
                    LastUpdateTime = table.Column<DateTime>(nullable: false),
                    LastUpdateIpAddress = table.Column<string>(nullable: true),
                    EntityTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoreEntityTypeProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoreEntityTypeProperties_CoreEntityTypes_EntityTypeId",
                        column: x => x.EntityTypeId,
                        principalTable: "CoreEntityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoreEntityPropertyValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TextValue = table.Column<string>(nullable: true),
                    IntValue = table.Column<int>(nullable: false),
                    DataTimeValue = table.Column<DateTime>(nullable: false),
                    DecimalValue = table.Column<decimal>(nullable: false),
                    GuidValue = table.Column<Guid>(nullable: false),
                    InsertUserId = table.Column<int>(nullable: false),
                    InsertTime = table.Column<DateTime>(nullable: false),
                    InsertIpAddress = table.Column<string>(nullable: true),
                    LastUpdateUserId = table.Column<int>(nullable: false),
                    LastUpdateTime = table.Column<DateTime>(nullable: false),
                    LastUpdateIpAddress = table.Column<string>(nullable: true),
                    EntityId = table.Column<Guid>(nullable: false),
                    EntityTypePropertyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoreEntityPropertyValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoreEntityPropertyValues_CoreEntities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "CoreEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CoreEntityPropertyValues_CoreEntityTypeProperties_EntityTypePropertyId",
                        column: x => x.EntityTypePropertyId,
                        principalTable: "CoreEntityTypeProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoreEntities_EntityTypeId",
                table: "CoreEntities",
                column: "EntityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CoreEntityPropertyValues_EntityId",
                table: "CoreEntityPropertyValues",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CoreEntityPropertyValues_EntityTypePropertyId",
                table: "CoreEntityPropertyValues",
                column: "EntityTypePropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_CoreEntityTypeProperties_EntityTypeId",
                table: "CoreEntityTypeProperties",
                column: "EntityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CoreEntityTypes_ParentEntityTypeId",
                table: "CoreEntityTypes",
                column: "ParentEntityTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoreEntityPropertyValues");

            migrationBuilder.DropTable(
                name: "CoreEntities");

            migrationBuilder.DropTable(
                name: "CoreEntityTypeProperties");

            migrationBuilder.DropTable(
                name: "CoreEntityTypes");
        }
    }
}
