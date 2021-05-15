using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Secure.SecurityDatabaseSync.BLL.Interfaces;
using Secure.SecurityDatabaseSync.DAL.Contexts;
using Secure.SecurityDatabaseSync.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Secure.SecurityDatabaseSync.BLL.Tasks
{
    /// <inheritdoc cref="ISyncTask"/>
    public class BulkSyncTask : ISyncTask, IDisposable
    {
        private readonly ApplicationContext _sourceContext;
        private readonly ApplicationContext _targetContext;
        private readonly string _code;

        private IList<Common> _sourceModels;
        private IList<Common> _targetModels;

        public BulkSyncTask(
            ApplicationContext sourceContext,
            ApplicationContext targetContext,
            string code)
        {
            _sourceContext = sourceContext ?? throw new ArgumentNullException(nameof(sourceContext));
            _targetContext = targetContext ?? throw new ArgumentNullException(nameof(targetContext));
            _code = code.ToUpper();
        }

        public void Dispose()
        {
            _sourceContext.Dispose();
            _targetContext.Dispose();

            GC.SuppressFinalize(this);
        }

        public async Task RunAsync()
        {
            _sourceModels = await _sourceContext.Commons
                .AsNoTracking()
                .Where(model => model.Code == _code)
                .ToListAsync();

            _targetModels = await _targetContext.Commons
                .AsNoTracking()
                .Where(model => model.Code == _code)
                .ToListAsync();

            if (_targetModels.Any())
            {
                await DeleteAsync();
            }

            if (_sourceModels.Any())
            {
                await AddAsync();
                await UpdateAsync();
            }
        }

        private async Task DeleteAsync()
        {
            var idsToDelete = _targetModels
                .Select(model => model.InternalNumber)
                .Except(_sourceModels
                    .Select(model => model.InternalNumber));

            if (idsToDelete.Any())
            {
                List<Common> modelsToDelete = _targetModels
                    .Where(model => idsToDelete.Contains(model.InternalNumber))
                    .ToList();

                await _targetContext.BulkDeleteAsync(modelsToDelete);
            }
        }

        private async Task AddAsync()
        {
            var idsToAdd = _sourceModels
                .Select(model => model.InternalNumber)
                .Except(_targetModels
                    .Select(model => model.InternalNumber));

            if (idsToAdd.Any())
            {
                IEnumerable<Common> GetModelsToAdd()
                {
                    var sourceModelsToAdd = _sourceModels
                        .Where(model => idsToAdd.Contains(model.InternalNumber));

                    foreach (var sourceModel in sourceModelsToAdd)
                    {
                        yield return new Common
                        {
                            InternalNumber = sourceModel.InternalNumber,
                            Code = sourceModel.Code,
                            Name = sourceModel.Name,
                            Updated = DateTime.Now,
                        };
                    }
                }

                List<Common> modelsToAdd = GetModelsToAdd().ToList();
                await _targetContext.BulkInsertAsync(modelsToAdd);
            }
        }

        private async Task UpdateAsync()
        {
            IEnumerable<Common> GetModelsToUpdate()
            {
                foreach (var targetModel in _targetModels)
                {
                    var sourceModel = _sourceModels
                        .FirstOrDefault(model => model.InternalNumber == targetModel.InternalNumber);

                    var isUpdated = false;

                    if (targetModel.Name != sourceModel.Name)
                    {
                        isUpdated = true;
                        targetModel.Name = sourceModel.Name;
                        targetModel.Updated = DateTime.Now;
                    }

                    if (isUpdated)
                    {
                        yield return targetModel;
                    }
                }
            }

            List<Common> modelsToUpdate = GetModelsToUpdate().ToList();
            if (modelsToUpdate.Any())
            {
                await _targetContext.BulkUpdateAsync(modelsToUpdate);
            }
        }
    }
}
