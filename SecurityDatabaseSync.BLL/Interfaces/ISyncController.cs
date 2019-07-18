using System.Threading.Tasks;

namespace SecurityDatabaseSync.BLL.Interfaces
{
    /// <summary>
    /// Интерфейс для жесткой синхронизации.
    /// </summary>
    public interface ISyncController
    {
        /// <summary>
        /// Добавить данные в конкретную базу данных.
        /// </summary>
        /// <param name="databaseName">название базы данных.</param>
        /// <param name="identifier">условие выборки (идентификатор).</param>
        Task InsertDataAsync(string databaseName, string identifier);

        /// <summary>
        /// Удалить данные из конкретной базы данных.
        /// </summary>
        /// <param name="databaseName">название базы данных.</param>
        Task ClearDataAsync(string databaseName);

        /// <summary>
        /// Удалить данные из конкретной базы данных по индетификатору.
        /// </summary>
        /// <param name="databaseName">название базы данных.</param>
        /// <param name="identifier">условие выборки (идентификатор).</param>
        Task ClearDataAsync(string databaseName, string identifier);

        /// <summary>
        /// Обновить данные из конкретной базы данных.
        /// </summary>
        /// <param name="dbFirst">первая база данных.</param>
        /// <param name="dbSecond">вторая база данеых.</param>
        /// <param name="identifier">условие выборки (идентификатор).</param>
        Task CopyDataAsync(string dbFirst, string dbSecond, string identifier);
    }
}
