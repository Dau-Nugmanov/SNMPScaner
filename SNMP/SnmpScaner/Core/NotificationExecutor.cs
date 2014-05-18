using System;
using System.Collections.Generic;
using DomainModel;
using DomainModel.Interfaces;

namespace Core
{
	public class NotificationExecutor : INotificationExecutor
	{
		public void Execute(IEnumerable<Notification> notifications)
		{
			//throw new NotImplementedException();
		}
	}
}
