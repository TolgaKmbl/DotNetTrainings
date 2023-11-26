using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.ComplexTypes;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.Business.Concrete.Managers
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<User> GetAll()
        {
            return _userDal.GetAll();
        }

        public User GetById(int userId)
        {
            return _userDal.Get(u => u.Id == userId);
        }

        public User GetByUserNameAndPassword(string username, string password)
        {
            return _userDal.Get(u => u.UserName == username & u.Password == password);
        }

        public List<UserRoles> GetUserRoles(User user)
        {
            return _userDal.GetUserRoles(user);
        }

        public User Insert(User user)
        {
            return _userDal.Add(user);
        }

        public User Update(User user)
        {
            return _userDal.Update(user);
        }
    }
}
