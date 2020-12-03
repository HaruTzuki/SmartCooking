using Microsoft.EntityFrameworkCore;
using SmartCooking.Data.Context;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCooking.Data.Domain
{
	/// <summary>
	/// Κλάση που είναι υπεύθυνση για τις κλήσεις στη βάση που αφορούν τα Unit.
	/// </summary>
	public class UserRepository : IUserRepository
	{
		private readonly MyDbContext context;

		public UserRepository(MyDbContext context)
		{
			this.context = context;
		}

		/// <summary>
		/// Διαγραφή Object
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public async Task<bool> DeleteUser(User user)
		{
			context.SC_User.Remove(user);

			if (await context.SaveChangesAsync() <= 0)
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Επιστροφή ενώς Object
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public async Task<User> GetUser(int Id)
		{
			return await context.SC_User.FirstOrDefaultAsync(x => x.Id == Id);
		}

		/// <summary>
		/// Επιστροφή λίστα από Object
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<User>> GetUsers()
		{
			return await context.SC_User.ToListAsync();
		}

		/// <summary>
		/// Προσθήκη Object
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public async Task<bool> InsertUser(User user)
		{
			context.SC_User.Add(user);

			if (await context.SaveChangesAsync() <= 0)
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Ενημέρωση Object
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public async Task<bool> UpdateUser(User user)
		{
			context.SC_User.Update(user);

			if (await context.SaveChangesAsync() <= 0)
			{
				return false;
			}

			return true;
		}
	}
}