using DomainModel.EfModels;

namespace DomainModel.DalInterfaces
{
    public interface ICustomersRepo
    {
        void Edit(Customer entity);
    }
}
