using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Secure.SecurityDatabaseSync.DAL.Contexts;

namespace Secure.SecurityDatabaseSync.DAL.Extensions
{
    /// <summary>
    /// Application context extension.
    /// </summary>
    public static class ApplicationContextExtension
    {
        /// <summary>
        /// Get application context.
        /// </summary>
        /// <param name="databaseName">Database name.</param>
        /// <param name="appSettingJson">App settings json file name.</param>
        /// <param name="section">Connection string section.</param>
        /// <returns>Application context.</returns>
        public static ApplicationContext GetApplicationContext(
            this string databaseName,
            string appSettingJson,
            string section) =>
                new ApplicationContext(
                    new DbContextOptionsBuilder<ApplicationContext>()
                        .UseSqlServer(new ConfigurationBuilder()
                            .AddJsonFile(appSettingJson, optional: true, reloadOnChange: true)
                            .Build()
                            .GetSection(section)
                            .Value
                            .Replace("%databaseName%", databaseName))
                        .Options);
    }
}
