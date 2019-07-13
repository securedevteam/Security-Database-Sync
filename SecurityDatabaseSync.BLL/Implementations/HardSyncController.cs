using SecurityDatabaseSync.DAL;
using SecurityDatabaseSync.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityDatabaseSync.BLL.Implementations
{
    public class HardSyncController
    {
        public HardSyncController()
        {
        }

        public async Task InsertDataAsync()
        {
            Console.Write("Enter database name: ");
            var databaseName = Console.ReadLine();

            using (ApplicationContext db = new ApplicationContext(databaseName))
            {
                var data = new List<TestModel>();

                for (int i = 0; i < 10; i++)
                {
                    data.Add(new TestModel { Name = Guid.NewGuid().ToString() });
                }

                await db.TestModelTable.AddRangeAsync(data);
                await db.SaveChangesAsync();

                Console.WriteLine("Complete!");
            }
        }

        public void ClearData()
        {
            Console.Write("Enter database name: ");
            var databaseName = Console.ReadLine();

            using (ApplicationContext db = new ApplicationContext(databaseName))
            {
                var client = db.TestModelTable.ToList();

                if (client.Count != 0)
                {
                    foreach (var item in client)
                    {
                        db.TestModelTable.Remove(item);
                    }

                    db.SaveChanges();
                }

                Console.WriteLine("Complete!");
            }
        }

        public void CopyData(string dbFirst, string dbSecond)
        {
            var dataFirst = new List<TestModel>();
            var dataSecond = new List<TestModel>();

            using (ApplicationContext db = new ApplicationContext(dbFirst))
            {
                dataFirst = db.TestModelTable.ToList();

                Console.WriteLine("ToList!");
            }

            using (ApplicationContext db = new ApplicationContext(dbSecond))
            {
                foreach (var item in dataFirst)
                {
                    dataSecond.Add(new TestModel { Name = item.Name });
                }

                db.TestModelTable.AddRange(dataSecond);
                db.SaveChanges();

                Console.WriteLine("Complete!");
            }
        }
    }
}
