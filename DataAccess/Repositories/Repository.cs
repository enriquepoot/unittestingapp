using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public abstract class Repository
    {
        internal FakeDatabase Context { get; set; }

        public IUnitOfWork UnitOfWork
        {
            set
            {
                Context = value as FakeDatabase;
            }
        }
    }
}
