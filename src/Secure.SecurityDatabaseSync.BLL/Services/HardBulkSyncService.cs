using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Secure.SecurityDatabaseSync.BLL.Interfaces;
using Secure.SecurityDatabaseSync.DAL.Contexts;
using Secure.SecurityDatabaseSync.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Secure.SecurityDatabaseSync.BLL.Services
{
    public class HardBulkSyncService : IHardBulkSyncService, IDisposable
    {
        private readonly ApplicationContext _firstAppContext;
        private readonly ApplicationContext _secondAppContext;
        private readonly string _code;

        public HardBulkSyncService(
            string source,
            string target,
            string code)
        {
            _firstAppContext = new ApplicationContext(source);
            _secondAppContext = new ApplicationContext(target);
            _code = code;
        }

        public void Dispose()
        {
            _firstAppContext.Dispose();
            _secondAppContext.Dispose();
        }

        public async Task RunAsync()
        {
            var firstAppContextModels =
                await _firstAppContext.Commons
                    .AsNoTracking()
                    .Where(model => model.Code == _code)
                    .ToListAsync();

            var secondAppContextModels =
                await _secondAppContext.Commons
                    .AsNoTracking()
                    .Where(model => model.Code == _code)
                    .ToListAsync();

            await _secondAppContext.BulkDeleteAsync(secondAppContextModels);

            IEnumerable<Common> GetModelsToAdd()
            {
                foreach (var firstModel in firstAppContextModels)
                {
                    yield return new Common
                    {
                        InternalNumber = firstModel.InternalNumber,
                        Code = _code,
                        Name = firstModel.Name,
                        Updated = DateTime.Now,
                    };
                }
            }

            var modelsToAdd = GetModelsToAdd().ToList();
            await _secondAppContext.BulkInsertAsync(modelsToAdd);
        }
    }
}
