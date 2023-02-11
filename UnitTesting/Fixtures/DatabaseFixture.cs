using DataEF.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting.Fixtures
{
    public class DatabaseFixture 
    {
        private readonly MLApiDbContext _context;
        public DatabaseFixture() {
          
            _context = new MLApiDbContext(new DbContextOptionsBuilder<MLApiDbContext>().UseInMemoryDatabase(databaseName: "MLApiDb").Options);
            _context.Database.EnsureCreated();
        }

        public MLApiDbContext GetContext()
        {
            return _context;
        }

    }
}
