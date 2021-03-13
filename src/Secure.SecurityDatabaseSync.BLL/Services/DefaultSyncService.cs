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
    /// <inheritdoc cref="IDefaultSyncService"/>
    public class DefaultSyncService : IDefaultSyncService, IDisposable
    {
        private readonly ApplicationContext _sourceContext;
        private readonly ApplicationContext _targetContext;
        private readonly string _code;

        public DefaultSyncService(
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
                _targetContext.Commons
                    .RemoveRange(targetModels
                        .Where(model => idsToDelete.Contains(model.InternalNumber)));

                await _targetContext.SaveChangesAsync();
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

                await _targetContext.Commons.AddRangeAsync(GetModelsToAdd());
                await _targetContext.SaveChangesAsync();
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

            var modelsToUpdate = GetModelsToUpdate();
            if (modelsToUpdate.Any())
            {
                _targetContext.Commons.UpdateRange(modelsToUpdate);
                await _targetContext.SaveChangesAsync();
            }
        }
    }
}
