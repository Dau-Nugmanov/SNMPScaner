using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;

namespace Core
{
	public class SnmpScanServer
	{
		public void Run()
		{
			var ipAddress = Dns.GetHostAddresses("demo.snmplabs.com");
			var result = Messenger.Get(VersionCode.V1,
						   new IPEndPoint(ipAddress[0], 161),
						   new OctetString("public"),
						   new List<Variable>
						   {
							   new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.1.0")),
							   new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.3.0")),
							   new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.5.0")),
						   },
						   6000);
		}
	}
}
