using SecurityDatabaseSync.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<List<TestModel>> GetDataFromDatabaseAsync(string databaseName);

        /// <summary>
        /// Получить весь список данных.
        /// </summary>
        /// <param name="databaseName">название базы данных.</param>
        /// <param name="identifier">условие выборки (идентификатор).</param>
        /// <returns>Список полученных моделей.</returns>
        Task<List<TestModel>> GetDataFromDatabaseAsync(string databaseName, string identifier);

        /// <summary>
        /// Добавить или удалить данные из базы данных.
        /// </summary>
        /// <param name="firstData">первый список моделей.</param>
        /// <param name="secondData">второй список моделей.</param>
        /// <param name="remove">удалить данные.</param>
        /// <param name="databaseName">название базы данных.</param>
        /// <returns>Результат операции.</returns>
        Task<bool> AddOrDeleteDataToDatabaseAsync(List<TestModel> firstData, List<TestModel> secondData, bool remove, string databaseName);

        /// <summary>
        /// Обновить данные на сервере.
        /// </summary>
        /// <param name="firstData">первый список моделей.</param>
        /// <param name="secondData">второй список моделей.</param>
        /// <param name="databaseName">название базы данных.</param>
        /// <returns>Результат операции.</returns>
        Task<bool> UpdateDataToServerAsync(List<TestModel> firstData, List<TestModel> secondData, string databaseName);
    }
}
