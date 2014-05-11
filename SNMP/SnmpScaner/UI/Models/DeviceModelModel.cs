
using DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DAL.EfModels;

namespace UI.Models
{
    public class DeviceModelModel : IModelEntity<DAL.EfModels.DeviceModel>
    {
        public int IdDeviceModel { get; set; }

        public int IdDeviceType { get; set; }

        public int IdMaker { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "Поле не может быть больше 50 символов")]
        [DisplayName("Наименование модели")]
        public string ModelName { get; set; }

        public ItemModel[] Items { get; set; }

        public DAL.EfModels.DeviceModel ToEfEntity()
        {
            List<DeviceItemEntity> items = new List<DeviceItemEntity>();
            if(Items != null && Items.Any())
                items.AddRange(Items.ToList().Select(t => t.ToEfEntity()));
            return new DAL.EfModels.DeviceModel
            {
                IdDeviceModel = IdDeviceModel,
                IdDeviceType = IdDeviceType,
                IdMaker = IdMaker,
                ModelName = ModelName,
                Items = items
            };
        }

        public static DeviceModelModel ToModelEntity(DAL.EfModels.DeviceModel model)
        {
            List<ItemModel> items = new List<ItemModel>();
            if(model.Items != null && model.Items.Any())
                items.AddRange(model.Items.Select(t => ItemModel.ToModelEntity(t)));
            return new DeviceModelModel
            {
                IdDeviceModel = model.IdDeviceModel,
                ModelName = model.ModelName,
                IdDeviceType = model.IdDeviceType,
                IdMaker = model.IdMaker,
                Items = items.ToArray()
            };
        }
    }
}