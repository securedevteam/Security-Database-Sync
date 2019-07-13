using System.Threading.Tasks;

namespace SecurityDatabaseSync.UI.ConsoleApp.Interfaces
{
    /// <summary>
    /// Интерфейсс для динамической смены синхронизации.
    /// </summary>
    public interface ISyncStart
    {
        /// <summary>
        /// Запуск синхронизации.
        /// </summary>
        Task SyncStart();
    }
}
