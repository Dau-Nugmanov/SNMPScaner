using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.EfModels;
using DAL.Interfaces;

namespace DAL
{
    public class DeviceItemsRepository : BaseRepository<DeviceItemEntity>, IDeviceItemsRepo
    {
        private SnmpDbContext _context;
        public DeviceItemsRepository(SnmpDbContext context)
            :base(context)
        {
            _context = context;
        }

        public IEnumerable<DeviceItemEntity> GetByIdModel(int idModel)
        {
            return dbSet.Where(t => t.IdModel == idModel);
        }

        public void Edit(DeviceItemEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            var oldEntity = dbSet.Find(entity.IdDeviceItemEntity);
            if (oldEntity == null)
                throw new InvalidOperationException("В базе на найдена сущность с id " + entity.IdDeviceItemEntity);
            _context.Entry(oldEntity).CurrentValues.SetValues(entity);
        }

        public IEnumerable<DeviceItemEntity> GetItemsByModelId(int id)
        {
            return dbSet
                .Include(t => t.Model)
                .Where(t => t.IdModel == id)
                .AsEnumerable<DeviceItemEntity>();
        }
    }
}
