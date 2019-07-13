using SecurityDatabaseSync.BLL.Implementations;
using SecurityDatabaseSync.DAL;
using SecurityDatabaseSync.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityDatabaseSync.UI.ConsoleApp
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            
            var hardSync = new HardSyncController();

            while (true)
            {
                Console.Write("Enter param: ");
                var param = Console.ReadLine();

                switch(param)
                {
                    case "db-i":
                        {
                            await hardSync.InsertDataAsync();
                        }
                        break;

                    case "db-c-u":
                        {
                            Console.Write("Enter database name: ");
                            var databaseName = Console.ReadLine();

                            hardSync.CopyData("cs1", databaseName);
                        }
                        break;

                    case "db-s-i":
                        {
                            Console.Write("Enter database name: ");
                            var databaseName = Console.ReadLine();

                            hardSync.CopyData(databaseName, "cs1");
                        }
                        break;

                    case "db-c":
                        {
                            hardSync.ClearData();
                        }
                        break;

                    default: { } break;
                }
            }











            //using (ApplicationContextClient db = new ApplicationContextClient("ip"))
            //{
            //    // создаем два объекта User
            //    TestModel user1 = new TestModel { Name = Guid.NewGuid().ToString() };
            //    TestModel user2 = new TestModel { Name = Guid.NewGuid().ToString() };

            //    // добавляем их в бд
            //    db.TestModelTable.Add(user1);
            //    db.TestModelTable.Add(user2);
            //    db.SaveChanges();
            //    Console.WriteLine("Объекты успешно сохранены");

            //    // получаем объекты из бд и выводим на консоль
            //    var users = db.TestModelTable.ToList();
            //    Console.WriteLine("Список объектов:");
            //    foreach (var u in users)
            //    {
            //        Console.WriteLine($"{u.Id}.{u.Name}");
            //    }
            //}

            //using (ApplicationContextServer db = new ApplicationContextServer())
            //{
            //    Console.WriteLine("ДБ создана");
            //}

            //var dbClient = new ApplicationContextClient("ip");
            //var dbServer = new ApplicationContextServer();

            //var bulkSyncController = new BulkSyncController(dbClient, dbServer);

            //var client = bulkSyncController.GetAllDataFromClientDatabases();
            //var server = bulkSyncController.GetAllDataFromServerDatabases();

            //bulkSyncController.DeleteAllDataFromServerDatabaseAsync(server);
            //bulkSyncController.InsertAllDataToServerDatabase(client);
            



            ////var t = Task.Run(() => GetAllData(bulkSyncController));
            ////t.Wait();

            //Console.WriteLine("123321");

            Console.Read();
        }

        //private static async Task DeleteServerData(BulkSyncController bulkSyncController)
        //{

        //}

        //private static async Task GetAllData(BulkSyncController bulkSyncController)
        //{
        //    var client = await bulkSyncController.GetAllDataFromClientDatabasesAsync();
        //    var server = await bulkSyncController.GetAllDataFromServerDatabasesAsync();

        //    await bulkSyncController.DeleteAllDataFromServerDatabaseAsync(server);

        //    //await bulkSyncController.SaveAllDataToServerDatabaseAsync();
        //    await bulkSyncController.InsertAllDataToServerDatabaseAsync(client);

        //    //await bulkSyncController.DeleteAllDataFromClientDatabaseAsync(client);

        //    //var server1 = await bulkSyncController.GetAllDataFromServerDatabasesAsync();
        //    await bulkSyncController.InsertAllDataToClientDatabaseAsync(client);
        //    //await bulkSyncController.SaveAllDataToServerDatabaseAsync();

        //    Console.WriteLine("123");
        //}
    }
}
