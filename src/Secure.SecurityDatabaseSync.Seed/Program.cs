using Secure.SecurityDatabaseSync.DAL.Contexts;
using Secure.SecurityDatabaseSync.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Secure.SecurityDatabaseSync.Seed
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Database filling started..");

            Console.Write("Enter database name: ");
            var databaseName = Console.ReadLine();

            Console.Write("Enter code name: ");
            var code = Console.ReadLine();

            Console.Write("Enter count of entities: ");
            var count = int.Parse(Console.ReadLine());

            IEnumerable<Common> GetModels()
            {
                for (int i = 0; i < count; i++)
                {
                    var guid = Guid.NewGuid().ToString();

                    yield return new Common
                    {
                        InternalNumber = guid,
                        Code = code.ToUpper(),
                        Name = Regex.Replace(guid, @"\d", ""),
                        Updated = DateTime.Now,
                    };
                }
            }

            try
            {
                using var context = new ApplicationContext(databaseName);
                context.Database.EnsureCreated();
                await context.Commons.AddRangeAsync(GetModels());
                await context.SaveChangesAsync();

                Console.WriteLine("Database filling ended..");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database filling ended with error..");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
