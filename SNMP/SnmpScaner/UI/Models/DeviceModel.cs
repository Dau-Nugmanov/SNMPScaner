
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
    public class DeviceModel : IModelEntity<DAL.EfModels.DeviceEntity>
    {
        public long IdDevice { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым")]
        [StringLength(50, ErrorMessage = "Значение не может превышать 50 символов")]
        [DisplayName("Название устройства")]
        public string Name { get; set; }

        [Range(1, 65535, ErrorMessage = "Значение должно быть числом в диапазон от 1 до 65535")]
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [DisplayName("Порт")]
        public int Port { get; set; }

        [StringLength(50, ErrorMessage = "Значение не может превышать 50 символов")]
        [Required(ErrorMessage="Поле не может быть пустым")]
        [DisplayName("Логин")]
        public string Login { get; set; }

        [StringLength(50, ErrorMessage = "Значение не может превышать 50 символов")]
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [DisplayName("Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(100, ErrorMessage="Поле не может быть больше 100 символов")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        public string Description { get; set; }

        public int IdCustomer { get; set; }

        public int IdModel { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым")]
        [DisplayName("Ip")]
        [RegularExpression(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}", ErrorMessage="Введите корректный IP адрес")]
        public string Ip_s { get; set; }

        public bool IsActive { get; set; }

        public ItemDeviceConcrete[] Items { get; set; }

        public DAL.EfModels.DeviceEntity ToEfEntity()
        {
            List<DevicesItems> devicesItems = new List<DevicesItems>();
            if (Items != null)
                devicesItems.AddRange(Items.Select(t => new DevicesItems { IdDeviceEntity = IdDevice, IdDeviceItemEntity = t.Id, DeltaT = t.DeltaT }));

            return new DAL.EfModels.DeviceEntity
            {
                IdDeviceEntity = IdDevice,
                DateCreate = DateTime.Now,
                Description = Description,
                DevicesItems = devicesItems,
                IdCustomer = IdCustomer,
                IdModel = IdModel,
                Ip = Ip_s,
                Login = Login,
                Name = Name,
                Password = Password,
                Port = Port,
                IsActive = IsActive
            };
        }

        public static DeviceModel ToModelEntity(DAL.EfModels.DeviceEntity device)
        {
            List<ItemDeviceConcrete> items = new List<ItemDeviceConcrete>();
            if (device.DevicesItems != null)
            {
                items.AddRange(device.DevicesItems
                    .Select(t => new ItemDeviceConcrete { Id = t.IdDeviceItemEntity
                        , IdDevice = t.IdDeviceEntity
                        , DeltaT = t.DeltaT
                        , Name = t.DeviceItemEntity.Name
                        , Oid = t.DeviceItemEntity.Oid }));
            }
            return new DeviceModel
            {
                Description = device.Description,
                IdCustomer = device.IdCustomer,
                IdDevice = device.IdDeviceEntity,
                IdModel = device.IdModel,
                Ip_s = device.Ip,
                IsActive = device.IsActive,
                Items = items.ToArray(),
                Login = device.Login,
                Name = device.Name,
                Password = device.Password,
                Port = device.Port,
            };
        }
    }

    public class ItemDeviceConcrete
    {
        public long Id { get; set; }

        public long IdDevice { get; set; }

        public string Name { get; set; }

        public string Oid { get; set; }

        [Required(ErrorMessage="Требуется поле DeltaT")]
        [Range(1, int.MaxValue, ErrorMessage = "Значение должно быть целым числом")]
        public int DeltaT { get; set; }
    }
}