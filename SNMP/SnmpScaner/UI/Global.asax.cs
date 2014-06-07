using System.Collections.ObjectModel;
using System.Net;
using System.Threading;
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

        public static SnmpScanServer SnmpServer;

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

            SnmpServer = new SnmpScanServer();

            InitStartParameters();
        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
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

                var deviceType = context.DeviceTypes.Add(new DeviceType { DeviceTypeName = "Принтер" });
                var maker = context.Makers.Add(new Maker { MakerName = "Самсунг" });
                var deviceModel = context.Models.Add(
                    new DeviceModel
                    {
                        DeviceType = deviceType,
                        Maker = maker,
                        ModelName = "LaserJet 1233"
                    });

                var oids = new List<Tuple<string, string, DeviceItemEntityDataType>>
				{
#region OIDS
                    new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.1.0",   "DeviceName",		DeviceItemEntityDataType.OctetString),
                    new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.2.0",   "Остаток тонера",		DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.3.0",   "Время работы",		DeviceItemEntityDataType.TimeTicks),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.4.0",   "Текущий статус",		DeviceItemEntityDataType.OctetString),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.5.0",   "Текущая страница",		DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.6.0",   "Осталось страниц",		DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.7.0",   "Текущее качество",		DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.8.0",   "IP адрес",		DeviceItemEntityDataType.OctetString),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.0",   "Доп IP",		DeviceItemEntityDataType.OctetString),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.1 ",   "Почта",		DeviceItemEntityDataType.OctetString),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.2 ",   "Пароль",	DeviceItemEntityDataType.OctetString),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.3 ",   "Test 12",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.4 ",   "Test 13",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.5 ",   "Test 14",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.6 ",   "Test 15",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.7 ",   "Test 16",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.8 ",   "Test 17",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.9 ",   "Test 18",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.10",  "Test 19",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.11",  "Test 20",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.12",  "Test 21",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.13",  "Test 22",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.14",  "Test 23",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.15",  "Test 24",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.16",  "Test 25",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.17",  "Test 26",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.18",  "Test 27",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.19",  "Test 28",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.20",  "Test 29",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.21",  "Test 30",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.22",  "Test 31",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.23",  "Test 32",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.24",  "Test 33",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.25",  "Test 34",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.26",  "Test 35",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.27",  "Test 36",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.28",  "Test 37",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.29",  "Test 38",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.30",  "Test 39",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.31",  "Test 40",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.32",  "Test 41",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.33",  "Test 42",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.34",  "Test 43",	DeviceItemEntityDataType.Integer32),
					new Tuple<string, string, DeviceItemEntityDataType>("1.3.6.1.2.1.1.9.1.2.35",  "Test 44",	DeviceItemEntityDataType.Integer32),
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

                var customer = context.Customers.Add(new Customer { CustomerName = "ГазНефтьАлмазБанк" });

                var deviceEntity = context.Devices.Add(new DeviceEntity
                {
                    Name = "Принтер в бухгалтерии",
                    Customer = customer,
                    DeviceModel = deviceModel,
					Ip = "127.0.0.1",
                    Port = 162,
                    Login = "Login",
                    Password = "Password",
                    IsActive = true,
                    Description = "Описание",
                    DateCreate = DateTime.Now,

                    DevicesItems = deviceItemEntities.Select(i => new DevicesItems
                    {
                        DeltaT = 60,
                        DeviceItemEntity = i,
                        DeltaV = 10,
                        Notifications = GetNotification(user),
                    }).ToList()
                });

				InitReportParametersDataTypes(context);
                context.SaveChanges();
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