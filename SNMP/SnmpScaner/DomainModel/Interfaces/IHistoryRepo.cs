using System;
using Lextm.SharpSnmpLib;

namespace DomainModel.Interfaces
{
	public interface IHistoryRepo
	{
		void Save(long id, ISnmpData value, DateTime timestamp);
	}
}
