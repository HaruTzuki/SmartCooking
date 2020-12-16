using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCooking.Data.Migrations
{
    public partial class addrelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SC_RecipeDetail_UnitId",
                table: "SC_RecipeDetail",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_SC_Item_ItemCategoryId",
                table: "SC_Item",
                column: "ItemCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SC_Item_SC_ItemCategory_ItemCategoryId",
                table: "SC_Item",
                column: "ItemCategoryId",
                principalTable: "SC_ItemCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SC_RecipeDetail_SC_Unit_UnitId",
                table: "SC_RecipeDetail",
                column: "UnitId",
                principalTable: "SC_Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SC_Item_SC_ItemCategory_ItemCategoryId",
                table: "SC_Item");

            migrationBuilder.DropForeignKey(
                name: "FK_SC_RecipeDetail_SC_Unit_UnitId",
                table: "SC_RecipeDetail");

            migrationBuilder.DropIndex(
                name: "IX_SC_RecipeDetail_UnitId",
                table: "SC_RecipeDetail");

            migrationBuilder.DropIndex(
                name: "IX_SC_Item_ItemCategoryId",
                table: "SC_Item");
        }
    }
}
