using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace DevFramework.Core.CrossCuttingConcerns.Security.Web
{
    public class SecurityUtilities
    {
        public Identity CookieToIdentity(FormsAuthenticationTicket ticket) 
        {
            string[] cookieData = ticket.UserData.Split('|');
            return new Identity
            { 
                Id = new Guid(cookieData[4]),
                Name = ticket.Name,
                FirstName = cookieData[2],
                LastName = cookieData[3],
                Email = cookieData[0],
                Roles = cookieData[1].Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries)

            };            
        }
    }
}
