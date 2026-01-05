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
        public List<Users> GetActivUsers()
        {
            return _UserRepository.GetActivUsers();
        }
        public Users GetUserById(int id)
        {
            return _UserRepository.GetUserById(id);
        }
        public Users RegisterUser( Users newUser)
        {
            if (newUser == null)
            {
                return null;
            }
            var user = _UserRepository.RegisterUser(newUser);
            if (user != null)
            {
                _UserRepository.save();
            }
            return user ;
        }

        public Users UpdateUser(int id,  Users newUser)
        {
            var user= _UserRepository.UpdateUser(id, newUser);
            if (user != null)
            {
                _UserRepository.save();
            }
            return user;
        }
        public Users DeactivateUser(int id)
        {
            var user= _UserRepository.DeactivateUser(id);
            if (user != null)
            {
                _UserRepository.save();
            }
            return user;
        }
    }
}
