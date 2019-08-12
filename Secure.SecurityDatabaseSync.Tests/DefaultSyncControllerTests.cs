using Microsoft.Extensions.DependencyInjection;
using SecurityDatabaseSync.DAL;
using System;
using Xunit;

namespace Secure.SecurityDatabaseSync.Tests
{
    public class DefaultSyncControllerTests : IClassFixture<ServiceFixture>, IDisposable
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly ApplicationContext _context;
        private readonly ClearingDataContext _clearingDataContext;

        private readonly Random rnd = new Random();

        public DefaultSyncControllerTests(ServiceFixture fixture)
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
        public void GetDataFromDatabaseAsync_Return_True()
        {

        }

        [Fact]
        public void AddOrDeleteDataToDatabaseAsync_Return_True()
        {

        }

        [Fact]
        public void UpdateDataToServerAsync_Return_True()
        {

        }
    }
}
