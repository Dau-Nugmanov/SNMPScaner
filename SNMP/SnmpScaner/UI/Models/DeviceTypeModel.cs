
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
    public class DeviceTypeModel : IModelEntity<DAL.EfModels.DeviceType>
    {
        public int IdDeviceType { get; set; }

        [Required(ErrorMessage="Это поле не может быть пустым")]
        [StringLength(50, ErrorMessage="Длина поля не может превышать 50 символов")]
        [DisplayName("Название типа устройств")]
        public string DeviceTypeName { get; set; }

        public DAL.EfModels.DeviceType ToEfEntity()
        {
            return new DeviceType
            {
                IdDeviceType = IdDeviceType,
                DeviceTypeName = DeviceTypeName
            };
        }

        public static DeviceTypeModel ToModelEntity(DAL.EfModels.DeviceType entity)
        {
            return new DeviceTypeModel
            {
                DeviceTypeName = entity.DeviceTypeName,
                IdDeviceType = entity.IdDeviceType
            };
        }
    }
}