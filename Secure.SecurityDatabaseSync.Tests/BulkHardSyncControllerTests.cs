using Microsoft.Extensions.DependencyInjection;
using SecurityDatabaseSync.DAL;
using System;
using Xunit;

namespace Secure.SecurityDatabaseSync.Tests
{
    public class BulkHardSyncControllerTests : IClassFixture<ServiceFixture>, IDisposable
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly ApplicationContext _context;
        private readonly ClearingDataContext _clearingDataContext;

        private readonly Random rnd = new Random();

        public BulkHardSyncControllerTests(ServiceFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;

            _context = _serviceProvider.GetRequiredService<ApplicationContext>();

            _clearingDataContext = new ClearingDataContext(_context);
        }

        public void Dispose()
        {
            _clearingDataContext.Clear();
        }

        [Fact]
        public void InsertDataAsync_Return_True()
        {

        }

        [Fact]
        public void ClearDataAsync_Return_True()
        {

        }

        [Fact]
        public void CopyDataAsync_Return_True()
        {

        }
    }
}
