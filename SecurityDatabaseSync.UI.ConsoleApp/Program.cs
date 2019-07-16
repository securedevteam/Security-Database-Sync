using SecurityDatabaseSync.BLL.Implementations;
using SecurityDatabaseSync.BLL.Interfaces;
using SecurityDatabaseSync.UI.ConsoleApp.Implementations;
using SecurityDatabaseSync.UI.ConsoleApp.Interfaces;
using System;
using System.Threading.Tasks;

namespace SecurityDatabaseSync.UI.ConsoleApp
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            ISyncController hardController = new HardSyncController();
            ISyncController bulkController = new BulkSyncController();
            IDefaultSyncController defController = new DefaultSyncController();

            ISyncStart hard = new HardSynchronization(hardController);
            ISyncStart bulk = new HardSynchronization(bulkController);
            ISyncStart def = new DefaultSynchronization(defController);
            
            while (true)
            {
                Console.WriteLine("-hard, -hard-bulk, -default, -default-bulk, -quit");
                Console.Write("Введите тип синхронизации: ");
                var param = Console.ReadLine();

                switch(param)
                {
                    case "-hard":
                        {
                            Console.WriteLine();
                            await hard.SyncStart();
                        }
                        break;

                    case "-hard-bulk":
                        {
                            Console.WriteLine();
                            await bulk.SyncStart();
                        }
                        break;

                    case "-default":
                        {
                            Console.WriteLine();
                            await def.SyncStart();
                        }
                        break;

                    case "-default-bulk":
                        {
                            // TODO: Реализовать через Bulk
                        }
                        break;

                    case "-quit":
                        {
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
