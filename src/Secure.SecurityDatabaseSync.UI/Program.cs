using Secure.SecurityDatabaseSync.BLL.Interfaces;
using Secure.SecurityDatabaseSync.BLL.Services;
using System;
using System.Threading.Tasks;

namespace Secure.SecurityDatabaseSync.UI
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine(UiConstant.COMMAND_AVAILABLE_LIST);
            while (true)
            {
                Console.WriteLine();

                try
                {
                    Console.Write(UiConstant.ENTER_SYNC_TYPE);
                    var syncType = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(syncType))
                    {
                        throw new ArgumentException(UiConstant.INVALID_DATABASE_SOURCE, nameof(syncType));
                    }

                    Console.Write(UiConstant.ENTER_DATABASE_SOURCE);
                    var sourceDb = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(sourceDb))
                    {
                        throw new ArgumentException(UiConstant.INVALID_DATABASE_SOURCE, nameof(sourceDb));
                    }

                    Console.Write(UiConstant.ENTER_DATABASE_CODE);
                    var code = Console.ReadLine().ToUpper();
                    if (string.IsNullOrWhiteSpace(code))
                    {
                        throw new ArgumentException(UiConstant.INVALID_CODE, nameof(code));
                    }

                    Console.Write(UiConstant.ENTER_DATABASE_TARGET);
                    var targetDb = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(targetDb))
                    {
                        throw new ArgumentException(UiConstant.INVALID_DATABASE_TARGET, nameof(targetDb));
                    }

                    switch (syncType)
                    {
                        case "--default":
                            {
                                ISyncService defaultSyncService = new DefaultSyncService(sourceDb, targetDb, code);
                                await defaultSyncService.RunAsync();
                                Console.WriteLine(UiConstant.COMMAND_COMPLETED);
                            }
                            break;

                        case "--bulk":
                            {
                                ISyncService bulkSyncService = new BulkSyncService(sourceDb, targetDb, code);
                                await bulkSyncService.RunAsync();
                                Console.WriteLine(UiConstant.COMMAND_COMPLETED);
                            }
                            break;

                        case "--hard-bulk":
                            {
                                ISyncService hardBulkSyncService = new HardBulkSyncService(sourceDb, targetDb, code);
                                await hardBulkSyncService.RunAsync();
                                Console.WriteLine(UiConstant.COMMAND_COMPLETED);
                            }
                            break;

                        case "--quit":
                            {
                                return;
                            }

                        default:
                            {
                                Console.WriteLine(UiConstant.COMMAND_INVALID);
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(UiConstant.COMMAND_FAILED);
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }
    }
}
