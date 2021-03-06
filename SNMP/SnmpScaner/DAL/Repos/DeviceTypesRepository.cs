﻿using System;
using DomainModel.DalInterfaces;
using DomainModel.EfModels;

namespace DAL.Repos
{
    public class DeviceTypesRepository : BaseRepository<DeviceType>, IDeviceTypesRepo
    {
        private SnmpDbContext _context;
        public DeviceTypesRepository(SnmpDbContext context)
            :base(context)
        {
            _context = context;
        }

        public void Edit(DeviceType entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            var oldEntity = dbSet.Find(entity.IdDeviceType);
            if (oldEntity == null)
                throw new InvalidOperationException("В базе на найдена сущность с id " + entity.IdDeviceType);
            _context.Entry(oldEntity).CurrentValues.SetValues(entity);
        }
    }
}
