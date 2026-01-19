using Books.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.core.Repositories
{
    public interface IUserRepository
    {
        List<Users> GetActivUsers();
        Users GetUserById(int id);
        Users RegisterUser(Users newUser);
        Users UpdateUser(int id, Users newUser);
        Users ChangeUserStatus(int id);
        void save();
    }
}
