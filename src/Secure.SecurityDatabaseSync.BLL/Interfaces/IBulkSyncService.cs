using System.Threading.Tasks;

namespace Secure.SecurityDatabaseSync.BLL.Interfaces
{
    /// <summary>
    /// Bulk synchronization service.
    /// </summary>
    public interface IBulkSyncService
    {
        /// <summary>
        /// Run.
        /// </summary>
        Task RunAsync();
    }
}
