using SecurityDatabaseSync.BLL.Implementations;
using SecurityDatabaseSync.UI.ConsoleApp.Interfaces;
using System;
using System.Threading.Tasks;

namespace SecurityDatabaseSync.UI.ConsoleApp.Implemetations
{
    /// <summary>
    /// Класс для управления жесткой синхронизацией.
    /// </summary>
    public class HardSync : ISyncStart
    {
        /// <summary>
        /// Запуск синхронизации.
        /// </summary>
        /// <returns></returns>
        public async Task SyncStart()
        {
            var hardSync = new HardSyncController();

            while (true)
            {
                Console.Write("Введите команду: ");
                var param = Console.ReadLine();

                switch (param)
                {
                    case "db-i":
                        {
                            Console.WriteLine();

                            Console.Write("Введите название базы данных: ");
                            var databaseName = Console.ReadLine();

                            Console.Write("Введите идентификатор: ");
                            var identifier = Console.ReadLine();

                            await hardSync.InsertDataAsync(databaseName, identifier);

                            Console.WriteLine(">> Операция выполнена!\n");
                        }
                        break;

                    case "db-ei":
                        {
                            Console.Write("Введите название базы данных для экспорта: ");
                            var dbFirst = Console.ReadLine();

                            Console.Write("Введите название базы данных для импорта: ");
                            var dbSecond = Console.ReadLine();

                            Console.Write("Введите идентификатор: ");
                            var identifier = Console.ReadLine();

                            await hardSync.CopyDataAsync(dbFirst, dbSecond, identifier);

                            Console.WriteLine(">> Операция выполнена!\n");
                        }
                        break;

                    case "db-c":
                        {
                            Console.WriteLine();

                            Console.Write("Введите название базы данных: ");
                            var databaseName = Console.ReadLine();

                            await hardSync.ClearDataAsync(databaseName);

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

                            await hardSync.ClearDataAsync(databaseName, identifier);

                            Console.WriteLine(">> Операция выполнена!\n");
                        }
                        break;

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
