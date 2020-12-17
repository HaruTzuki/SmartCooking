using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCooking.Data.Migrations
{
    public partial class AddNewFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RecipeOfTheDay",
                table: "SC_RecipeHeader",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "SC_UserFavoriteRecipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecipeHeaderId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SC_UserFavoriteRecipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SC_UserFavoriteRecipe_SC_RecipeHeader_RecipeHeaderId",
                        column: x => x.RecipeHeaderId,
                        principalTable: "SC_RecipeHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SC_UserFavoriteRecipe_SC_User_UserId",
                        column: x => x.UserId,
                        principalTable: "SC_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SC_UserFavoriteRecipe_RecipeHeaderId",
                table: "SC_UserFavoriteRecipe",
                column: "RecipeHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_SC_UserFavoriteRecipe_UserId",
                table: "SC_UserFavoriteRecipe",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SC_UserFavoriteRecipe");

            migrationBuilder.DropColumn(
                name: "RecipeOfTheDay",
                table: "SC_RecipeHeader");
        }
    }
}
