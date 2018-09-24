using Lab.Toggler.Infra.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Toggler.Tests.Integration
{
    public class SqliteIntegrationTest
    {
        private readonly DbContextOptions<TogglerContext> _options;

        public SqliteIntegrationTest()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            _options = new DbContextOptionsBuilder<TogglerContext>()
                    .UseSqlite(connection)
                    .Options;

            using (var context = new TogglerContext(_options))
            {
                context.Database.EnsureCreated();
            }
        }

        public async Task<T> ExecuteCommand<T>(Func<TogglerContext, Task<T>> action)
        {
            using (var context = new TogglerContext(_options))
            {
                return await action(context);
            }
        }

        public async Task ExecuteCommand(Func<TogglerContext, Task> action)
        {
            using (var context = new TogglerContext(_options))
            {
                await action(context);
            }
        }

        public async Task AddEntity(params object[] entities)
        {
            using (var context = new TogglerContext(_options))
            {
                foreach (var item in entities)
                {
                    await context.AddAsync(item);
                }
                context.SaveChanges();
            }
        }
    }
}
