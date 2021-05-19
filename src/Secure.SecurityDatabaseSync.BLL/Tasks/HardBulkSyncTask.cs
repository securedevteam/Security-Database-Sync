using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Secure.SecurityDatabaseSync.BLL.Interfaces;
using Secure.SecurityDatabaseSync.DAL.Contexts;
using Secure.SecurityDatabaseSync.DAL.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Secure.SecurityDatabaseSync.BLL.Tasks
{
    /// <inheritdoc cref="ISyncTask"/>
    public class HardBulkSyncTask : ISyncTask, IDisposable
    {
        private readonly ApplicationContext _firstAppContext;
        private readonly ApplicationContext _secondAppContext;
        private readonly string _code;

        public HardBulkSyncTask(
            ApplicationContext sourceContext,
            ApplicationContext targetContext,
            string code)
        {
            _firstAppContext = sourceContext ?? throw new ArgumentNullException(nameof(sourceContext));
            _secondAppContext = targetContext ?? throw new ArgumentNullException(nameof(targetContext));
            _code = code.ToUpper();
        }

        public void Dispose()
        {
            _firstAppContext.Dispose();
            _secondAppContext.Dispose();

            GC.SuppressFinalize(this);
        }

        public async Task RunAsync()
        {
            var firstAppContextModels = await _firstAppContext.Commons
                .AsNoTracking()
                .Where(model => model.Code == _code)
                .ToListAsync();

            var secondAppContextModels = await _secondAppContext.Commons
                .AsNoTracking()
                .Where(model => model.Code == _code)
                .ToListAsync();

            await _secondAppContext.BulkDeleteAsync(secondAppContextModels);

            var modelsToAdd = firstAppContextModels
                .Select(firstModel => new Common
                {
                    InternalNumber = firstModel.InternalNumber,
                    Code = _code,
                    Name = firstModel.Name,
                    Updated = DateTime.Now,
                })
                .ToList();

            await _secondAppContext.BulkInsertAsync(modelsToAdd);
        }
    }
}
