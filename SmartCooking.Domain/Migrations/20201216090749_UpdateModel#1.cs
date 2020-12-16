using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCooking.Data.Migrations
{
    public partial class UpdateModel1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SC_RecipeDetail_ItemId",
                table: "SC_RecipeDetail",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_SC_RecipeDetail_SC_Item_ItemId",
                table: "SC_RecipeDetail",
                column: "ItemId",
                principalTable: "SC_Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SC_RecipeDetail_SC_Item_ItemId",
                table: "SC_RecipeDetail");

            migrationBuilder.DropIndex(
                name: "IX_SC_RecipeDetail_ItemId",
                table: "SC_RecipeDetail");
        }
    }
}
