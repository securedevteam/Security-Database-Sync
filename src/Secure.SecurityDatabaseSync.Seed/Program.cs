using Secure.SecurityDatabaseSync.DAL.Contexts;
using Secure.SecurityDatabaseSync.DAL.Models;
using System;
using System.Collections.Generic;

namespace Secure.SecurityDatabaseSync.Seed
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Database filling started..");

            IEnumerable<FirstModel> GetFirstModels()
            {
                for (int i = 0; i < 1000; i++)
                {
                    yield return new FirstModel
                    {
                        Name = Guid.NewGuid().ToString(),
                        Description = Guid.NewGuid().ToString(),
                    };
                }
            }

            try
            {
                using var context = new FirstAppContext();
                await context.FirstModels.AddRangeAsync(GetFirstModels());
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
