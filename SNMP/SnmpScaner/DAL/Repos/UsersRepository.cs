using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Repos;
using DomainModel.DalInterfaces;
using DomainModel.EfModels;

namespace DAL
{
    public class UsersRepository : BaseRepository<User>, IUsersRepo
    {
        SnmpDbContext _context;
        public UsersRepository(SnmpDbContext context)
            :base(context)
        {
            _context = context;
        }

        public override IEnumerable<User> GetAll()
        {
            return dbSet
                .Include(t => t.Emails)
                .Include(t => t.PhoneNumbers)
                .AsEnumerable<User>();
        }

        public override User GetById(object id)
        {
            return dbSet
                .Include(t => t.Emails)
                .Include(t => t.PhoneNumbers)
                .FirstOrDefault(t => t.IdUser == (int)id);
        }

        public override void Add(User item)
        {
            item.FirstEntry = true;
            item.Password = "123456";
            base.Add(item);
        }

        public User GetUserByLoginAndPassword(string login, string password)
        {
            return dbSet
                .Include(t => t.Emails)
                .Include(t => t.PhoneNumbers)
                .FirstOrDefault(t => t.Login == login && t.Password == password);
        }

        public bool IsLoginExists(string login)
        {
            return dbSet.FirstOrDefault(t => t.Login == login) == null ? false : true;
        }

        public void Edit(User user)
        {
            var oldUser = dbSet.FirstOrDefault(t => t.IdUser == user.IdUser);
            if (oldUser == null)
                throw new InvalidOperationException("В бд не найден пользователь с id " + user.IdUser);
            oldUser.IsAdmin = user.IsAdmin;
            oldUser.LastName = user.LastName;
            oldUser.Login = user.Login;
            oldUser.Name = user.Name;

            if (user.Emails != null && user.Emails.Any())
                EditEmails(user.Emails.ToList());
            if (user.PhoneNumbers != null && user.PhoneNumbers.Any())
                EditPhoneNumbers(user.PhoneNumbers.ToList());
        }

        private void EditPhoneNumbers(List<PhoneNumber> numbers)
        {
            PhoneNumbersRepository numRepo = new PhoneNumbersRepository(_context);
            foreach (var number in numbers)
            {
                if (numRepo.IsPhoneNumberExists(number.Number))
                    continue;
                numRepo.Add(number);
            }
        }

        private void EditEmails(List<EmailEntity> emails)
        {
            EmailsRepository emailsRepo = new EmailsRepository(_context);
            foreach (var email in emails)
            {
                if (emailsRepo.IsEmailExists(email.Email))
                    continue;
                emailsRepo.Add(email);
            }
        }

        public void SetPasswordToDefault(int idUser)
        {
            var user = dbSet.Find(idUser);
            if (user == null)
                throw new InvalidOperationException("Не найден пользователь с id " + idUser);
            user.Password = "123456";
            user.FirstEntry = true;
        }
    }
}
