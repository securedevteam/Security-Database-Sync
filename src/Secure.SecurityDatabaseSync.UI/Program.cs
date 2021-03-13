using Microsoft.Extensions.DependencyInjection;
using Secure.SecurityDatabaseSync.BLL.Interfaces;
using Secure.SecurityDatabaseSync.BLL.Services;
using Secure.SecurityDatabaseSync.DAL.Contexts;
using System;
using System.Threading.Tasks;

namespace Secure.SecurityDatabaseSync.UI
{
    class Program
    {
        private static IDefaultSyncService _defaultSyncService;
        private static IBulkSyncService _bulkSyncService;
        private static IHardBulkSyncService _hardBulkSyncService;

        private static async Task Main(string[] args)
        {
            ConfigureServices();
            await ShowUiAsync();
        }

        private static void ConfigureServices()
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<FirstAppContext>()
                .AddDbContext<SecondAppContext>()
                .AddScoped<IDefaultSyncService, DefaultSyncService>()
                .AddScoped<IBulkSyncService, BulkSyncService>()
                .AddScoped<IHardBulkSyncService, HardBulkSyncService>()
                .BuildServiceProvider();

            _defaultSyncService = serviceProvider.GetService<IDefaultSyncService>();
            _bulkSyncService = serviceProvider.GetService<IBulkSyncService>();
            _hardBulkSyncService = serviceProvider.GetService<IHardBulkSyncService>();
        }

        private static async Task ShowUiAsync()
        {
            Console.WriteLine(UiConstant.COMMAND_AVAILABLE_LIST);
            while (true)
            {
                Console.WriteLine();
                Console.Write(UiConstant.ENTER_SYNC_TYPE);
                var userInput = Console.ReadLine();

                try
                {
                    switch (userInput)
                    {
                        case "--default":
                            {
                                await _defaultSyncService.RunAsync();
                                Console.WriteLine(UiConstant.COMMAND_COMPLETED);
                            }
                            break;

                        case "--bulk":
                            {
                                await _bulkSyncService.RunAsync();
                                Console.WriteLine(UiConstant.COMMAND_COMPLETED);
                            }
                            break;

                        case "--hard-bulk":
                            {
                                await _hardBulkSyncService.RunAsync();
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
