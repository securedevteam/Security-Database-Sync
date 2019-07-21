using SecurityDatabaseSync.BLL.Interfaces;
using SecurityDatabaseSync.Core;
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
                Console.WriteLine(Constants.COMMAND_DEFAULT);
                Console.Write(Constants.ENTER_COMMAND);
                var param = Console.ReadLine();

                switch (param)
                {
                    case "-add":
                        {
                            var (client, server, targetDatabaseName) = await FillInitialData();

                            var resultAdd = await _defaultSyncController.AddOrDeleteDataToDatabaseAsync(client, server, false, targetDatabaseName);

                            StaticMethods.OperationResult(resultAdd);
                        }
                        break;

                    case "-delete":
                        {
                            var (client, server, targetDatabaseName) = await FillInitialData();

                            var resultDelete = await _defaultSyncController.AddOrDeleteDataToDatabaseAsync(server, client, true, targetDatabaseName);

                            StaticMethods.OperationResult(resultDelete);
                        }
                        break;

                    case "-update":
                        {
                            var (client, server, targetDatabaseName) = await FillInitialData();

                            var resultUpdate = await _defaultSyncController.UpdateDataToServerAsync(client, server, targetDatabaseName);

                            StaticMethods.OperationResult(resultUpdate);
                        }
                        break;

                    case "-exit":
                        {
                            Console.WriteLine();
                            return;
                        }

                    default:
                        {
                            Console.WriteLine(Constants.INVALID_COMMAND);
                        }
                        break;
                }
            }
        }

        private async Task<(List<TestModel>, List<TestModel>, string)> FillInitialData()
        {
            var result = (client: new List<TestModel>(), server: new List<TestModel>(), targetDatabaseName: string.Empty);

            Console.WriteLine();

            Console.Write(Constants.ENTER_DATABASE_EXPORT);
            var clientDatabaseName = Console.ReadLine();

            Console.Write(Constants.ENTER_DATABASE_IMPORT);
            var serverDatabaseName = Console.ReadLine();

            Console.Write(Constants.ENTER_DATABASE_IDENTIFIER);
            var identifier = Console.ReadLine();

            Console.Write(Constants.ENTER_DATABASE_TARGET);
            result.targetDatabaseName = Console.ReadLine();

            result.client = await _defaultSyncController.GetDataFromDatabaseAsync(clientDatabaseName, identifier);
            result.server = await _defaultSyncController.GetDataFromDatabaseAsync(serverDatabaseName, identifier);

            return result;
        }
    }
}
