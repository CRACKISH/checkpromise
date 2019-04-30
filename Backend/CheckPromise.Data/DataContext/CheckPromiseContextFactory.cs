using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CheckPromise.Data.DataContext
{
    class CheckPromiseContextFactory : IDesignTimeDbContextFactory<CheckPromiseContext>
    {
        private static string _connectionString;
        [JsonProperty("DefaultConnection")]
        public string DefaultConnection { get; set; }

        public CheckPromiseContext CreateDbContext()
        {
            return CreateDbContext(null);
        }

        public CheckPromiseContext CreateDbContext(string[] args)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                LoadConnectionString();
            }

            var builder = new DbContextOptionsBuilder<CheckPromiseContext>();
            builder.UseSqlServer(_connectionString);

            return new CheckPromiseContext(builder.Options);
        }

        public CheckPromiseContext CreateDbContextFromConnectionString(string connectionString)
        {
            var builder = new DbContextOptionsBuilder<CheckPromiseContext>();
            builder.UseSqlServer(connectionString);

            return new CheckPromiseContext(builder.Options);
        }

        private static void LoadConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
