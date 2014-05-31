using System.Collections.Generic;
using DomainModel.Models;

namespace DomainModel.Interfaces
{
	public interface INotificationExecutor
	{
		void Execute(IEnumerable<Notification> notifications);
	}
}
