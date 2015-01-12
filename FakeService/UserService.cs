using DataAccess;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork context;

        public UserService(IUserRepository userRepo, IUnitOfWork unitOfWork)
        {
            userRepository = userRepo;

            context = unitOfWork;

            if (userRepo != null) userRepository.UnitOfWork = unitOfWork;
        }

        public IEnumerable<Entities.User> GetActiveUsers()
        {
            return userRepository
                .GetAll()
                .Where(w=>!w.Deleted);
        }

        public Entities.User SearchByEmail(string email)
        {
            return userRepository
                .GetAll()
                .Where(w => w.Email.Equals(email))
                .FirstOrDefault();
        }

        public void SaveUser(Entities.User user)
        {
            if (!user.IsValid())
                return;

            if (userRepository.GetByID(user.UserID) != null)
            {
                userRepository.Update(user);
            }
            else
                userRepository.Add(user);
            context.Save();
        }

        public void DeleteUser(Entities.User user)
        {
            userRepository.Delete(user.UserID);
        }

        public IEnumerable<Entities.User> SearchByLastName(string lastName)
        {
            return userRepository
                .GetAll()
                .Where(w => w.FirstName.Contains(lastName));
        }
    }
}
