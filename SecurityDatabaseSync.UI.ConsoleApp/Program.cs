using SecurityDatabaseSync.UI.ConsoleApp.Implemetations;
using SecurityDatabaseSync.UI.ConsoleApp.Interfaces;
using System;
using System.Threading.Tasks;

namespace SecurityDatabaseSync.UI.ConsoleApp
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            ISyncStart sync = new HardSync();

            while (true)
            {
                Console.Write("Введите тип синхронизации: ");
                var param = Console.ReadLine();

                switch(param)
                {
                    case "hard":
                        {
                            Console.WriteLine();
                            await sync.SyncStart();
                        }
                        break;

                    case "bulk":
                        {
                            // TODO: Реализовать данный тип
                        }
                        break;

                    case "default":
                        {
                            // TODO: Реализовать данный тип
                        }
                        break;

                    case "exit":
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
