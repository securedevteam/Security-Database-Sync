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
    /// <inheritdoc cref="IBulkSyncService"/>
    public class BulkSyncService : IBulkSyncService
    {
        private readonly ApplicationContext _sourceContext;
        private readonly ApplicationContext _targetContext;
        private readonly string _code;

        public BulkSyncService(
            string source,
            string target,
            string code)
        {
            _sourceContext = new ApplicationContext(source);
            _targetContext = new ApplicationContext(target);
            _code = code;
        }

        public void Dispose()
        {
            _sourceContext.Dispose();
            _targetContext.Dispose();
        }

        public async Task RunAsync()
        {
            await DeleteAsync();
            await AddAsync();
            await UpdateAsync();
        }

        private async Task DeleteAsync()
        {
            var sourceModels =
                await _sourceContext.Commons
                    .AsNoTracking()
                    .Where(model => model.Code == _code)
                    .ToListAsync();

            var targetModels =
                await _targetContext.Commons
                    .AsNoTracking()
                    .Where(model => model.Code == _code)
                    .ToListAsync();

            var idsToDelete =
                targetModels
                    .Select(model => model.InternalNumber)
                    .Except(sourceModels
                        .Select(model => model.InternalNumber));

            if (idsToDelete.Any())
            {
                var modelsToDelete = targetModels
                        .Where(model => idsToDelete.Contains(model.InternalNumber))
                        .ToList();

                await _targetContext.BulkDeleteAsync(modelsToDelete);
            }
        }

        private async Task AddAsync()
        {
            var sourceModels =
                await _sourceContext.Commons
                    .AsNoTracking()
                    .Where(model => model.Code == _code)
                    .ToListAsync();

            var targetModels =
                await _targetContext.Commons
                    .AsNoTracking()
                    .Where(model => model.Code == _code)
                    .ToListAsync();

            var idsToAdd =
                sourceModels
                    .Select(model => model.InternalNumber)
                    .Except(targetModels
                        .Select(model => model.InternalNumber));

            if (idsToAdd.Any())
            {
                IEnumerable<Common> GetModelsToAdd()
                {
                    var sourceModelsToAdd = sourceModels.Where(model => idsToAdd.Contains(model.InternalNumber));

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

                var modelsToAdd = GetModelsToAdd().ToList();
                await _targetContext.BulkInsertAsync(modelsToAdd);
            }
        }

        private async Task UpdateAsync()
        {
            var sourceModels =
                await _sourceContext.Commons
                    .AsNoTracking()
                    .Where(model => model.Code == _code)
                    .ToListAsync();

            var targetModels =
                await _targetContext.Commons
                    .AsNoTracking()
                    .Where(model => model.Code == _code)
                    .ToListAsync();

            IEnumerable<Common> GetModelsToUpdate()
            {
                foreach (var targetModel in targetModels)
                {
                    var sourceModel =
                        sourceModels
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

            var modelsToUpdate = GetModelsToUpdate().ToList();
            if (modelsToUpdate.Any())
            {
                await _targetContext.BulkUpdateAsync(modelsToUpdate);
            }
        }
    }
}
