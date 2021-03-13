using System.Threading.Tasks;

namespace Secure.SecurityDatabaseSync.BLL.Interfaces
{
    /// <summary>
    /// Default synchronization service.
    /// </summary>
    public interface IDefaultSyncService
    {
        /// <summary>
        /// Run.
        /// </summary>
        Task RunAsync();
    }
}
