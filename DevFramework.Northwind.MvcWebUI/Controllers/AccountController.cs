using DevFramework.Core.CrossCuttingConcerns.Security.Web;
using DevFramework.Northwind.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevFramework.Northwind.MvcWebUI.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }


        // GET: Account
        public string Login(string username, string password)
        {
            Entities.Concrete.User user = _userService.GetByUserNameAndPassword(username, password);            
            bool doesUserExist = user != null;
            if (doesUserExist)
            {
                List<Entities.ComplexTypes.UserRoles> userRoles = _userService.GetUserRoles(user);
                AuthenticationHelper.CreateAuthCookie(new Guid(),
                                user.UserName,
                                user.Email,
                                DateTime.Now.AddDays(1),
                                userRoles.Select(ur => ur.Role).ToArray(),
                                false,
                                user.FirstName,
                                user.LastName);
                return "User is authenticated";
            }

            return "User is NOT authenticated";

        }
    }
}