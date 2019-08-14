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
    public class BulkHardSyncControllerTests
    {
        private readonly Random rnd = new Random();
        private readonly string _databaseFirstName = Guid.NewGuid().ToString();
        private readonly string _databaseSecondName = Guid.NewGuid().ToString();
        private readonly string _identifier = "test_";

        [Fact]
        public async void InsertDataAsync_Return_True()
        {
            // Arrange
            var operationResult = true;

            // Act
            ISyncController syncController = new BulkHardSyncController();
            var resultFirst = await syncController.InsertDataAsync(_databaseFirstName, _identifier);
            var resultSecond = await syncController.InsertDataAsync(_databaseSecondName, _identifier);

            // Assert
            Assert.Equal(operationResult, resultFirst);
            Assert.Equal(operationResult, resultSecond);
        }

        [Fact]
        public async void ClearDataWithIdentifierAsync_Return_False()
        {
            // Arrange
            var operationResult = false;
            
            // Act
            ISyncController syncController = new BulkHardSyncController();
            var result = await syncController.ClearDataAsync(_databaseFirstName, string.Empty);

            Assert.Equal(operationResult, result);
        }

        [Fact]
        public async void CopyDataAsync_Return_True()
        {
            // Arrange
            var operationResult = true;

            // Act
            ISyncController syncController = new BulkHardSyncController();
            var result = await syncController.CopyDataAsync(_databaseFirstName, _databaseSecondName, _identifier);

            Assert.Equal(operationResult, result);
        }
    }
}
