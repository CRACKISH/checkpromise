using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Checkpromise.Persistence
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app) {
            using (var context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>())
            {
                context.Database.Migrate();
                context.SaveChanges();
            }
        }
    }
}
