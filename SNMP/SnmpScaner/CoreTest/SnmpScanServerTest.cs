using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using NUnit.Framework;

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
	}
}

