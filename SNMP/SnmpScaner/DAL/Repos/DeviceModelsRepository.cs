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
    public class DeviceModelsRepository : BaseRepository<DeviceModel>, IDeviceModelsRepo
    {
        private SnmpDbContext _context;
        public DeviceModelsRepository(SnmpDbContext context)
            :base(context)
        {
            _context = context;
        }

        public void Edit(DeviceModel entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            var oldEntity = dbSet.Find(entity.IdDeviceModel);
            if (oldEntity == null)
                throw new InvalidOperationException("В базе на найдена сущность с id " + entity.IdDeviceModel);
            _context.Entry(oldEntity).CurrentValues.SetValues(entity);
            UpdateParameters(entity);
        }

        public IEnumerable<DeviceModel> GetAllByMakerId(int id)
        {
            return dbSet
                .Where(t => t.IdMaker == id)
                .AsEnumerable<DeviceModel>();
        }

        public override IEnumerable<DeviceModel> GetAll()
        {
            return dbSet
                .Include(t => t.Maker)
                .Include(t => t.DeviceType)
                .AsEnumerable<DeviceModel>();
        }

        private void UpdateParameters(DeviceModel entity)
        {
            DeviceItemsRepository itemsRepo = new DeviceItemsRepository(_context);
            var oldParameters = itemsRepo.GetByIdModel(entity.IdDeviceModel);
            UpdateParametersValues(entity.Items);
            RemoveItems(oldParameters.ToList(), entity.Items.ToList(), itemsRepo);
            AddItems(entity.Items.ToList(), itemsRepo);
        }

        private void RemoveItems(List<DeviceItemEntity> oldItems, List<DeviceItemEntity> newItems, DeviceItemsRepository itemsRepo)
        {
            var emailNotisRepo = new EmailNotificationsRepository(_context);
            var phoneNotisRepo = new PhoneNotificationsRepository(_context);
            foreach (var oldItem in oldItems)
            {
                if (!newItems.Select(t => t.IdDeviceItemEntity).Contains(oldItem.IdDeviceItemEntity))
                {
                    //emailNotisRepo.RemoveItemNotifications(oldItem.IdDeviceItemEntity);
                    //phoneNotisRepo.RemoveByItemId(oldItem.IdDeviceItemEntity);
                    itemsRepo.RemoveById(oldItem.IdDeviceItemEntity);
                }
            }
        }

        private void UpdateParametersValues(IEnumerable<DeviceItemEntity> parameters)
        {
            var paramsRepo = new DeviceItemsRepository(_context);
            parameters
                .Where(t => t.IdDeviceItemEntity != 0)
                .ToList()
                .ForEach(t =>
                {
                    paramsRepo.Edit(t);
                });
        }

        private void AddItems(List<DeviceItemEntity> newItems, DeviceItemsRepository itemRepo)
        {
            newItems
                .Where(t => t.IdDeviceItemEntity == 0)
                .ToList()
                .ForEach(t =>
                {
                    itemRepo.Add(t);
                });
        }
    }
}
