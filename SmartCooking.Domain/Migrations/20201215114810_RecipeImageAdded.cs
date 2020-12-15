using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCooking.Data.Migrations
{
    public partial class RecipeImageAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SC_RecipeImage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecipeId = table.Column<int>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    FileSize = table.Column<long>(nullable: false),
                    ContentType = table.Column<string>(nullable: true),
                    ContentValue = table.Column<string>(nullable: true),
                    ProfileImage = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SC_RecipeImage", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SC_RecipeImage");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "SC_User",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "SC_User",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
