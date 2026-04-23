using CheckPromise.BusinessLayer;
using CheckPromise.Data.DataContext;
using CheckPromise.Ingestion;
using Checkpromise.Provider;
using CheckPromise.Uploader;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<CheckPromiseContext>((serviceProvider, options) =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection is not configured.");
    options.UseSqlite(connectionString);
});

builder.Services.AddIndicatorIngestion();
builder.Services.AddClientDataBuilder(builder.Configuration);
builder.Services.AddFtpClientDataProvider(builder.Configuration);

builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddOptions<UploaderOptions>()
    .Bind(builder.Configuration.GetSection(UploaderOptions.SectionName));

builder.Services.AddHostedService<UploaderWorker>();

var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CheckPromiseContext>();
    await context.Database.EnsureCreatedAsync();
}

await host.RunAsync();
