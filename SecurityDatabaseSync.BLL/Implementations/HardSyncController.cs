using Microsoft.EntityFrameworkCore;
using SecurityDatabaseSync.BLL.Interfaces;
using SecurityDatabaseSync.DAL;
using SecurityDatabaseSync.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityDatabaseSync.BLL.Implementations
{
    /// <summary>
    /// Жесткая синхронизация.
    /// </summary>
    public class HardSyncController : IHardSyncController
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public HardSyncController() { }

        /// <inheritdoc/>
        public async Task InsertDataAsync(string databaseName, string identifier)
        {
            using (ApplicationContext db = new ApplicationContext(databaseName))
            {
                var data = new List<TestModel>();

                for (int i = 0; i < 10; i++)
                {
                    data.Add(new TestModel { Name = identifier + Guid.NewGuid().ToString() });
                }

                await db.TestModelTable.AddRangeAsync(data);
                await db.SaveChangesAsync();
            }
        }

        /// <inheritdoc/>
        public async Task ClearDataAsync(string databaseName)
        {
            using (ApplicationContext db = new ApplicationContext(databaseName))
            {
                var client = await db.TestModelTable.ToListAsync();

                if (client.Count != 0)
                {
                    foreach (var item in client)
                    {
                        db.TestModelTable.Remove(item);
                    }

                    await db.SaveChangesAsync();
                }
            }
        }

        /// <inheritdoc/>
        public async Task ClearDataAsync(string databaseName, string identifier)
        {
            using (ApplicationContext db = new ApplicationContext(databaseName))
            {
                var client = await db.TestModelTable.ToListAsync();

                if (client.Count != 0)
                {
                    foreach (var item in client)
                    {
                        if (item.Name.StartsWith(identifier))
                        {
                            db.TestModelTable.Remove(item);
                        }
                    }

                    await db.SaveChangesAsync();
                }
            }
        }

        /// <inheritdoc/>
        public async Task CopyDataAsync(string dbFirst, string dbSecond, string identifier)
        {
            var dataFirst = new List<TestModel>();
            var dataSecond = new List<TestModel>();

            using (ApplicationContext db = new ApplicationContext(dbFirst))
            {
                dataFirst = await db.TestModelTable.Select(record => record)
                                                   .Where(r => r.Name.StartsWith(identifier))
                                                   .ToListAsync();

                Console.WriteLine("> Данные успешно считаны!");
            }

            using (ApplicationContext db = new ApplicationContext(dbSecond))
            {
                foreach (var item in dataFirst)
                {
                    dataSecond.Add(new TestModel { Name = item.Name });
                }

                await db.TestModelTable.AddRangeAsync(dataSecond);
                await db.SaveChangesAsync();
            }
        }
    }
}
