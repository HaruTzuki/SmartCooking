using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCooking.Data.Migrations
{
	public partial class DraftRecipe : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "SC_DraftRecipeDetail",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					ItemId = table.Column<int>(nullable: true),
					Quantity = table.Column<float>(nullable: false),
					UnitId = table.Column<int>(nullable: true),
					RecipeHeaderId = table.Column<int>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_SC_DraftRecipeDetail", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "SC_DraftRecipeHeader",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					Name = table.Column<string>(nullable: true),
					Description = table.Column<string>(nullable: true),
					Code = table.Column<string>(nullable: true),
					Tags = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_SC_DraftRecipeHeader", x => x.Id);
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "SC_DraftRecipeDetail");

			migrationBuilder.DropTable(
				name: "SC_DraftRecipeHeader");
		}
	}
}