using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class FakeDatabase : IUnitOfWork
    {
        #region Entities
        public List<User> Users { get; private set; }
        #endregion

        #region Inner classes
        public class User
        {
            #region Properties
            public Guid UserID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime BirthDay { get; set; }
            public string Email { get; set; }
            public string Title { get; set; }
            public string Address { get; set; }
            public bool Deleted { get; set; }
            #endregion
        }
        #endregion

        #region Constructor
        public FakeDatabase()
        {
            Users = new List<User>()
            {
                new User{ FirstName ="Peter", LastName="Smith", Address="NY", BirthDay = DateTime.Now.AddDays(-2580), Email="pet@myemail.com", Title="Dr.", UserID = Guid.NewGuid() }
                ,new User{ FirstName ="Loue", LastName="Lancaster", Address="Tulsa", BirthDay = DateTime.Now.AddDays(-3000), Email="loue@myemail.com", Title="Lic.", UserID = Guid.NewGuid(), Deleted = true }
                ,new User{ FirstName ="Dexter", LastName="Wilson", Address="Florida", BirthDay = DateTime.Now.AddDays(-1300), Email="dex@myemail.com", Title="Sr.", UserID = Guid.NewGuid() }
                ,new User{ FirstName ="Richard", LastName="Thomas", Address="Canada", BirthDay = DateTime.Now.AddDays(-4000), Email="rick@myemail.com", Title="", UserID = Guid.NewGuid() }
                ,new User{ FirstName ="Michael", LastName="Moore", Address="New Mexico", BirthDay = DateTime.Now.AddDays(-800), Email="mike@myemail.com", Title="", UserID = Guid.NewGuid() }
            };
        }
        #endregion

        public void Save()
        {
            Console.WriteLine("Saved");
        }
    }
}
