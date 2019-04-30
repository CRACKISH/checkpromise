using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CheckPromise.Data.DataContext
{
    class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        private static string _connectionString;
        [JsonProperty("DefaultConnection")]
        public string DefaultConnection { get; set; }

        public DataContext CreateDbContext()
        {
            return CreateDbContext(null);
        }

        public DataContext CreateDbContext(string[] args)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                LoadConnectionString();
            }

            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseSqlServer(_connectionString);

            return new DataContext(builder.Options);
        }

        public DataContext CreateDbContextFromConnectionString(string connectionString)
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseSqlServer(connectionString);

            return new DataContext(builder.Options);
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
