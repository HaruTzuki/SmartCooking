using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SmartCooking.Infastructure.Products;
using SmartCooking.Infastructure.Recipes;
using SmartCooking.Infastructure.Security;

namespace SmartCooking.Data.Context
{
	/// <summary>
	/// Κλάση σύνδεσης βάσης με το ORM Entity Framework.
	/// https://entityframework.net/knowledge-base/51648844/sqlite-codefirst-example
	/// </summary>
	public class MyDbContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// Δημιουργεί το αντικείμενο της σύνδεσης με ένα Connection String που του ορίζουμε και ανοίγουμε το Connection
			SqliteConnectionStringBuilder connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "SmartCooking.db" };
			string connectionString = connectionStringBuilder.ToString();
			SqliteConnection connection = new SqliteConnection(connectionString);
			optionsBuilder.UseSqlite(connection);
		}

		// Δηλώνουμε ως DbSet τα Tables που θα γίνουν Generate στην SQLite.
		public DbSet<Item> SC_Item { get; set; }
		public DbSet<ItemCategory> SC_ItemCategory { get; set; }
		public DbSet<Unit> SC_Unit { get; set; }
		public DbSet<RecipeHeader> SC_RecipeHeader { get; set; }
		public DbSet<RecipeDetail> SC_RecipeDetail { get; set; }
		public DbSet<RecipeImage> SC_RecipeImage { get; set; }
		public DbSet<User> SC_User { get; set; }
	}
}