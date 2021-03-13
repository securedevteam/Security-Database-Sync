using System.Threading.Tasks;

namespace Secure.SecurityDatabaseSync.BLL.Interfaces
{
    /// <summary>
    /// Hard bulk synchronization service.
    /// </summary>
    public interface IHardBulkSyncService
    {
        /// <summary>
        /// Run.
        /// </summary>
        Task RunAsync();
    }
}
