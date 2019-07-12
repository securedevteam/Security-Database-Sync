using Microsoft.EntityFrameworkCore;
using SecurityDatabaseSync.BLL.Interfaces;
using SecurityDatabaseSync.DAL;
using SecurityDatabaseSync.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityDatabaseSync.BLL.Implementations
{
    public class SyncController : IBulkSyncController
    {
        private readonly ApplicationContextClient _clientContext;
        private readonly ApplicationContextServer _serverContext;
        private readonly int _clientNumber;

        public SyncController(ApplicationContextClient client, ApplicationContextServer server)
        {
            _clientContext = client ?? throw new ArgumentNullException(nameof(client));
            _serverContext = server ?? throw new ArgumentNullException(nameof(server));
        }

        public SyncController(ApplicationContextClient client, ApplicationContextServer server, int clientNumber)
        {
            _clientContext = client ?? throw new ArgumentNullException(nameof(client));
            _serverContext = server ?? throw new ArgumentNullException(nameof(server));
            _clientNumber = clientNumber;
        }

        public async Task<(List<Pledge> clientTable, List<Pledge> serverTable)> GetAllDataFromDatabasesAsync()
        {
            var result = (clientTable: new List<Pledge>(), serverTable: new List<Pledge>());

            result.clientTable = await _clientContext.PledgeTable.ToListAsync();
            result.serverTable = await _serverContext.PledgeTable.ToListAsync();

            return result;
        }

        public List<Pledge> GetAllСonditionRecords(List<Pledge> clientTable)
        {
            var _selectedTableClient = clientTable.Select(record => record)
                                                  .Where(r => r.Code == _clientNumber)
                                                  .ToList();
            return _selectedTableClient;
        }

        public async Task DeleteAllDataFromClientDatabaseAsync(List<Pledge> clientTable)
        {
            await _clientContext.BulkDeleteAsync(clientTable);
        }

        public async Task DeleteAllDataFromServerDatabaseAsync(List<Pledge> serverTable)
        {
            await _serverContext.BulkDeleteAsync(serverTable);
        }

        public async Task InsertAllDataToClientDatabaseAsync(List<Pledge> clientTable)
        {
            await _clientContext.BulkInsertAsync(clientTable);
        }

        public async Task InsertAllDataToServerDatabaseAsync(List<Pledge> serverTable)
        {
            await _serverContext.BulkInsertAsync(serverTable);
        }

        public async Task SaveAllDataToClientDatabaseAsync()
        {
            await _clientContext.BulkSaveChangesAsync();
        }

        public async Task SaveAllDataToServerDatabaseAsync()
        {
            await _serverContext.BulkSaveChangesAsync();
        }
    }
}
