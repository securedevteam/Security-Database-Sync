using System.Threading.Tasks;

namespace Secure.SecurityDatabaseSync.BLL.Interfaces
{
    /// <summary>
    /// Synchronization service.
    /// </summary>
    public interface ISyncService
    {
        /// <summary>
        /// Run.
        /// </summary>
        Task RunAsync();
    }
}
