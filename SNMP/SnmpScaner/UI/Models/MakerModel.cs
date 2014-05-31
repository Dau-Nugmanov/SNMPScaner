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
    public class MakerModel : IModelEntity<Maker>
    {
        public int IdMaker { get; set; }

        [Required(ErrorMessage="*")]
        [StringLength(50, ErrorMessage = "Поле не может быть больше 50 символов")]
        [DisplayName("Наименование производителя")]
        public string MakerName { get; set; }

        public Maker ToEfEntity()
        {
            return new Maker
            {
                IdMaker = IdMaker,
                MakerName = MakerName
            };
        }

        public static MakerModel ToModel(Maker maker)
        {
            return new MakerModel
            {
                IdMaker = maker.IdMaker,
                MakerName = maker.MakerName
            };
        }
    }
}