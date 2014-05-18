using DAL;
using DAL.EfModels;
using DAL.Interfaces;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using UI.Code;
using UI.Models;

namespace UI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

			DalInit.Init();
            ///////

            WarningsStorage.AddWarning(new WarningModel
            {
                DeviceName = "device1",
                IdDevice = 1,
                IdParameter = 11,
                //TimeStamp = DateTime.Now.AddDays(-10),
                DeviceMaker = "Samsung",
                DeviceModel = "ht23",
                Value = "Value!"
            });
            WarningsStorage.AddWarning(new WarningModel
            {
                DeviceName = "device1",
                IdDevice = 2,
                IdParameter = 11,
                //TimeStamp = DateTime.Now.AddDays(-8),
                DeviceMaker = "Samsung",
                DeviceModel = "fer321",
                Value = "Value!"
            }); WarningsStorage.AddWarning(new WarningModel
            {
                DeviceName = "device1",
                IdDevice = 3,
                IdParameter = 11,
                DeviceMaker = "HP",
                DeviceModel = "qqq",
                //TimeStamp = DateTime.Now.AddDays(-5),
                Value = "Value!"
            });
            /////
            SnmpDbContext context = new SnmpDbContext();
            if (!context.Database.Exists())
            {
                context.Database.Create();
                var pamella = new User
                {
                    Name = "Pamella",
                    LastName = "Anderson",
                    IsAdmin = true,
                    FirstEntry = false,
                    Password = "123",
                    Login = "pam",

                };


                var deviceType = new DeviceType()
                {
                    DeviceTypeName = "Printer"
                };

                var deviceType1 = new DeviceType()
                {
                    DeviceTypeName = "Server"
                };

                var deviceType2 = new DeviceType()
                {
                    DeviceTypeName = "Router"
                };
                context.Users.Add(pamella);
                context.SaveChanges();
                context.DeviceTypes.Add(deviceType);
                context.DeviceTypes.Add(deviceType1);
                context.DeviceTypes.Add(deviceType2);
                context.SaveChanges();
            }
        }

        void Application_PostAuthenticateRequest()
        {
            HttpCookie authCookie =
                Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket =
                                            FormsAuthentication.Decrypt(authCookie.Value);
                string[] roles = authTicket.UserData.Split(new Char[] { ',' });
                GenericPrincipal userPrincipal =
                                 new GenericPrincipal(new GenericIdentity(authTicket.Name),
                                                      roles);
                Context.User = userPrincipal;
            }
        }
    }
}