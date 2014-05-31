using DomainModel.EfModels;

namespace DAL.Repos
{
    public class DeviceItemsHistoryRepository : BaseRepository<DeviceItemHistory>
    {
        private SnmpDbContext _context;
        public DeviceItemsHistoryRepository(SnmpDbContext context)
            :base(context)
        {
            _context = context;
        }
    }
}
