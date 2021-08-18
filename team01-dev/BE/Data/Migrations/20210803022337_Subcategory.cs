using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Subcategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubcategoryTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnglishName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateByDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateByDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteByDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubcategoryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subcategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubcategoryTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateByDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateByDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteByDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subcategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subcategories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subcategories_SubcategoryTypes_SubcategoryTypeId",
                        column: x => x.SubcategoryTypeId,
                        principalTable: "SubcategoryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_CategoryId",
                table: "Subcategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_ProductId",
                table: "Subcategories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_SubcategoryTypeId",
                table: "Subcategories",
                column: "SubcategoryTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subcategories");

            migrationBuilder.DropTable(
                name: "SubcategoryTypes");
        }
    }
}
