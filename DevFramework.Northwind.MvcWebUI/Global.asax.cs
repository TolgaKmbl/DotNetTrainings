using DevFramework.Core.CrossCuttingConcerns.Security.Web;
using DevFramework.Core.Utilities.Mvc.Infrastructure;
using DevFramework.Northwind.Business.DependencyResolvers.Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace DevFramework.Northwind.MvcWebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(new ServiceModule(),/*new BusinessModule(),*/ new AutoMapperModule()));
        }

        public override void Init()
        {
            PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }

        private void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            try
            {
                var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    var encryptedTicket = authCookie.Value;
                    if (!String.IsNullOrEmpty(encryptedTicket))
                    {
                        var ticket = FormsAuthentication.Decrypt(encryptedTicket);
                        var security = new SecurityUtilities();
                        var identity = security.CookieToIdentity(ticket);
                        var principal = new GenericPrincipal(identity, identity.Roles);

                        HttpContext.Current.User = principal;
                        Thread.CurrentPrincipal = principal;
                    }
                }
            } catch (Exception) 
            {

            }
        }
    }
}
