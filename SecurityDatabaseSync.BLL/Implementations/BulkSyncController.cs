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
    //public class BulkSyncController 
    //    //: IBulkSyncController
    //{
    //    private readonly ApplicationContextClient _clientContext;
    //    private readonly ApplicationContextServer _serverContext;
    //    private readonly int _clientNumber;

    //    public BulkSyncController(ApplicationContextClient client, ApplicationContextServer server)
    //    {
    //        _clientContext = client ?? throw new ArgumentNullException(nameof(client));
    //        _serverContext = server ?? throw new ArgumentNullException(nameof(server));
    //    }

    //    public BulkSyncController(ApplicationContextClient client, ApplicationContextServer server, int clientNumber)
    //    {
    //        _clientContext = client ?? throw new ArgumentNullException(nameof(client));
    //        _serverContext = server ?? throw new ArgumentNullException(nameof(server));
    //        _clientNumber = clientNumber;
    //    }

    //    public List<TestModel> GetAllDataFromClientDatabases()
    //    {
    //        var clientTable = _clientContext.TestModelTable.ToList();

    //        return clientTable;
    //    }

    //    public List<TestModel> GetAllDataFromServerDatabases()
    //    {
    //        var serverTable = _serverContext.TestModelTable.ToList();

    //        return serverTable;
    //    }

    //    public List<Pledge> GetAllСonditionRecords(List<Pledge> clientTable)
    //    {
    //        var _selectedTableClient = clientTable.Select(record => record)
    //                                              .Where(r => r.Code == _clientNumber)
    //                                              .ToList();
    //        return _selectedTableClient;
    //    }

    //    public void DeleteAllDataFromClientDatabaseAsync(List<TestModel> clientTable)
    //    {
    //        _clientContext.BulkDelete(clientTable);
    //    }

    //    public void DeleteAllDataFromServerDatabaseAsync(List<TestModel> serverTable)
    //    {
    //        _serverContext.BulkDelete(serverTable);
    //    }

    //    public void InsertAllDataToClientDatabaseAsync(List<TestModel> clientTable)
    //    {
    //        _clientContext.BulkInsert(clientTable);
    //    }

    //    public void InsertAllDataToServerDatabase(List<TestModel> serverTable)
    //    {
    //        _serverContext.BulkInsert(serverTable);
    //    }

    //    public void SaveAllDataToClientDatabase()
    //    {
    //        _clientContext.BulkSaveChanges();
    //    }

    //    public void SaveAllDataToServerDatabase()
    //    {
    //        _serverContext.BulkSaveChanges();
    //    }
    //}
}
