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
    public class DefaultSyncTask : ISyncTask, IDisposable
    {
        private readonly ApplicationContext _sourceContext;
        private readonly ApplicationContext _targetContext;
        private readonly string _code;

        private Common[] _sourceModels;
        private Common[] _targetModels;

        public DefaultSyncTask(
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
                .ToArrayAsync();

            _targetModels = await _targetContext.Commons
                .AsNoTracking()
                .Where(model => model.Code == _code)
                .ToArrayAsync();

            await DeleteAsync();
            await AddAsync();
            await UpdateAsync();
        }

        private async Task DeleteAsync()
        {
            var targetModelsToDelete = _targetModels
                .Select(targetModel => targetModel.InternalNumber)
                .Except(_sourceModels
                    .Select(sourceModel => sourceModel.InternalNumber))
                .Join(_targetModels,
                    sourceModelNumber => sourceModelNumber,
                    targetModel => targetModel.InternalNumber,
                    (sourceModelNumber, targetModel) => targetModel)
                .ToArray();

            if (targetModelsToDelete.Any())
            {
                _targetContext.Commons.RemoveRange(targetModelsToDelete);
                await _targetContext.SaveChangesAsync();
            }
        }

        private async Task AddAsync()
        {
            var targetModelsToAdd = _sourceModels
                .Select(sourceModel => sourceModel.InternalNumber)
                .Except(_targetModels
                    .Select(targetModel => targetModel.InternalNumber))
                .Join(_sourceModels,
                    targetModelNumber => targetModelNumber,
                    sourceModel => sourceModel.InternalNumber,
                    (targetModelNumber, sourceModel) => sourceModel)
                .Select(model =>
                    new Common
                    {
                        InternalNumber = model.InternalNumber,
                        Code = model.Code,
                        Name = model.Name,
                        Updated = DateTime.Now,
                    })
                .ToArray();

            if (targetModelsToAdd.Any())
            {
                await _targetContext.Commons.AddRangeAsync(targetModelsToAdd);
                await _targetContext.SaveChangesAsync();
            }
        }

        private async Task UpdateAsync()
        {
            var models = _sourceModels
                .Join(_targetModels,
                    source => source.InternalNumber,
                    target => target.InternalNumber,
                    (source, target) => (source, target))
                .Select(tuple => {
                    var (sourceModel, targetModel) = tuple;

                    var isUpdated = false;

                    if (targetModel.Name != sourceModel.Name)
                    {
                        isUpdated = true;
                        targetModel.Name = sourceModel.Name;
                    }

                    return (isUpdated, sourceModel);
                })
                .Where(tuple => tuple.isUpdated)
                .Select(tuple => new {
                    tuple.sourceModel.InternalNumber,
                    tuple.sourceModel.Name,
                })
                .ToArray();

            if (models.Any())
            {
                foreach (var model in models)
                {
                    var common = _targetModels
                        .First(targetModel =>
                            targetModel.InternalNumber == model.InternalNumber);

                    common.Name = model.Name;
                    common.Updated = DateTime.Now;

                    _targetContext.Commons.Update(common);
                }

                await _targetContext.SaveChangesAsync();
            }
        }
    }
}
