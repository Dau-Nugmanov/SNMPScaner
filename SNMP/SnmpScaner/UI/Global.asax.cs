using System.Collections.ObjectModel;
using System.Net;
using Core;
using DAL;
using DAL.EfModels;
using DAL.Interfaces;
using DomainModel.Interfaces;
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
using DeviceModel = DAL.EfModels.DeviceModel;

namespace UI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
	    private static SnmpScanServer _server;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

			DalInit.Init();
			ObjectFactory.Configure(i => i.For<INotificationExecutor>().Use<NotificationExecutor>());
			InitTestSettings();

	        _server = new SnmpScanServer();

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
        }

	    private void InitTestSettings()
	    {
			SnmpDbContext context = new SnmpDbContext();
			if (!context.Database.Exists())
			{
				context.Database.Create();
				var user = context.Users.Add(
					new User
					{
						Name = "Pamella",
						LastName = "Anderson",
						IsAdmin = true,
						FirstEntry = false,
						Password = "123",
						Login = "pam",

					});
				
				var deviceType = context.DeviceTypes.Add(new DeviceType { DeviceTypeName = "Тип устройства" });
				var maker = context.Makers.Add(new Maker {MakerName = "Изготовитель"});
				var deviceModel = context.Models.Add(
					new DeviceModel
					{
						DeviceType = deviceType, 
						Maker = maker,
						ModelName = "Модель устройства"
					});

				var oids = new List<Tuple<string,string>>
				{
#region OIDS
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.1.1",   "1"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.1.2",   "2"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.2.1",   "3"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.2.2",   "4"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.3.1",   "5"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.3.2",   "6"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.4.1",   "7"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.4.2",   "8"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.5.1",   "9"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.5.2",   "0"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.6.1",   "11"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.6.2",   "12"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.7.1",   "13"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.7.2",   "14"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.8.1",   "15"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.8.2",   "16"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.9.1",   "17"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.9.2",   "18"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.10.1",  "19"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.10.2",  "20"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.11.1",  "21"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.11.2",  "22"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.12.1",  "23"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.12.2",  "24"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.13.1",  "25"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.13.2",  "26"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.14.1",  "27"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.14.2",  "28"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.15.1",  "29"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.15.2",  "30"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.16.1",  "31"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.16.2",  "32"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.17.1",  "33"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.17.2",  "34"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.18.1",  "35"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.18.2",  "36"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.19.1",  "37"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.19.2",  "38"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.20.1",  "39"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.20.2",  "40"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.21.1",  "41"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.21.2",  "42"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.22.1",  "43"),
					new Tuple<string, string>("1.3.6.1.2.1.2.2.1.22.2",  "44"),
#endregion
				};

				var deviceItemEntities = context.Parameters.AddRange(
					oids.Select(o => new DeviceItemEntity
					{
						Model = deviceModel,
						Oid = o.Item1,
						Name = o.Item2
					}));

				var customer = context.Customers.Add(new Customer {CustomerName = "Владелец"});

				var deviceEntity = context.Devices.Add(new DeviceEntity
				{
					Name = "Конкретное устройство",
					Customer = customer,
					DeviceModel = deviceModel,
					Ip = Dns.GetHostAddresses("demo.snmplabs.com")[0].ToString(),
					Port = 161,
					Login = "Login",
					Password = "Password",
					IsActive = true,
					Description = "Описание",
					DateCreate = DateTime.Now,

					DevicesItems = deviceItemEntities.Select(i=> new DevicesItems
					{
						DeltaT = 60,
						DeviceItemEntity = i,
					}).ToList()
				});

				


				//var deviceType = new DeviceType()
				//{
				//	DeviceTypeName = "Printer"
				//};

				//var deviceType1 = new DeviceType()
				//{
				//	DeviceTypeName = "Server"
				//};

				//var deviceType2 = new DeviceType()
				//{
				//	DeviceTypeName = "Router"
				//};
				
				context.SaveChanges();
				//
				//context.DeviceTypes.Add(deviceType1);
				//context.DeviceTypes.Add(deviceType2);
				//context.SaveChanges();
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