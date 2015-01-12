using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Entities.Test
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void TestUserIsInvalid()
        {
            var user = new User { };
            Assert.IsFalse(user.IsValid());
        }

        [TestMethod]
        public void TestUserIsValid()
        {
            var user = new User { UserID = Guid.NewGuid(), FirstName = "Alfonso", LastName = "Loria", Email = "alf@email.com", Title="Dr." };
            Assert.IsTrue(user.IsValid());
        }
    }
}
