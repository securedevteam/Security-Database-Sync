using SecurityDatabaseSync.BLL.Interfaces;
using SecurityDatabaseSync.Core;
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
                Console.WriteLine(Constants.COMMAND_HARD);
                Console.Write(Constants.ENTER_COMMAND);
                var param = Console.ReadLine();

                switch (param)
                {
                    case "-insert":
                        {
                            Console.WriteLine();

                            Console.Write(Constants.ENTER_DATABASE_NAME);
                            var databaseName = Console.ReadLine();

                            Console.Write(Constants.ENTER_DATABASE_IDENTIFIER);
                            var identifier = Console.ReadLine();

                            var resultInsert = await _sync.InsertDataAsync(databaseName, identifier);

                            StaticMethods.OperationResult(resultInsert);
                        }
                        break;

                    case "-transfer":
                        {
                            Console.Write(Constants.ENTER_DATABASE_EXPORT);
                            var dbFirst = Console.ReadLine();

                            Console.Write(Constants.ENTER_DATABASE_IMPORT);
                            var dbSecond = Console.ReadLine();

                            Console.Write(Constants.ENTER_DATABASE_IDENTIFIER);
                            var identifier = Console.ReadLine();

                            var resultCopy = await _sync.CopyDataAsync(dbFirst, dbSecond, identifier);

                            StaticMethods.OperationResult(resultCopy);
                        }
                        break;

                    case "-clear":
                        {
                            Console.WriteLine();

                            Console.Write(Constants.ENTER_DATABASE_NAME);
                            var databaseName = Console.ReadLine();

                            var resultClear = await _sync.ClearDataAsync(databaseName);

                            StaticMethods.OperationResult(resultClear);
                        }
                        break;

                    case "db-c-t":
                        {
                            Console.WriteLine();

                            Console.Write(Constants.ENTER_DATABASE_NAME);
                            var databaseName = Console.ReadLine();

                            Console.Write(Constants.ENTER_DATABASE_IDENTIFIER);
                            var identifier = Console.ReadLine();

                            var resultClear = await _sync.ClearDataAsync(databaseName, identifier);

                            StaticMethods.OperationResult(resultClear);
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
    }
}
