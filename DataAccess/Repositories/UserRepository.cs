using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository : Repository, IUserRepository
    {
        public IEnumerable<Entities.User> GetAll()
        {
            return Context.Users.Select(s=> new Entities.User{
                UserID = s.UserID,
                FirstName = s.FirstName,
                LastName = s.LastName,
                BirthDay = s.BirthDay,
                Email = s.Email,
                Title = s.Title,
                Address = s.Address,
                Deleted = s.Deleted
            });
        }

        public Entities.User GetByID(Guid id)
        {
            if (Context.Users.Any(a => a.UserID == id))
            {
                return Context.Users.Where(w => w.UserID == id).Select(s => new Entities.User
                {
                    UserID = s.UserID,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    BirthDay = s.BirthDay,
                    Email = s.Email,
                    Title = s.Title,
                    Address = s.Address,
                    Deleted = s.Deleted
                }).FirstOrDefault();
            }
            else
                return null;
        }

        public void Add(Entities.User obj)
        {
            var user = new DataAccess.FakeDatabase.User();
            user.UserID = obj.UserID;
            user.FirstName = obj.FirstName;
            user.LastName = obj.LastName;
            user.BirthDay = obj.BirthDay;
            user.Email = obj.Email;
            user.Title = obj.Title;
            user.Address = obj.Address;
            user.Deleted = obj.Deleted;
            Context.Users.Add(user);
        }

        public void Update(Entities.User obj)
        {
            var user = Context.Users.SingleOrDefault(s=>s.UserID == obj.UserID);
            if (user != null)
            {
                user.FirstName = obj.FirstName;
                user.LastName = obj.LastName;
                user.BirthDay = obj.BirthDay;
                user.Email = obj.Email;
                user.Title = obj.Title;
                user.Address = obj.Address;
                user.Deleted = obj.Deleted;
            }
        }

        public void Delete(Guid id)
        {
            var user = Context.Users.SingleOrDefault(s => s.UserID == id);
            if (user != null)
            {
                user.Deleted = true;
            }
        }
    }
}
