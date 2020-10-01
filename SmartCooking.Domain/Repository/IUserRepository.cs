using SmartCooking.Infastructure.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCooking.Data.Repository
{
    public interface IUserRepository
    {
        User GetUser(int Id);
        List<User> GetUsers();
        bool InsertUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);

    }
}
