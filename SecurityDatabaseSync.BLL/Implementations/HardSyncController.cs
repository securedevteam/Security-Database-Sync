﻿using Microsoft.EntityFrameworkCore;
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
    public class HardSyncController : ISyncController
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public HardSyncController() { }

        /// <inheritdoc/>
        public async Task<bool> InsertDataAsync(string databaseName, string identifier)
        {
            using (ApplicationContext db = new ApplicationContext(databaseName))
            {
                var data = new List<TestModel>();

                for (int i = 0; i < 10000; i++)
                {
                    data.Add(new TestModel
                    {
                        Code = identifier + Guid.NewGuid().ToString(),
                        Name = Guid.NewGuid().ToString().Substring(0, 8),
                        Current = DateTime.Now
                    });
                }

                await db.AddRangeAsync(data);
                await db.SaveChangesAsync();

                return true;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> ClearDataAsync(string databaseName)
        {
            using (ApplicationContext db = new ApplicationContext(databaseName))
            {
                var data = await db.TestModelTable.ToListAsync();

                if (data.Count != 0)
                {
                    foreach (var item in data)
                    {
                        db.TestModelTable.Remove(item);
                    }

                    await db.SaveChangesAsync();

                    return true;
                }

                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> ClearDataAsync(string databaseName, string identifier)
        {
            using (ApplicationContext db = new ApplicationContext(databaseName))
            {
                var data = await db.TestModelTable.ToListAsync();

                if (data.Count != 0)
                {
                    foreach (var item in data)
                    {
                        if (item.Code.StartsWith(identifier))
                        {
                            db.TestModelTable.Remove(item);
                        }
                    }

                    await db.SaveChangesAsync();

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
                dataFirst = await db.TestModelTable.Where(r => r.Code.StartsWith(identifier))
                                                   .ToListAsync();

                Console.WriteLine("> Данные успешно считаны!");
            }

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

                await db.TestModelTable.AddRangeAsync(dataSecond);
                await db.SaveChangesAsync();

                return true;
            }
        }
    }
}
