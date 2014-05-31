using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Repos;
using DomainModel;
using DomainModel.DalInterfaces;
using DomainModel.EfModels;
using DomainModel.Interfaces;
using DomainModel.Models;
using Lextm.SharpSnmpLib;
using StructureMap;
using Notification = DomainModel.EfModels.Notification;

namespace DAL
{
	public static class DalInit
	{
		public static void Init()
		{
			InitRepos();
			InitMapping();
		}

		private static void InitRepos()
		{
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
				x.For<IConfigRepo>().Use<ConfigRepo>();
				x.For<IHistoryRepo>().Use<HistoryRepo>();
			});
		}

		private static void InitMapping()
		{
			Mapper.CreateMap<DevicesItems, DeviceItem>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdDeviceItemEntity))
				.ForMember(dest => dest.Oid, opt => opt.MapFrom(src => new ObjectIdentifier(src.DeviceItemEntity.Oid)))
				.ForMember(dest => dest.TimeDelta, opt => opt.MapFrom(src => Convert.ToInt64(src.DeltaT)));

			Mapper.CreateMap<DeviceEntity, Device>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdDeviceEntity))
				.ForMember(dest => dest.Ip, opt => opt.MapFrom(src => IPAddress.Parse(src.Ip)))
				.ForMember(dest => dest.Port, opt => opt.MapFrom(src => src.Port))
				.ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.DevicesItems));

			Mapper.CreateMap<Notification, SubscriptionItem>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdNotification))
				.ForMember(dest => dest.TimeDelta, opt => opt.MapFrom(src => src.TimeDelta))
				.ForMember(dest => dest.ValueDelta, opt => opt.MapFrom(src => src.ValueDelta));

		}
	}
}
