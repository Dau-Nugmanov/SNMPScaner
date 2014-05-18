﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.EfModels;
using DAL.Interfaces;

namespace DAL
{
    public class DevicesRepository : BaseRepository<DeviceEntity>, IDevicesRepo
    {
        private SnmpDbContext _context;
        public DevicesRepository(SnmpDbContext context)
            :base(context)
        {
            _context = context;
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
                if (!newItems.Any(t => t.IdDeviceEntity == item.IdDeviceEntity && t.IdDeviceItemEntity == item.IdDeviceItemEntity))
                {
                    itemsRepo.RemoveById(new IdsWrap { IdDevice = item.IdDeviceEntity, IdItem = item.IdDeviceItemEntity });
                }
            }
        }

        private void UpdatePrametersValues(IEnumerable<DevicesItems> oldItems, IEnumerable<DevicesItems> newItems)
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
                if (isExists)
                {
                    devItemsRepo.Edit(item);
                }
            }
        }

    }
}