using SecurityDatabaseSync.BLL.Implementations;
using System;
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
                Console.Write("Введите действие: ");
                var param = Console.ReadLine();

                switch(param)
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
