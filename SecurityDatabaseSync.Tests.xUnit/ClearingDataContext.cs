using SecurityDatabaseSync.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Secure.SecurityDatabaseSync.Tests
{
    class ClearingDataContext
    {
        private readonly ApplicationContext _context;

        public ClearingDataContext(ApplicationContext context)
        {
            _context = context;
        }

        public void Clear()
        {
            foreach (var entity in _context.TestModelTable)
            {
                _context.TestModelTable.Remove(entity);
            }

            _context.SaveChanges();
        }
    }
}
