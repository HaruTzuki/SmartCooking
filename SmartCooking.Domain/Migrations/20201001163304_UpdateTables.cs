using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCooking.Data.Migrations
{
	public partial class UpdateTables : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "SC_Item",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					Name = table.Column<string>(nullable: true),
					ItemCategoryId = table.Column<int>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_SC_Item", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "SC_ItemCategory",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					Name = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_SC_ItemCategory", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "SC_RecipeDetail",
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
					table.PrimaryKey("PK_SC_RecipeDetail", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "SC_RecipeHeader",
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
					table.PrimaryKey("PK_SC_RecipeHeader", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "SC_Unit",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					Name = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_SC_Unit", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "SC_User",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					Username = table.Column<string>(nullable: true),
					Password = table.Column<string>(nullable: true),
					Email = table.Column<string>(nullable: true),
					FamilyName = table.Column<string>(nullable: true),
					IsAdmin = table.Column<bool>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_SC_User", x => x.Id);
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "SC_Item");

			migrationBuilder.DropTable(
				name: "SC_ItemCategory");

			migrationBuilder.DropTable(
				name: "SC_RecipeDetail");

			migrationBuilder.DropTable(
				name: "SC_RecipeHeader");

			migrationBuilder.DropTable(
				name: "SC_Unit");

			migrationBuilder.DropTable(
				name: "SC_User");
		}
	}
}
