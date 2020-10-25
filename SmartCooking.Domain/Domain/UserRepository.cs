using Microsoft.EntityFrameworkCore;
using SmartCooking.Data.Context;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCooking.Data.Domain
{

    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext context;

        public UserRepository(MyDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteUser(User user)
        {
            context.SC_User.Remove(user);

            if(await context.SaveChangesAsync() <= 0)
            {
                return false;
            }

            return true;
        }

        public async Task<User> GetUser(int Id)
        {
            return await context.SC_User.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await context.SC_User.ToListAsync();
        }

        public async Task<bool> InsertUser(User user)
        {
            context.SC_User.Add(user);

            if(await context.SaveChangesAsync() <= 0)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateUser(User user)
        {
            context.SC_User.Update(user);

            if(await context.SaveChangesAsync() <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
