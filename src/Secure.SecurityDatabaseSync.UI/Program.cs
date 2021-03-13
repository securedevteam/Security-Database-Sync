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

                Console.Write(UiConstant.ENTER_SYNC_TYPE);
                var syncType = Console.ReadLine();
                Console.Write(UiConstant.ENTER_DATABASE_SOURCE);
                var sourceDb = Console.ReadLine();
                Console.Write(UiConstant.ENTER_DATABASE_CODE);
                var code = Console.ReadLine().ToUpper();
                Console.Write(UiConstant.ENTER_DATABASE_TARGET);
                var targetDb = Console.ReadLine();

                try
                {
                    var incorrectUserInputData = false;

                    if (string.IsNullOrEmpty(sourceDb))
                    {
                        incorrectUserInputData = true;
                    }

                    if (string.IsNullOrEmpty(code))
                    {
                        incorrectUserInputData = true;
                    }

                    if (string.IsNullOrEmpty(targetDb))
                    {
                        incorrectUserInputData = true;
                    }

                    if (incorrectUserInputData)
                    {
                        throw new ArgumentException();
                    }

                    switch (syncType)
                    {
                        case "--default":
                            {
                                var defaultSyncService = new DefaultSyncService(sourceDb, targetDb, code);
                                await defaultSyncService.RunAsync();
                                Console.WriteLine(UiConstant.COMMAND_COMPLETED);
                            }
                            break;

                        case "--bulk":
                            {
                                var bulkSyncService = new BulkSyncService(sourceDb, targetDb, code);
                                await bulkSyncService.RunAsync();
                                Console.WriteLine(UiConstant.COMMAND_COMPLETED);
                            }
                            break;

                        case "--hard-bulk":
                            {
                                var hardBulkSyncService = new HardBulkSyncService(sourceDb, targetDb, code);
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
