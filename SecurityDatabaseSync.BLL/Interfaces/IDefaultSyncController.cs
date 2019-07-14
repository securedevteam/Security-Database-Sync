using SecurityDatabaseSync.DAL.Models;
using System.Collections.Generic;

namespace SecurityDatabaseSync.BLL.Interfaces
{
    /// <summary>
    /// Интерфейс классической синхронизации базы данных.
    /// </summary>
    public interface IDefaultSyncController
    {
        /// <summary>
        /// Получить весь список данных.
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns>Спиок полученных моделей.</returns>
        List<TestModel> GetDataFromDatabase(string databaseName);

        /// <summary>
        /// Получить весь список данных.
        /// </summary>
        /// <param name="databaseName">название базы данных.</param>
        /// <param name="identifier">условие выборки (идентификатор).</param>
        /// <returns>Список полученных моделей.</returns>
        List<TestModel> GetDataWithFilterFromDatabase(string databaseName, string identifier);

        /// <summary>
        /// Добавить или удалить данные из базы данных.
        /// </summary>
        /// <param name="firstData">первый список моделей.</param>
        /// <param name="secondData">второй список моделей.</param>
        /// <param name="remove">удалить данные.</param>
        /// <param name="databaseName">название базы данных.</param>
        /// <returns>Результат операции.</returns>
        bool AddOrDeleteDataToDatabase(List<TestModel> firstData, List<TestModel> secondData, bool remove, string databaseName);
    }
}
