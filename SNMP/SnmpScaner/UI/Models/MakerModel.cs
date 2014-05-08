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
    public class MakerModel : IModelEntity<DAL.EfModels.Maker>
    {
        public int IdMaker { get; set; }

        [Required(ErrorMessage="*")]
        [StringLength(50, ErrorMessage = "Поле не может быть больше 50 символов")]
        [DisplayName("Наименование производителя")]
        public string MakerName { get; set; }

        public DAL.EfModels.Maker ToEfEntity()
        {
            return new DAL.EfModels.Maker
            {
                IdMaker = IdMaker,
                MakerName = MakerName
            };
        }

        public static MakerModel ToModel(DAL.EfModels.Maker maker)
        {
            return new MakerModel
            {
                IdMaker = maker.IdMaker,
                MakerName = maker.MakerName
            };
        }
    }
}