using Imagile.Data.Shared.Connection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Imagile.Data.Shared;
public static class DependencyExtensions
{
    public static IServiceCollection AddSharedData(this IServiceCollection services)
    {
        services.TryAddSingleton<IDatabaseConnectionStringResolver, DatabaseConnectionStringResolver>();
        services.AddDbContext<SharedDbContext>((serviceProvider, dbContextOptionsBuilder) =>
        {
            var resolver = serviceProvider.GetRequiredService<IDatabaseConnectionStringResolver>();
            dbContextOptionsBuilder.UseSqlServer(resolver.ResolveSharedConnectionString().ToString(),
                options => options.UseAzureSqlDefaults());
        });
        return services;
    }



}
