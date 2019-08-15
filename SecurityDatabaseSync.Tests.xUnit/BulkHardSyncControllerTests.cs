using SecurityDatabaseSync.BLL.Implementations;
using SecurityDatabaseSync.BLL.Interfaces;
using System;
using Xunit;

namespace Secure.SecurityDatabaseSync.Tests
{
    /// <summary>
    /// Класс для тестов класса BulkHurdSyncController.
    /// </summary>
    public class BulkHardSyncControllerTests
    {
        private readonly Random rnd = new Random();
        private readonly string _databaseFirstName = Guid.NewGuid().ToString();
        private readonly string _databaseSecondName = Guid.NewGuid().ToString();
        private readonly string _identifier = "test_";

        /// <summary>
        /// Тест на проверку добавления данных.
        /// </summary>
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

        /// <summary>
        /// Тест на проверку удаления данных.
        /// </summary>
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

        /// <summary>
        /// Тест на проверку обновления данных.
        /// </summary>
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
