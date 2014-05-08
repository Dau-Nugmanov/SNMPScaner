
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
    public class CustomerModel : IModelEntity<DAL.EfModels.Customer>
    {
        public int IdCustomer { get; set; }

        [Required(ErrorMessage="*")]
        [StringLength(50, ErrorMessage="Поле не может быть больше 50 символов")]
        [DisplayName("Наименование заказчика")]
        public string CustomerName { get; set; }

        public DAL.EfModels.Customer ToEfEntity()
        {
            return new DAL.EfModels.Customer
            {
                CustomerName = CustomerName,
                IdCustomer = IdCustomer
            };
        }

        public static CustomerModel ToModelEntity(DAL.EfModels.Customer customer)
        {
            return new CustomerModel
            {
                IdCustomer = customer.IdCustomer,
                CustomerName = customer.CustomerName
            };
        }
    }
}