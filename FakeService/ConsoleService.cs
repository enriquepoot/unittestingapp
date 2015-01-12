using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeService
{
    class ConsoleService
    {
        private UserService service;

        public ConsoleService()
        {
            IUnitOfWork unit = new FakeDatabase();
            IUserRepository repository = new UserRepository();
            service = new UserService(repository, unit);
        }

        public void RunService()
        {
            var users = service.GetActiveUsers();
            Console.WriteLine(string.Format("GetActiveUsers() call count {0}:", users.Count()));
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(users));

            var addUser = new User
            {
                UserID = Guid.NewGuid(),
                FirstName = "Olive",
                LastName = "Thompson",
                Email = "oli@email.com"
            };
            service.SaveUser(addUser);
            Console.WriteLine(string.Format("GetActiveUsers() call count {0}:", users.Count()));
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(users));
        }

        static void Main(string[] args)
        {
            var program = new ConsoleService();
            program.RunService();
            Console.ReadKey();
        }
    }
}
