using SecurityDatabaseSync.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecurityDatabaseSync.BLL.Interfaces
{
    interface IBulkSyncController
    {
        Task<(List<Pledge> clientTable, List<Pledge> serverTable)> GetAllDataFromDatabasesAsync();
        List<Pledge> GetAllСonditionRecords(List<Pledge> clientTable);
        Task DeleteAllDataFromClientDatabaseAsync(List<Pledge> clientTable);
        Task DeleteAllDataFromServerDatabaseAsync(List<Pledge> serverTable);
        Task InsertAllDataToClientDatabaseAsync(List<Pledge> clientTable);
        Task InsertAllDataToServerDatabaseAsync(List<Pledge> serverTable);
        Task SaveAllDataToClientDatabaseAsync();
        Task SaveAllDataToServerDatabaseAsync();
    }
}
