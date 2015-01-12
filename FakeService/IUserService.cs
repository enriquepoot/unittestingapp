using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeService
{
    public interface IUserService
    {
        IEnumerable<User> GetActiveUsers();
        User SearchByEmail(string email);
        void SaveUser(User user);
        void DeleteUser(User user);
        IEnumerable<User> SearchByLastName(string lastName);
    }
}
