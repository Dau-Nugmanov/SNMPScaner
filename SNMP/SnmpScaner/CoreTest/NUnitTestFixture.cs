using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using NUnit.Framework;

namespace CoreTest
{
	[TestFixture]
	public class CoreTest
	{
		[Test]
		public void Test()
		{
			SnmpScanServer t = new SnmpScanServer();
			t.Run();
		}
	}
}

