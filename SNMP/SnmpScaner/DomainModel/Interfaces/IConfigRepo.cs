using System.Collections.Generic;

namespace DomainModel.Interfaces
{
	public interface IConfigRepo
	{
		Device[] GetAllDevices();
	}
}
