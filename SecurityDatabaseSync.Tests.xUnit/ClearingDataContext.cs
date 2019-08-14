using SecurityDatabaseSync.DAL;

namespace Secure.SecurityDatabaseSync.Tests
{
    public class ClearingDataContext
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
