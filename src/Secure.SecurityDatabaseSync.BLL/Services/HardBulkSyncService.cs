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
    public class HardBulkSyncService : IHardBulkSyncService
    {
        private readonly FirstAppContext _firstAppContext;
        private readonly SecondAppContext _secondAppContext;

        public HardBulkSyncService(
            FirstAppContext firstAppContext,
            SecondAppContext secondAppContext)
        {
            _firstAppContext = firstAppContext ?? throw new ArgumentNullException(nameof(firstAppContext));
            _secondAppContext = secondAppContext ?? throw new ArgumentNullException(nameof(secondAppContext));
        }

        public async Task RunAsync()
        {
            var firstAppContextModels =
                await _firstAppContext.FirstModels
                    .AsNoTracking()
                    .ToListAsync();

            var secondAppContextModels =
                await _secondAppContext.SecondModels
                    .AsNoTracking()
                    .ToListAsync();

            await _secondAppContext.BulkDeleteAsync(secondAppContextModels);

            IEnumerable<SecondModel> GetSecondModelsToAdd()
            {
                foreach (var firstModel in firstAppContextModels)
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
}
