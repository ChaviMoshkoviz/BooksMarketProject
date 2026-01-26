using books;
using Books.core.Entities;
using Books.core.Repositories;
using Books.core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.service
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _UserRepository;
        public UserService(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }
        public async Task< List<Users>> GetActivUsers()
        {
            return await _UserRepository.GetActivUsers();
        }
        public async Task< Users> GetUserById(int id)
        {
            return await _UserRepository.GetUserById(id);
        }
        public async Task< Users> RegisterUser( Users newUser)
        {
            if (newUser == null)
            {
                return null;
            }
            var user = await _UserRepository.RegisterUser(newUser);
            if (user != null)
            {
               await _UserRepository.save();
            }
            return user ;
        }

        public async Task< Users> UpdateUser(int id,  Users newUser)
        {
            var user= await _UserRepository.UpdateUser(id, newUser);
            if (user != null)
            {
              await  _UserRepository.save();
            }
            return user;
        }
        public async Task< Users> ChangeUserStatus(int id)
        {
            var user= await _UserRepository.ChangeUserStatus(id);
            if (user != null)
            {
                await _UserRepository.save();
            }
            return user;
        }
    }
}
