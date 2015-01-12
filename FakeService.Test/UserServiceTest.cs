using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entities;
using System.Collections.Generic;
using Moq;
using DataAccess.Interfaces;
using DataAccess;
using System.Linq;

namespace FakeService.Test
{
    [TestClass]
    public class UserServiceTest
    {
        #region Test

        Guid fixedGuid;
        List<User> users;
        Mock<IUserRepository> userRepo;
        Mock<IUnitOfWork> context;
        UserService service;

        private void InitializeDependencies()
        {
            userRepo = new Mock<IUserRepository>();
            context = new Mock<IUnitOfWork>();
            fixedGuid = Guid.NewGuid();
            users = new List<User>()
            {
                new User{ FirstName ="Peter", LastName="Smith", Address="NY", BirthDay = DateTime.Now.AddDays(-2580), Email="pet@myemail.com", Title="Dr.", UserID = Guid.NewGuid() }
                ,new User{ FirstName ="Loue", LastName="Lancaster", Address="Tulsa", BirthDay = DateTime.Now.AddDays(-3000), Email="loue@myemail.com", Title="Lic.", UserID = Guid.NewGuid(), Deleted = true }
                ,new User{ FirstName ="Dexter", LastName="Wilson", Address="Florida", BirthDay = DateTime.Now.AddDays(-1300), Email="dex@myemail.com", Title="Sr.", UserID = fixedGuid }
                ,new User{ FirstName ="Richard", LastName="Thomas", Address="Canada", BirthDay = DateTime.Now.AddDays(-4000), Email="rick@myemail.com", Title="", UserID = Guid.NewGuid() }
                ,new User{ FirstName ="Michael", LastName="Moore", Address="New Mexico", BirthDay = DateTime.Now.AddDays(-800), Email="mike@myemail.com", Title="", UserID = Guid.NewGuid() }
            };
        }

        private void InitializeServiceForTest()
        {
            service = new UserService(userRepo.Object, context.Object);
        }

        [TestMethod]
        public void TestGetActiveUsers()
        {
            InitializeDependencies();

            userRepo.Setup(s => s.GetAll()).Returns(() => users);

            InitializeServiceForTest();

            try
            {
                var result = service.GetActiveUsers();

                userRepo.Verify(v => v.GetAll(), Times.Once);

                Assert.IsTrue(result.Count() == 4);
            }
            catch
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public void TestValidUpdateUser()
        {
            InitializeDependencies();

            userRepo.Setup(s => s.GetByID(It.IsAny<Guid>())).Returns(() => users.Where(w => w.UserID.Equals(fixedGuid)).FirstOrDefault());

            InitializeServiceForTest();

            try
            {
                var user = new User
                {
                    FirstName = "Peter",
                    LastName = "Smith",
                    Address = "NY",
                    BirthDay = DateTime.Now.AddDays(-2580),
                    Email = "pet@myemail.com",
                    Title = "Dr.",
                    UserID = fixedGuid
                };

                service.SaveUser(user);

                userRepo.Verify(v => v.GetByID(It.IsAny<Guid>()), Times.Once);
                userRepo.Verify(v => v.Update(It.IsAny<User>()), Times.Once);
                userRepo.Verify(v => v.Add(It.IsAny<User>()), Times.Never);

                context.Verify(v => v.Save(), Times.Once);
            }
            catch
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public void TestInvalidUpdateUser()
        {
            InitializeDependencies();

            userRepo.Setup(s => s.GetByID(It.IsAny<Guid>())).Returns(() => users.Where(w => w.UserID.Equals(fixedGuid)).FirstOrDefault());

            InitializeServiceForTest();

            try
            {
                var user = new User
                {
                    FirstName = "",
                    LastName = "Smith",
                    Address = "NY",
                    BirthDay = DateTime.Now.AddDays(-2580),
                    Email = "pet@myemail.com",
                    Title = "Dr.",
                    UserID = fixedGuid
                };

                service.SaveUser(user);

                userRepo.Verify(v => v.GetByID(It.IsAny<Guid>()), Times.Never);
                userRepo.Verify(v => v.Update(It.IsAny<User>()), Times.Never);
                userRepo.Verify(v => v.Add(It.IsAny<User>()), Times.Never);

                context.Verify(v => v.Save(), Times.Never);
            }
            catch
            {
                Assert.Fail();
            }

        }

        #endregion
    }

}
