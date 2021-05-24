using Secure.SecurityDatabaseSync.DAL.Constants;
using Secure.SecurityDatabaseSync.DAL.Contexts;
using Secure.SecurityDatabaseSync.DAL.Extensions;
using Secure.SecurityDatabaseSync.DAL.Models;
using Secure.SecurityDatabaseSync.Seed.Resources;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Secure.SecurityDatabaseSync.Seed
{
    internal class Program
    {
        private async static Task Main()
        {
            string UserInput(string message)
            {
                Console.Write(message);
                return Console.ReadLine();
            }

            Console.WriteLine(MessageResource.Start);

            var databaseName = UserInput(MessageResource.EnterDatabaseName);
            var code = UserInput(MessageResource.EnterCodeName);
            var count = int.Parse(UserInput(MessageResource.EnterCount));

            if (count > 0)
            {
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
                    using ApplicationContext context = databaseName
                        .GetApplicationContext(
                            AppSettingConstant.AppSettingJsonName,
                            AppSettingConstant.ConnectionStringSection);

                    context.Database.EnsureCreated();
                    await context.Commons.AddRangeAsync(GetModels());
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(MessageResource.Error);
                    Console.WriteLine(ex.StackTrace);
                }
            }

            Console.WriteLine(MessageResource.End);
        }
    }
}
