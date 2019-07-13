using System.Threading.Tasks;

namespace SecurityDatabaseSync.BLL.Interfaces
{
    /// <summary>
    /// Интерфейс для жесткой синхронизации.
    /// </summary>
    public interface IHardSyncController
    {
        /// <summary>
        /// Добавить данные в конкретную базу данных.
        /// </summary>
        Task InsertDataAsync(string databaseName, string identifier);

        /// <summary>
        /// Удалить данные из конкретной базы данных.
        /// </summary>
        Task ClearDataAsync(string databaseName);

        /// <summary>
        /// Обновить данные из конкретной базы данных.
        /// </summary>
        /// <param name="dbFirst">первая база данных.</param>
        /// <param name="dbSecond">вторая база данеых.</param>
        Task CopyDataAsync(string dbFirst, string dbSecond, string identifier);
    }
}
