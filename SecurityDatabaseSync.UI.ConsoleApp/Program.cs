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
            ISyncController hController = new HardSyncController();
            ISyncController bhController = new BulkHardSyncController();
            IDefaultSyncController dController = new DefaultSyncController();
            IDefaultSyncController bdController = new BulkDefaultSyncController();

            ISyncStart hard = new HardSynchronization(hController);
            ISyncStart bulkhard = new HardSynchronization(bhController);
            ISyncStart def = new DefaultSynchronization(dController);
            ISyncStart bulkdef = new DefaultSynchronization(bdController);

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
                            await bulkhard.SyncStart();
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
                            Console.WriteLine();
                            await bulkdef.SyncStart();
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
