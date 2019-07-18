using Microsoft.EntityFrameworkCore;
using SecurityDatabaseSync.BLL.Interfaces;
using SecurityDatabaseSync.DAL;
using SecurityDatabaseSync.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Extensions;

namespace SecurityDatabaseSync.BLL.Implementations
{
    /// <summary>
    /// Класс классической синхронизации базы данных.
    /// </summary>
    public class BulkDefaultSyncController : IDefaultSyncController
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public BulkDefaultSyncController() { }

        /// <inheritdoc/>
        public async Task<List<TestModel>> GetDataFromDatabaseAsync(string databaseName)
        {
            using (ApplicationContext db = new ApplicationContext(databaseName))
            {
                var result = await db.TestModelTable.ToListAsync();

                return result;
            }
        }

        /// <inheritdoc/>
        public async Task<List<TestModel>> GetDataFromDatabaseAsync(string databaseName, string identifier)
        {
            using (ApplicationContext db = new ApplicationContext(databaseName))
            {
                var result = await db.TestModelTable.Where(r => r.Code.StartsWith(identifier))
                                              .ToListAsync();

                return result;
            }
        }


        // Пункт 1.1 Если в отделении нет записи из этого отделения, то удалить ее и на сервере
        // Пункт 1.2 Если на сервере нет записей других отделений, то удалить их и в отделении
        // Пункт 1.4 Добавить на сервер недостающие записи
        // Пункт 1.5 Добавить в отделение недостающие записи

        /// <inheritdoc/>
        public async Task<bool> AddOrDeleteDataToDatabaseAsync(List<TestModel> firstData, List<TestModel> secondData, bool remove, string databaseName)
        {
            var firstCodes = firstData.Select(d => d.Code)
                                      .ToList();

            var secondCodes = secondData.Select(d => d.Code)
                                       .ToList();

            var exceptCodes = firstCodes.Except(secondCodes)
                                        .ToList();

            if (exceptCodes.Count != 0)
            {
                var exceptData = new List<TestModel>();

                foreach (var item in exceptCodes)
                {
                    var model = firstData.Where(d => d.Code == item)
                                         .FirstOrDefault();

                    if (model != null)
                    {
                        exceptData.Add(new TestModel
                        {
                            Code = model.Code,
                            Name = model.Name,
                            Current = model.Current
                        });
                    }
                }

                EntityFrameworkManager.ContextFactory = db => new ApplicationContext(databaseName);

                using (ApplicationContext db = new ApplicationContext(databaseName))
                {
                    if (remove)
                    {
                        await db.BulkDeleteAsync(exceptData);
                    }
                    else
                    {
                        await db.BulkInsertAsync(exceptData);
                    }

                    await db.SaveChangesAsync();
                }

                return true;
            }

            return false;
        }


        // Пункт 1.3  Если на сервере и оделении записи различаются по UpdateDate, то выполнить Update записи с меньшей датой

        /// <inheritdoc/>
        public async Task<bool> UpdateDataToServerAsync(List<TestModel> firstData, List<TestModel> secondData, string databaseName)
        {
            var exceptData = new List<TestModel>();

            foreach (var fd_item in firstData)
            {
                foreach (var sd_item in secondData)
                {
                    if (fd_item.Code == sd_item.Code)
                    {
                        if (fd_item.Current > sd_item.Current)
                        {
                            exceptData.Add(new TestModel
                            {
                                Code = fd_item.Code,
                                Name = fd_item.Name,
                                Current = fd_item.Current
                            });
                        }
                    }
                }
            }

            if (exceptData.Count != 0)
            {
                EntityFrameworkManager.ContextFactory = db => new ApplicationContext(databaseName);

                using (ApplicationContext db = new ApplicationContext(databaseName))
                {
                    await db.BulkUpdateAsync(exceptData);

                    await db.SaveChangesAsync();
                }

                return true;
            }

            return false;
        }
    }
}
