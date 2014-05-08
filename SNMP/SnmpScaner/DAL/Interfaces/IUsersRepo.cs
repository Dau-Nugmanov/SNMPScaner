using DAL.EfModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUsersRepo
    {
        User GetUserByLoginAndPassword(string login, string password);
        bool IsLoginExists(string login);
        void Edit(User user);
        void SetPasswordToDefault(int idUser);
    }
}
