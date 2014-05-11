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

            ObjectFactory.Initialize(x =>
            {
                x.For<ICustomersRepo>().Use<CustomersRepository>().Ctor<SnmpDbContext>("context");
                x.For<IDevicesRepo>().Use<DevicesRepository>().Ctor<SnmpDbContext>("context");
                x.For<IDeviceItemsRepo>().Use<DeviceItemsRepository>().Ctor<SnmpDbContext>("context");
                x.For<IDeviceModelsRepo>().Use<DeviceModelsRepository>().Ctor<SnmpDbContext>("context");
                x.For<IDevicesItemsRepo>().Use<DevicesItemsRepository>().Ctor<SnmpDbContext>("context");
                x.For<IDeviceTypesRepo>().Use<DeviceTypesRepository>().Ctor<SnmpDbContext>("context");
                x.For<IEmailNotificationsRepo>().Use<EmailNotificationsRepository>().Ctor<SnmpDbContext>("context");
                x.For<IMakersRepository>().Use<MakersRepository>().Ctor<SnmpDbContext>("context");
                x.For<IPhoneNotificationsRepo>().Use<PhoneNotificationsRepository>().Ctor<SnmpDbContext>("context");
                x.For<IPhoneNumbersRepo>().Use<PhoneNumbersRepository>().Ctor<SnmpDbContext>("context");
                x.For<IUsersRepo>().Use<UsersRepository>().Ctor<SnmpDbContext>("context");
            });

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