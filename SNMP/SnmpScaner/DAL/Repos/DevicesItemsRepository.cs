using DAL.EfModels;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAL
{
    public class DevicesItemsRepository : BaseRepository<DevicesItems>, IDevicesItemsRepo
    {
        private SnmpDbContext _context;
        public DevicesItemsRepository(SnmpDbContext context)
            :base(context)
        {
            _context = context;
        }

        public void Edit(DevicesItems entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            var oldEntity = dbSet.FirstOrDefault(t => t.IdDeviceEntity == entity.IdDeviceEntity 
                && t.IdDeviceItemEntity == entity.IdDeviceItemEntity);
            if (oldEntity == null)
                throw new InvalidOperationException("В базе на найдена сущность с id " + entity.IdDeviceEntity);
            _context.Entry(oldEntity).CurrentValues.SetValues(entity);
        }

        public IEnumerable<DevicesItems> GetByDeviceId(long id)
        {
            return dbSet
                .Include(t => t.DeviceEntity)
                .Include(t => t.DeviceItemEntity)
                .Where(t => t.IdDeviceEntity == id);
        }

        public override void RemoveById(object id)
        {
            var entity = dbSet.FirstOrDefault(t => t.IdDevicesItems == (long)id);
            if (entity == null)
                throw new InvalidOperationException("Нет такой сущности!");
            dbSet.Remove(entity);
        }

        public override IEnumerable<DevicesItems> GetAll()
        {
            return dbSet
                .Include(t => t.DeviceItemEntity);
        }
    }
}
