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
    public class DefaultSyncControllerTests
    {
        private readonly Random rnd = new Random();
        private readonly string _databaseName = Guid.NewGuid().ToString();
        private readonly string _identifier = "test_";
        private readonly List<TestModel> firstData = new List<TestModel>();
        private readonly List<TestModel> secondData = new List<TestModel>();

        /// <summary>
        /// Тест на проверку получения списка данных.
        /// </summary>
        [Fact]
        public async void GetDataFromDatabaseAsync_Result_True()
        {
            //Arrange
            var operationResult = new List<TestModel>();

            //Act
            IDefaultSyncController defaultSyncController = new BulkDefaultSyncController();
            var result = await defaultSyncController.GetDataFromDatabaseAsync(_databaseName, _identifier);

            //Assert
            Assert.Equal(operationResult, result);
        }

        /// <summary>
        /// Тест на проверку добавления и удаления данных.
        /// </summary>
        [Fact]
        public async void AddOrDeleteDataToDatabaseAsync_Return_False()
        {
            //Arrange
            var operatinResult = false;
            var removeAdd = false;
            var removeDelete = true;

            //Act
            IDefaultSyncController defaultSyncController = new BulkDefaultSyncController();

            var resultAdd = await defaultSyncController.AddOrDeleteDataToDatabaseAsync(firstData, secondData, removeAdd, _databaseName);
            var resultDelete = await defaultSyncController.AddOrDeleteDataToDatabaseAsync(firstData, secondData, removeDelete, _databaseName);

            //Assert
            Assert.Equal(operatinResult, resultAdd);
            Assert.Equal(operatinResult, resultDelete);
        }

        /// <summary>
        /// Тест на проверку обновления данных.
        /// </summary>
        [Fact]
        public async void UpdateDataToServerAsync_Result_False()
        {
            //Arrange
            var operationResult = false;

            //Act
            IDefaultSyncController defaultSyncController = new BulkDefaultSyncController();
            var result = await defaultSyncController.UpdateDataToServerAsync(firstData, secondData, _databaseName);

            //Assert
            Assert.Equal(operationResult, result);
        }
    }
}
