using Secure.SecurityDatabaseSync.BLL.Interfaces;
using Secure.SecurityDatabaseSync.BLL.Services;
using Secure.SecurityDatabaseSync.DAL.Constants;
using Secure.SecurityDatabaseSync.DAL.Contexts;
using Secure.SecurityDatabaseSync.DAL.Extensions;
using Secure.SecurityDatabaseSync.UI.Resources;
using System;
using System.Threading.Tasks;

namespace Secure.SecurityDatabaseSync.UI
{
    internal class Program
    {
        private async static Task Main()
        {
            static string UserInput(
                string message,
                string errorMessage,
                string paramName)
            {
                Console.Write(message);
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    throw new ArgumentException(errorMessage, paramName);
                }

                return input;
            }

            Console.WriteLine(MessageResource.CommandAvailableList);
            while (true)
            {
                Console.WriteLine();

                try
                {
                    var syncType = UserInput(
                        MessageResource.EnterSyncType,
                        MessageResource.InvalidSyncType,
                        MessageResource.ParamSyncType);

                    var sourceDb = UserInput(
                        MessageResource.EnterDatabaseNameSource,
                        MessageResource.InvalidDatabaseSource,
                        MessageResource.ParamSourceDb);

                    var code = UserInput(
                        MessageResource.EnterDatabaseCode,
                        MessageResource.InvalidCode,
                        MessageResource.ParamCode);

                    var targetDb = UserInput(
                        MessageResource.EnterDatabaseNameTarget,
                        MessageResource.InvalidDatabaseTarget,
                        MessageResource.ParamTargetDb);

                    switch (syncType)
                    {
                        case "--default":
                            {
                                ISyncTask defaultSyncService = new DefaultSyncTask(
                                    GetContext(sourceDb),
                                    GetContext(targetDb),
                                    code);

                                await defaultSyncService.RunAsync();
                                Console.WriteLine(MessageResource.CommandCompleted);
                            }
                            break;

                        case "--bulk":
                            {
                                ISyncTask bulkSyncService = new BulkSyncTask(
                                    GetContext(sourceDb),
                                    GetContext(targetDb),
                                    code);
                                await bulkSyncService.RunAsync();
                                Console.WriteLine(MessageResource.CommandCompleted);
                            }
                            break;

                        case "--hard-bulk":
                            {
                                ISyncTask hardBulkSyncService = new HardBulkSyncTask(
                                    GetContext(sourceDb),
                                    GetContext(targetDb),
                                    code);
                                await hardBulkSyncService.RunAsync();
                                Console.WriteLine(MessageResource.CommandCompleted);
                            }
                            break;

                        case "--quit":
                            {
                                return;
                            }

                        default:
                            {
                                Console.WriteLine(MessageResource.CommandInvalid);
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(MessageResource.CommandFailed);
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        private static ApplicationContext GetContext(string databaseName) =>
            databaseName.GetApplicationContext(
                    AppSettingConstant.AppSettingJsonName,
                    AppSettingConstant.ConnectionStringSection);
    }
}
