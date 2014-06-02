using System.Collections.ObjectModel;
using System.Net;
using Core;
using DAL;
using DomainModel.EfModels;
using DomainModel.Interfaces;
using DotNetOpenAuth.Messaging;
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
using WebGrease.Css.Extensions;
using DeviceModel = DomainModel.EfModels.DeviceModel;

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

            //_server = new SnmpScanServer();

            InitStartParameters();
            
        }

        private void InitStartParameters()
        {
            using (SnmpDbContext context = new SnmpDbContext())
            {
                InitReportParametersDataTypes(context);
                context.SaveChanges();
            }
        }

        private void InitReportParametersDataTypes(SnmpDbContext context)
        {
            //datatypes
            context.ReportParametersDataTypes.Add
              (
                  new ReportParameterDataType
                  {
                      DataTypeName = "Числовой"
                  }
              );
            context.ReportParametersDataTypes.Add
                (
                    new ReportParameterDataType
                    {
                        DataTypeName = "Строковый"
                    }
                );
            context.ReportParametersDataTypes.Add
                (
                    new ReportParameterDataType
                    {
                        DataTypeName = "Параметр устройства"
                    }
                );
            context.ReportParametersDataTypes.Add
                (
                    new ReportParameterDataType
                    {
                        DataTypeName = "Устройство"
                    }
                );
            context.ReportParametersDataTypes.Add
                (
                    new ReportParameterDataType
                    {
                        DataTypeName = "Дата"
                    }
                );
            context.ReportParametersDataTypes.Add
                (
                    new ReportParameterDataType
                    {
                        DataTypeName = "Заказчик"
                    }
                );
            context.ReportParametersDataTypes.Add
                (
                    new ReportParameterDataType
                    {
                        DataTypeName = "Производитель оборудования"
                    }
                );
            context.ReportParametersDataTypes.Add
                (
                    new ReportParameterDataType
                    {
                        DataTypeName = "Модель устройства"
                    }
                );
        }

        private void InitWarningsModels()
        {
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
            InitWarningsModels();
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

				user.PhoneNumbers = new List<PhoneNumber>
				{
					new PhoneNumber {Number = "+7 123 456 7890"}
				};

				user.Emails = new List<EmailEntity>
				{
					new EmailEntity {Email = "SNMPEmailTest@gmail.com"}
				};
				
				var deviceType = context.DeviceTypes.Add(new DeviceType { DeviceTypeName = "Тип устройства" });
				var maker = context.Makers.Add(new Maker {MakerName = "Изготовитель"});
				var deviceModel = context.Models.Add(
					new DeviceModel
					{
						DeviceType = deviceType, 
						Maker = maker,
						ModelName = "Модель устройства"
					});

				var oids = new List<Tuple<string, string, DeviceItemEntityDataType>>
				{
#region OIDS
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.1.1",   "1",		DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.1.2",   "2",		DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.2.1",   "3",		DeviceItemEntityDataType.OctetString),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.2.2",   "4",		DeviceItemEntityDataType.OctetString),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.3.1",   "5",		DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.3.2",   "6",		DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.4.1",   "7",		DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.4.2",   "8",		DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.5.1",   "9",		DeviceItemEntityDataType.Gauge32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.5.2",   "0",		DeviceItemEntityDataType.Gauge32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.6.1",   "11",	DeviceItemEntityDataType.OctetString),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.6.2",   "12",	DeviceItemEntityDataType.OctetString),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.7.1",   "13",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.7.2",   "14",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.8.1",   "15",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.8.2",   "16",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.9.1",   "17",	DeviceItemEntityDataType.TimeTicks),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.9.2",   "18",	DeviceItemEntityDataType.TimeTicks),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.10.1",  "19",	DeviceItemEntityDataType.Counter32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.10.2",  "20",	DeviceItemEntityDataType.Counter32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.11.1",  "21",	DeviceItemEntityDataType.Counter32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.11.2",  "22",	DeviceItemEntityDataType.Counter32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.12.1",  "23",	DeviceItemEntityDataType.Counter32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.12.2",  "24",	DeviceItemEntityDataType.Counter32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.13.1",  "25",	DeviceItemEntityDataType.Counter32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.13.2",  "26",	DeviceItemEntityDataType.Counter32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.14.1",  "27",	DeviceItemEntityDataType.Counter32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.14.2",  "28",	DeviceItemEntityDataType.Counter32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.15.1",  "29",	DeviceItemEntityDataType.Counter32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.15.2",  "30",	DeviceItemEntityDataType.Counter32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.16.1",  "31",	DeviceItemEntityDataType.Counter32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.16.2",  "32",	DeviceItemEntityDataType.Counter32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.17.1",  "33",	DeviceItemEntityDataType.Counter32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.17.2",  "34",	DeviceItemEntityDataType.Counter32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.18.1",  "35",	DeviceItemEntityDataType.Counter32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.18.2",  "36",	DeviceItemEntityDataType.Counter32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.19.1",  "37",	DeviceItemEntityDataType.Counter32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.19.2",  "38",	DeviceItemEntityDataType.Counter32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.20.1",  "39",	DeviceItemEntityDataType.Counter32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.20.2",  "40",	DeviceItemEntityDataType.Counter32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.21.1",  "41",	DeviceItemEntityDataType.Gauge32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.21.2",  "42",	DeviceItemEntityDataType.Gauge32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.22.1",  "43",	DeviceItemEntityDataType.Unknown),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.2.2.1.22.2",  "44",	DeviceItemEntityDataType.Unknown),
#endregion
				};
			
				var deviceItemEntities = context.Parameters.AddRange(
					oids.Select(o => new DeviceItemEntity
					{
						Model = deviceModel,
						Oid = o.Item1,
						Name = o.Item2,
						DataType = o.Item3,
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
						Notifications = GetNotification(user),
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
                
                InitReportParametersDataTypes(context);
				context.SaveChanges();
				//
				//context.DeviceTypes.Add(deviceType1);
				//context.DeviceTypes.Add(deviceType2);
				//context.SaveChanges();
			}
	    }


	    private Notification[] GetNotification(User user)
	    {
		    return new[]{new Notification
			{
				EmailNotifications = user.Emails.Select(e=>new EmailNotification{ EmailEntity = e}).ToList(),
				PhoneNotifications = user.PhoneNumbers.Select(p => new PhoneNotification { PhoneNumber = p }).ToList(),
				TimeDelta = 1,
				ValueDelta = 10,
				Hi = 20,
				Lo = 10,
			}};
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