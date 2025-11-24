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
            _UserRepository.save();
            return _UserRepository.RegisterUser(newUser);
        }

        public Users UpdateUser(int id,  Users newUser)
        {
            _UserRepository.save();
            return _UserRepository.UpdateUser(id, newUser);
        }
        public Users DeactivateUser(int id)
        {
            _UserRepository.save();
            return _UserRepository.DeactivateUser(id);
        }
    }
}
