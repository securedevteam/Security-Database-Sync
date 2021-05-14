using System.Threading.Tasks;

namespace Secure.SecurityDatabaseSync.BLL.Interfaces
{
    /// <summary>
    /// Synchronization task.
    /// </summary>
    public interface ISyncTask
    {
        /// <summary>
        /// Run.
        /// </summary>
        Task RunAsync();
    }
}
