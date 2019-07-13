using SecurityDatabaseSync.BLL.Interfaces;
using SecurityDatabaseSync.UI.ConsoleApp.Interfaces;
using System;
using System.Threading.Tasks;

namespace SecurityDatabaseSync.UI.ConsoleApp.Implementations
{
    /// <summary>
    /// Класс для управления жесткой синхронизацией.
    /// </summary>
    public class HardSynchronization : ISyncStart
    {
        private readonly ISyncController _sync;

        /// <summary>
        /// Конструктор с параметром.
        /// </summary>
        /// <param name="syncController">интерфейс.</param>
        public HardSynchronization(ISyncController syncController)
        {
            _sync = syncController;
        }

        /// <summary>
        /// Запуск синхронизации.
        /// </summary>
        /// <returns></returns>
        public async Task SyncStart()
        {
            while (true)
            {
                Console.WriteLine("-insert, -transfer, -clear, -clear-ident, -exit");
                Console.Write("Введите команду: ");
                var param = Console.ReadLine();

                switch (param)
                {
                    case "-insert":
                        {
                            Console.WriteLine();

                            Console.Write("Введите название базы данных: ");
                            var databaseName = Console.ReadLine();

                            Console.Write("Введите идентификатор: ");
                            var identifier = Console.ReadLine();

                            await _sync.InsertDataAsync(databaseName, identifier);

                            Console.WriteLine(">> Операция выполнена!\n");
                        }
                        break;

                    case "-transfer":
                        {
                            Console.Write("Введите название базы данных для экспорта: ");
                            var dbFirst = Console.ReadLine();

                            Console.Write("Введите название базы данных для импорта: ");
                            var dbSecond = Console.ReadLine();

                            Console.Write("Введите идентификатор: ");
                            var identifier = Console.ReadLine();

                            await _sync.CopyDataAsync(dbFirst, dbSecond, identifier);

                            Console.WriteLine(">> Операция выполнена!\n");
                        }
                        break;

                    case "-clear":
                        {
                            Console.WriteLine();

                            Console.Write("Введите название базы данных: ");
                            var databaseName = Console.ReadLine();

                            await _sync.ClearDataAsync(databaseName);

                            Console.WriteLine(">> Операция выполнена!\n");
                        }
                        break;

                    case "db-c-t":
                        {
                            Console.WriteLine();

                            Console.Write("Введите название базы данных: ");
                            var databaseName = Console.ReadLine();

                            Console.Write("Введите идентификатор: ");
                            var identifier = Console.ReadLine();

                            await _sync.ClearDataAsync(databaseName, identifier);

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
