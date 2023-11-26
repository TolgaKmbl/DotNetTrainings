using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.Business.Abstract
{
    public interface IUserService
    {
        List<User> GetAll();
        User GetById(int userId);
        User GetByUserNameAndPassword(string username, string password);
        User Insert(User user);
        User Update(User user);
    }
}
