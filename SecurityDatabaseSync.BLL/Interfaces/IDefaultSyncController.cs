using SecurityDatabaseSync.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityDatabaseSync.BLL.Interfaces
{
    public interface IDefaultSyncController
    {
        List<TestModel> GetDataFromDatabase(string databaseName);
        List<TestModel> GetDataWithFilterFromDatabase(string databaseName, string identifier);
        bool AddOrDeleteDataToDatabase(List<TestModel> firstData, List<TestModel> secondData, bool remove, string databaseName);
    }
}
