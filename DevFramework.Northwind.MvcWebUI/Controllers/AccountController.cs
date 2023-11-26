using DevFramework.Core.CrossCuttingConcerns.Security.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevFramework.Northwind.MvcWebUI.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public string Login()
        {
            AuthenticationHelper.CreateAuthCookie(new Guid(), 
                "tolgadeneme",
                "tolgadeneme@gmail.com", 
                DateTime.Now.AddDays(1), 
                new[] { "Admin" }, 
                false, 
                "Tolga", 
                "Kumbul");
            return "User is authenticated";
        }
    }
}