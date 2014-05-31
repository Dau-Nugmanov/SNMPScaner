
using DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DomainModel.EfModels;

namespace UI.Models
{
    public class DeviceTypeModel : IModelEntity<DeviceType>
    {
        public int IdDeviceType { get; set; }

        [Required(ErrorMessage="Это поле не может быть пустым")]
        [StringLength(50, ErrorMessage="Длина поля не может превышать 50 символов")]
        [DisplayName("Название типа устройств")]
        public string DeviceTypeName { get; set; }

        public DeviceType ToEfEntity()
        {
            return new DeviceType
            {
                IdDeviceType = IdDeviceType,
                DeviceTypeName = DeviceTypeName
            };
        }

        public static DeviceTypeModel ToModelEntity(DeviceType entity)
        {
            return new DeviceTypeModel
            {
                DeviceTypeName = entity.DeviceTypeName,
                IdDeviceType = entity.IdDeviceType
            };
        }
    }
}