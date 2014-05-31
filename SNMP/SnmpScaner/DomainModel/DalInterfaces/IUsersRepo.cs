using DomainModel.EfModels;

namespace DomainModel.DalInterfaces
{
    public interface IUsersRepo
    {
        User GetUserByLoginAndPassword(string login, string password);
        bool IsLoginExists(string login);
        void Edit(User user);
        void SetPasswordToDefault(int idUser);
    }
}
