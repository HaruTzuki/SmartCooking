using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SmartCooking.Infastructure.Products;
using SmartCooking.Infastructure.Recipes;
using SmartCooking.Infastructure.Security;

namespace SmartCooking.Data.Context
{
	public class MyDbContext : DbContext
	{

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "SmartCooking.db" };
			var connectionString = connectionStringBuilder.ToString();
			var connection = new SqliteConnection(connectionString);

			optionsBuilder.UseSqlite(connection);
		}

		public DbSet<Item> SC_Item { get; set; }
		public DbSet<ItemCategory> SC_ItemCategory { get; set; }
		public DbSet<Unit> SC_Unit { get; set; }
		public DbSet<RecipeHeader> SC_RecipeHeader { get; set; }
		public DbSet<RecipeDetail> SC_RecipeDetail { get; set; }
		public DbSet<User> SC_User { get; set; }

	}
}
