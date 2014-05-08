using System.Collections.Generic;

namespace DomainModel.Interfaces
{
	public interface INotificationExecutor
	{
		void Execute(IEnumerable<Notification> notifications);
	}
}
