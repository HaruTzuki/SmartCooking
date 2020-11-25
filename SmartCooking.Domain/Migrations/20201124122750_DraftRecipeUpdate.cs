using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCooking.Data.Migrations
{
	public partial class DraftRecipeUpdate : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "SC_DraftRecipeDetail");

			migrationBuilder.DropTable(
				name: "SC_DraftRecipeHeader");

			migrationBuilder.AddColumn<int>(
				name: "RecipeType",
				table: "SC_RecipeHeader",
				nullable: false,
				defaultValue: 0);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "RecipeType",
				table: "SC_RecipeHeader");

			migrationBuilder.CreateTable(
				name: "SC_DraftRecipeDetail",
				columns: table => new
				{
					Id = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					ItemId = table.Column<int>(type: "INTEGER", nullable: true),
					Quantity = table.Column<float>(type: "REAL", nullable: false),
					RecipeHeaderId = table.Column<int>(type: "INTEGER", nullable: true),
					UnitId = table.Column<int>(type: "INTEGER", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_SC_DraftRecipeDetail", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "SC_DraftRecipeHeader",
				columns: table => new
				{
					Id = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					Code = table.Column<string>(type: "TEXT", nullable: true),
					Description = table.Column<string>(type: "TEXT", nullable: true),
					Name = table.Column<string>(type: "TEXT", nullable: true),
					Tags = table.Column<string>(type: "TEXT", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_SC_DraftRecipeHeader", x => x.Id);
				});
		}
	}
}
