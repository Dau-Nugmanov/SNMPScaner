using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DomainModel.DalInterfaces;
using DomainModel.EfModels;

namespace DAL.Repos
{
    public class DevicesRepository : BaseRepository<DeviceEntity>, IDevicesRepo
    {
        private SnmpDbContext _context;
        public DevicesRepository(SnmpDbContext context)
            :base(context)
        {
            _context = context;
        }

        public override void Add(DeviceEntity item)
        {
            base.Add(item);

        }

        public override IEnumerable<DeviceEntity> GetAll()
        {
            return dbSet
                .Include(t => t.Customer)
                .Include(t => t.DeviceModel)
                .Include(t => t.DevicesItems)
                .AsEnumerable<DeviceEntity>();
        }

        public override DeviceEntity GetById(object id)
        {
            return dbSet
                .Include(t => t.DeviceModel)
                .Include(t => t.DeviceModel.Maker)
                .Include(t => t.DevicesItems)
                .Include(t => t.DevicesItems.Select(x => x.DeviceItemEntity))
                .FirstOrDefault(t => t.IdDeviceEntity == (int)id);
        }

        public void Edit(DeviceEntity entity)
        {
             if (entity == null)
                throw new ArgumentNullException("entity");
            var oldEntity = dbSet.Find(entity.IdDeviceEntity);
            if (oldEntity == null)
                throw new InvalidOperationException("В базе на найдена сущность с id " + entity.IdDeviceEntity);
            _context.Entry(oldEntity).CurrentValues.SetValues(entity);
            UpdateDeviceParameters(entity);
        }

        private void UpdateDeviceParameters(DeviceEntity entity)
        {
            
            var itemsRepo = new DevicesItemsRepository(_context);
            var oldItems = itemsRepo.GetByDeviceId(entity.IdDeviceEntity);

            UpdatePrametersValues(oldItems, entity.DevicesItems);
            AddItems(oldItems.ToList(), entity.DevicesItems.ToList());
            RemoveItems(oldItems.ToList(), entity.DevicesItems.ToList());
        }

        private void AddItems(List<DevicesItems> oldItems, List<DevicesItems> newItems)
        {
            var devItemsRepo = new DevicesItemsRepository(_context);
            foreach (var item in newItems)
            {
                bool isExists = false;
                foreach (var oldItem in oldItems)
                {
                    if (oldItem.IdDeviceEntity == item.IdDeviceEntity && oldItem.IdDeviceItemEntity == item.IdDeviceItemEntity)
                    {
                        isExists = true;
                    }
                }
                if (!isExists)
                {
                    devItemsRepo.Add(item);
                }
            }
        }

        private void RemoveItems(List<DevicesItems> oldItems, List<DevicesItems> newItems)
        {
            var itemsRepo = new DevicesItemsRepository(_context);
            foreach (var item in oldItems)
            {
                if (!newItems.Any(t => t.IdDevicesItems == item.IdDevicesItems))
                {
                    RemoveItemsHistoryByItemId(item.IdDevicesItems);
                    itemsRepo.RemoveById(item.IdDevicesItems);
                }
            }
        }

        private void RemoveItemsHistoryByItemId(long id)
        {
            var itemsHistory = new DeviceItemsHistoryRepository(_context);
            itemsHistory
                .GetAll()
                .Where(t => t.IdDevicesItems == id)
                .ToList()
                .ForEach(t =>
                {
                    itemsHistory.RemoveById(t.IdItemHistory);
                });
        }

        private void UpdatePrametersValues(IEnumerable<DevicesItems> oldItems, IEnumerable<DevicesItems> newItems)
        {
            var devItemsRepo = new DevicesItemsRepository(_context);
            foreach (var item in newItems)
            {
                bool isExists = false;
                foreach (var oldItem in oldItems)
                {
                    if (oldItem.IdDevicesItems == item.IdDevicesItems)
                    {
                        isExists = true;
                    }
                }
                if (isExists)
                {
                    devItemsRepo.Edit(item);
                }
            }
        }
    }
}
