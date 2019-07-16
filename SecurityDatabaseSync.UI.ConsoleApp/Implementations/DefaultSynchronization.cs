using SecurityDatabaseSync.BLL.Interfaces;
using SecurityDatabaseSync.UI.ConsoleApp.Interfaces;
using System;
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
                            Console.WriteLine();

                            Console.Write("Введите название базы данных клиента: ");
                            var clientDatabaseName = Console.ReadLine();

                            Console.Write("Введите название базы данных сервера: ");
                            var serverDatabaseName = Console.ReadLine();

                            Console.Write("Введите идентификатор: ");
                            var identifier = Console.ReadLine();

                            Console.Write("Введите название целевой базы данных: ");
                            var targetDatabaseName = Console.ReadLine();

                            var client = await _defaultSyncController.GetDataFromDatabaseAsync(clientDatabaseName, identifier);
                            var server = await _defaultSyncController.GetDataFromDatabaseAsync(serverDatabaseName, identifier);

                            var resultAdd = await _defaultSyncController.AddOrDeleteDataToDatabaseAsync(client, server, false, targetDatabaseName);

                            Console.WriteLine(">> Операция выполнена!\n");
                        }
                        break;

                    case "-delete":
                        {
                            Console.WriteLine();

                            Console.Write("Введите название базы данных клиента: ");
                            var clientDatabaseName = Console.ReadLine();

                            Console.Write("Введите название базы данных сервера: ");
                            var serverDatabaseName = Console.ReadLine();

                            Console.Write("Введите идентификатор: ");
                            var identifier = Console.ReadLine();

                            Console.Write("Введите название целевой базы данных: ");
                            var targetDatabaseName = Console.ReadLine();

                            var client = await _defaultSyncController.GetDataFromDatabaseAsync(clientDatabaseName, identifier);
                            var server = await _defaultSyncController.GetDataFromDatabaseAsync(serverDatabaseName, identifier);

                            var resultDelete = _defaultSyncController.AddOrDeleteDataToDatabaseAsync(server, client, true, targetDatabaseName);

                            Console.WriteLine(">> Операция выполнена!\n");
                        }
                        break;

                    case "-update":
                        {
                            Console.WriteLine();

                            Console.Write("Введите название базы данных клиента: ");
                            var clientDatabaseName = Console.ReadLine();

                            Console.Write("Введите название базы данных сервера: ");
                            var serverDatabaseName = Console.ReadLine();

                            Console.Write("Введите идентификатор: ");
                            var identifier = Console.ReadLine();

                            Console.Write("Введите название целевой базы данных: ");
                            var targetDatabaseName = Console.ReadLine();

                            var client = await _defaultSyncController.GetDataFromDatabaseAsync(clientDatabaseName, identifier);
                            var server = await _defaultSyncController.GetDataFromDatabaseAsync(serverDatabaseName, identifier);

                            var resultUpdate = _defaultSyncController.UpdateDataToServerAsync(client, server, targetDatabaseName);

                            Console.WriteLine(">> Операция выполнена!\n");
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
    }
}
