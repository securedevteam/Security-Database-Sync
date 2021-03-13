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
        private readonly FirstAppContext _firstAppContext;
        private readonly SecondAppContext _secondAppContext;

        public BulkSyncService(
            FirstAppContext firstAppContext,
            SecondAppContext secondAppContext)
        {
            _firstAppContext = firstAppContext ?? throw new ArgumentNullException(nameof(firstAppContext));
            _secondAppContext = secondAppContext ?? throw new ArgumentNullException(nameof(secondAppContext));
        }

        public async Task RunAsync()
        {
            await DeleteAsync();
            await AddAsync();
            await UpdateAsync();
        }

        private async Task DeleteAsync()
        {
            var firstAppContextModels =
                await _firstAppContext.FirstModels
                    .AsNoTracking()
                    .ToListAsync();

            var secondAppContextModels =
                await _secondAppContext.SecondModels
                    .AsNoTracking()
                    .ToListAsync();

            var idsToDelete =
                secondAppContextModels
                    .Select(model => model.AnotherSystemId)
                    .Except(firstAppContextModels
                        .Select(model => model.Id));

            if (idsToDelete.Any())
            {
                var modelsToDelete = secondAppContextModels
                        .Where(model => idsToDelete.Contains(model.AnotherSystemId))
                        .ToList();

                await _secondAppContext.BulkDeleteAsync(modelsToDelete);
            }
        }

        private async Task AddAsync()
        {
            var firstAppContextModels =
                await _firstAppContext.FirstModels
                    .AsNoTracking()
                    .ToListAsync();

            var secondAppContextModels =
                await _secondAppContext.SecondModels
                    .AsNoTracking()
                    .ToListAsync();

            var idsToAdd =
                firstAppContextModels
                    .Select(model => model.Id)
                    .Except(secondAppContextModels
                        .Select(model => model.AnotherSystemId));

            if (idsToAdd.Any())
            {
                IEnumerable<SecondModel> GetSecondModelsToAdd()
                {
                    var modelsToAdd = firstAppContextModels.Where(model => idsToAdd.Contains(model.Id));

                    foreach (var firstModel in modelsToAdd)
                    {
                        yield return new SecondModel
                        {
                            AnotherSystemId = firstModel.Id,
                            Name = firstModel.Name,
                            Updated = DateTime.Now,
                        };
                    }
                }

                var modelsToAdd = GetSecondModelsToAdd().ToList();
                await _secondAppContext.BulkInsertAsync(modelsToAdd);
            }
        }

        private async Task UpdateAsync()
        {
            var firstAppContextModels =
                await _firstAppContext.FirstModels
                    .AsNoTracking()
                    .ToListAsync();

            var secondAppContextModels =
                await _secondAppContext.SecondModels
                    .AsNoTracking()
                    .ToListAsync();


            IEnumerable<SecondModel> GetSecondModelsToUpdate()
            {
                foreach (var secondModel in secondAppContextModels)
                {
                    var firstModel = firstAppContextModels.FirstOrDefault(model => model.Id == secondModel.AnotherSystemId);
                    var isUpdated = false;

                    if (secondModel.Name != firstModel.Name)
                    {
                        isUpdated = true;
                        secondModel.Name = firstModel.Name;
                        secondModel.Updated = DateTime.Now;
                    }

                    if (isUpdated)
                    {
                        yield return secondModel;
                    }
                }
            }

            var modelsToUpdate = GetSecondModelsToUpdate().ToList();
            if (modelsToUpdate.Any())
            {
                await _secondAppContext.BulkUpdateAsync(modelsToUpdate);
            }
        }
    }
}
