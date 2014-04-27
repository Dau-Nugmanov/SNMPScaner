using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using Core;
using DomainModel;
using DomainModel.Interfaces;
using Lextm.SharpSnmpLib;
using Moq;
using NUnit.Framework;
using StructureMap;

namespace CoreTest
{
	[TestFixture]
	public class SnmpScanServerTest
	{
		[Test]
		public void Test()
		{
			var server = new SnmpScanServer();
			Assert.DoesNotThrow(server.Run);
		}

		[Test]
		public void TestAsync()
		{
			var server = new SnmpScanServer();
			Assert.DoesNotThrow(server.RunAsync);
		}

		[Test]
		public void TestInit()
		{
			var devices = new[]
			{
				new Device
				{
					Name = "Test",
					Ip = Dns.GetHostAddresses("demo.snmplabs.com")[0],
					Port = 161,
					Timeout = 6000,
					VersionCode = VersionCode.V1,
					Items = new List<DeviceItem>
					{
						new DeviceItem {Oid = new ObjectIdentifier(".1.3.6.1.2.1.1.1.0")},
						new DeviceItem {Oid = new ObjectIdentifier(".1.3.6.1.2.1.1.3.0")},
						new DeviceItem {Oid = new ObjectIdentifier(".1.3.6.1.2.1.1.5.0")}
					}
				}
			};

			ObjectFactory.Configure(item =>item.For<IConfigRepo>().Add(Mock.Of<IConfigRepo>(m => m.GetAllDevices() == devices)));

			var server = new SnmpScanServer();
			Thread.Sleep(6000);
			var values = server.GetAllValues();
			
			Assert.AreEqual(values.Count(), devices.SelectMany(d=>d.Items).Count());
			Assert.AreEqual(values.First(i => i.Oid.ToString() == ".1.3.6.1.2.1.1.1.0").Value, "SunOS zeus.snmplabs.com 4.1.3_U1 1 sun4m");
			Assert.AreEqual(values.First(i => i.Oid.ToString() == ".1.3.6.1.2.1.1.5.0").Value, "zeus.snmplabs.com");
		} 
	}
}

