using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.DependencyResolvers.Ninject;
using DevFramework.Northwind.Entities.ComplexTypes;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace DevFramework.Northwind.WebApi.MessageHandlers
{
    public class AuthenticationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var token = request.Headers.GetValues("Authorization").FirstOrDefault();
                if (token != null)
                {
                    byte[] data = Convert.FromBase64String(token);
                    string decodedData = Encoding.UTF8.GetString(data);
                    string[] tokenValues = decodedData.Split(':');

                    IUserService userService = InstanceFactory.GetInstance<IUserService>();
                    User userByNameAndPw = userService.GetByUserNameAndPassword(tokenValues[0], tokenValues[1]);
                    if (userByNameAndPw != null)
                    {
                        List<UserRoles> userRoles = userService.GetUserRoles(userByNameAndPw);
                        IPrincipal principal = new GenericPrincipal(new GenericIdentity(tokenValues[0]), userRoles.Select(r => r.Role).ToArray());
                        Thread.CurrentPrincipal = principal;
                        HttpContext.Current.User = principal;
                    }
                }
            }
            catch
            {
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}