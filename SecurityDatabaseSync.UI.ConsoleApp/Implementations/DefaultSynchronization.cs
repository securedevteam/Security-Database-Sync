using SecurityDatabaseSync.BLL.Interfaces;
using SecurityDatabaseSync.DAL.Models;
using SecurityDatabaseSync.UI.ConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecurityDatabaseSync.UI.ConsoleApp.Implementations
{
    /// <summary>
    /// Класс для управления жесткой синхронизацией.
    /// </summary>
    public class DefaultSynchronization : ISyncStart
    {
        private readonly IDefaultSyncController _defaultSyncController;

        /// <summary>
        /// Конструктор с параметром.
        /// </summary>
        /// <param name="defaultSyncController">интерфейс.</param>
        public DefaultSynchronization(IDefaultSyncController defaultSyncController)
        {
            _defaultSyncController = defaultSyncController;
        }

        /// <summary>
        /// Запуск синхронизации.
        /// </summary>
        /// <returns></returns>
        public async Task SyncStart()
        {
            while (true)
            {
                Console.WriteLine("-add, -delete, -update, -exit");
                Console.Write("Введите команду: ");
                var param = Console.ReadLine();

                switch (param)
                {
                    case "-add":
                        {
                            var (client, server, targetDatabaseName) = await FillInitialData();

                            var resultAdd = await _defaultSyncController.AddOrDeleteDataToDatabaseAsync(client, server, false, targetDatabaseName);

                            OperationResult(resultAdd);
                        }
                        break;

                    case "-delete":
                        {
                            var (client, server, targetDatabaseName) = await FillInitialData();

                            var resultDelete = await _defaultSyncController.AddOrDeleteDataToDatabaseAsync(server, client, true, targetDatabaseName);

                            OperationResult(resultDelete);
                        }
                        break;

                    case "-update":
                        {
                            var (client, server, targetDatabaseName) = await FillInitialData();

                            var resultUpdate = await _defaultSyncController.UpdateDataToServerAsync(client, server, targetDatabaseName);

                            OperationResult(resultUpdate);
                        }
                        break;

                    case "-exit":
                        {
                            Console.WriteLine();
                            return;
                        }

                    default:
                        {
                            Console.WriteLine(">> Введена неверная команда!\n");
                        }
                        break;
                }
            }
        }

        private async Task<(List<TestModel>, List<TestModel>, string)> FillInitialData()
        {
            var result = (client: new List<TestModel>(), server: new List<TestModel>(), targetDatabaseName: string.Empty);

            Console.WriteLine();

            Console.Write("Введите название базы данных для экспорта: ");
            var clientDatabaseName = Console.ReadLine();

            Console.Write("Введите название базы данных для импорта: ");
            var serverDatabaseName = Console.ReadLine();

            Console.Write("Введите идентификатор: ");
            var identifier = Console.ReadLine();

            Console.Write("Введите название целевой базы данных: ");
            result.targetDatabaseName = Console.ReadLine();

            result.client = await _defaultSyncController.GetDataFromDatabaseAsync(clientDatabaseName, identifier);
            result.server = await _defaultSyncController.GetDataFromDatabaseAsync(serverDatabaseName, identifier);

            return result;
        }

        private void OperationResult(bool result)
        {
            // TODO: Доделать в зависимости от результата.

            Console.WriteLine(">> Операция выполнена!\n");
        }
    }
}
