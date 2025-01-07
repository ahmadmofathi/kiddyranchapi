using KiddyRanchWeb.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepo _userRepo;

        public UserManager(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public IEnumerable<UserDTO> GetUsers()
        {
            IEnumerable<User> usersDB = _userRepo.GetUsers();
            return usersDB.Select(u => new UserDTO
            {
                Id = u.Id,
                Name = u.Name,
                UserName = u.UserName
            });
        }
        public string Add(UserAddDTO userRequest)
        {
            User user = new User
            {
                Name = userRequest.Name,
                UserName = userRequest.UserName,
            };
            _userRepo.Add(user);
            _userRepo.SaveChanges();
            return user.Id;
        }

        public bool Delete(string id)
        {
            User? user = _userRepo.GetById(id);
            if (user == null)
            {
                return false;
            }
            _userRepo.Delete(user);
            _userRepo.SaveChanges();
            return true;
        }

        public UserDTO GetUser(string id)
        {
            User? user = _userRepo.GetById(id);
            if (user is null)
            {
                return null;
            }

            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,

            };
        }
        public User GetUserById(string id)
        {
            User? user = _userRepo.GetById(id) as User;
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public bool Update(UserDTO userRequest)
        {
            if (userRequest.Id == null)
            {
                return false;
            }
            User? user = _userRepo.GetById(userRequest.Id);
            if (user is null)
            {
                return false;
            }
            user.Name = userRequest.Name;

            _userRepo.Update(user);
            _userRepo.SaveChanges();
            return true;

        }
    }
}
