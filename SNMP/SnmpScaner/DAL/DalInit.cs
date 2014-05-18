﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL.EfModels;
using DAL.Interfaces;
using DomainModel;
using StructureMap;

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
			});
		}

		private static void InitMapping()
		{
			Mapper.CreateMap<DevicesItems, DeviceItem>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdDeviceItemEntity))
				.ForMember(dest => dest.Oid, opt => opt.MapFrom(src => new Oid(src.DeviceItemEntity.Oid)))
				.ForMember(dest => dest.TimeDelta, opt => opt.MapFrom(src => src.DeltaT));

			Mapper.CreateMap<DeviceEntity, Device>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdDeviceEntity))
				.ForMember(dest => dest.Ip, opt => opt.MapFrom(src => IPAddress.Parse(src.Ip)))
				.ForMember(dest => dest.Port, opt => opt.MapFrom(src => src.Port))
				.ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.DevicesItems));
		}
	}
}