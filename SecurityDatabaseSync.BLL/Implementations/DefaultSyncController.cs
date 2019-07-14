using SecurityDatabaseSync.BLL.Interfaces;
using SecurityDatabaseSync.DAL;
using SecurityDatabaseSync.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace SecurityDatabaseSync.BLL.Implementations
{
    /// <summary>
    /// Класс классической синхронизации базы данных.
    /// </summary>
    public class DefaultSyncController : IDefaultSyncController
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public DefaultSyncController() { }

        /// <inheritdoc/>
        public List<TestModel> GetDataFromDatabase(string databaseName)
        {
            using (ApplicationContext db = new ApplicationContext(databaseName))
            {
                var result = db.TestModelTable.ToList();

                return result;
            }
        }

        /// <inheritdoc/>
        public List<TestModel> GetDataWithFilterFromDatabase(string databaseName, string identifier)
        {
            using (ApplicationContext db = new ApplicationContext(databaseName))
            {
                var result = db.TestModelTable.Where(r => r.Code.StartsWith(identifier))
                                              .ToList();

                return result;
            }
        }

        // Пункт 1.1 Если в отделении нет записи из этого отделения, то удалить ее и на сервере
        // Пункт 1.2 Если на сервере нет записей других отделений, то удалить их и в отделении
        // Пункт 1.4 Добавить на сервер недостающие записи
        // Пункт 1.5 Добавить в отделение недостающие записи

        /// <inheritdoc/>
        public bool AddOrDeleteDataToDatabase(List<TestModel> firstData, List<TestModel> secondData, bool remove, string databaseName)
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

                using (ApplicationContext db = new ApplicationContext(databaseName))
                {
                    if (remove)
                    {
                        db.TestModelTable.RemoveRange(exceptData);
                    }
                    else
                    {
                        db.TestModelTable.AddRange(exceptData);
                    }

                    db.SaveChanges();
                }

                return true;
            }

            return false;
        }

        // Пункт 1.3  Если на сервере и оделении записи различаются по UpdateDate, то выполнить Update записи с меньшей датой
    }
}
