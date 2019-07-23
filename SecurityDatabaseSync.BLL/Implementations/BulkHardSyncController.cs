using Microsoft.EntityFrameworkCore;
using SecurityDatabaseSync.BLL.Interfaces;
using SecurityDatabaseSync.DAL;
using SecurityDatabaseSync.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Extensions;

namespace SecurityDatabaseSync.BLL.Implementations
{
    /// <summary>
    /// Bulk синхронизация.
    /// </summary>
    public class BulkHardSyncController : ISyncController
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public BulkHardSyncController() { }

        /// <inheritdoc/>
        public async Task<bool> InsertDataAsync(string databaseName, string identifier)
        {
            EntityFrameworkManager.ContextFactory = db => new ApplicationContext(databaseName);

            using (var db = new ApplicationContext(databaseName))
            {
                var data = new List<TestModel>();

                for (int i = 0; i < 10; i++)
                {
                    data.Add(new TestModel
                    {
                        Code = identifier + Guid.NewGuid().ToString(),
                        Name = Guid.NewGuid().ToString().Substring(0, 8),
                        Current = DateTime.Now
                    });
                }

                await db.BulkInsertAsync(data);

                return true;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> ClearDataAsync(string databaseName)
        {
            EntityFrameworkManager.ContextFactory = db => new ApplicationContext(databaseName);

            using (ApplicationContext db = new ApplicationContext(databaseName))
            {
                var data = await db.TestModelTable.ToListAsync();

                if (data.Count != 0)
                {
                    await db.BulkDeleteAsync(data);

                    return true;
                }

                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> ClearDataAsync(string databaseName, string identifier)
        {
            EntityFrameworkManager.ContextFactory = db => new ApplicationContext(databaseName);

            using (ApplicationContext db = new ApplicationContext(databaseName))
            {
                var data = await db.TestModelTable.Where(p => p.Code.StartsWith(identifier))
                                                  .ToListAsync();

                if (data.Count != 0)
                {
                    await db.BulkDeleteAsync(data);

                    return true;
                }

                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> CopyDataAsync(string dbFirst, string dbSecond, string identifier)
        {
            var dataFirst = new List<TestModel>();
            var dataSecond = new List<TestModel>();

            using (ApplicationContext db = new ApplicationContext(dbFirst))
            {
                dataFirst = await db.TestModelTable.Select(record => record)
                                                   .Where(r => r.Code.StartsWith(identifier))
                                                   .ToListAsync();

                Console.WriteLine("> Данные успешно считаны!");
            }

            EntityFrameworkManager.ContextFactory = db => new ApplicationContext(dbSecond);

            using (ApplicationContext db = new ApplicationContext(dbSecond))
            {
                foreach (var item in dataFirst)
                {
                    dataSecond.Add(new TestModel
                    {
                        Code = item.Code,
                        Name = item.Name,
                        Current = item.Current
                    });
                }

                await db.BulkInsertAsync(dataSecond);

                return true;
            }
        }
    }
}
