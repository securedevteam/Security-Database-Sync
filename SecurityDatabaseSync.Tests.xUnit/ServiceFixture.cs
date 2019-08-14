using Microsoft.Extensions.DependencyInjection;
using SecurityDatabaseSync.DAL;
using System;
using Microsoft.EntityFrameworkCore;
using SecurityDatabaseSync.BLL.Interfaces;
using SecurityDatabaseSync.BLL.Implementations;

namespace Secure.SecurityDatabaseSync.Tests
{
    public class ServiceFixture
    {
        public ServiceProvider ServiceProvider { get; set; }

        public ServiceFixture()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()), ServiceLifetime.Transient);

            serviceCollection.AddScoped<IDefaultSyncController, BulkDefaultSyncController>();
            serviceCollection.AddScoped<IDefaultSyncController, DefaultSyncController>();
            serviceCollection.AddScoped<ISyncController, BulkHardSyncController>();
            serviceCollection.AddScoped<ISyncController, HardSyncController>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
