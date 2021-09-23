using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryPattern.Service
{
    public class UserService
    {
        private static IRepository<User> repository = new DapperRepository<User>();

        public static User GetUserById(string Id)
        {
            return repository.Get(Id);
        }

        public static void InsertUser(User user)
        {
            repository.Insert(user);
        }
    }
}
