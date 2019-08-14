using Microsoft.Extensions.DependencyInjection;
using SecurityDatabaseSync.BLL.Implementations;
using SecurityDatabaseSync.BLL.Interfaces;
using SecurityDatabaseSync.DAL;
using SecurityDatabaseSync.DAL.Models;
using System;
using System.Collections.Generic;
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
        public async void InsertDataAsync_Return_True()
        {
            var databaseName = "dataBase";
            var identifier = "1";
            var operationResult = true;

            ISyncController syncController = new BulkHardSyncController();

            var result = await syncController.InsertDataAsync(databaseName, identifier);

            Assert.Equal(operationResult, result);
        }

        [Fact]
        public async void ClearDataAsync_Return_True()
        {
            var databaseName = "DataBase";
            var identifier = "1";
            var listTestModel = new List<TestModel>();
            var expected = 10;
            var operationResult = true;

            ISyncController syncController = new BulkHardSyncController();

            for (int i = 0; i < expected; i++)
            {
                listTestModel.Add(new TestModel
                {
                    Code = identifier + Guid.NewGuid().ToString(),
                    Name = Guid.NewGuid().ToString().Substring(0, 8),
                    Current = DateTime.Now
                });
            }

            _context.TestModelTable.AddRange(listTestModel);
            _context.SaveChanges();

            var result = await syncController.ClearDataAsync(databaseName);

            Assert.Equal(operationResult, result);
        }

        [Fact]
        public void CopyDataAsync_Return_True()
        {

        }
    }
}
