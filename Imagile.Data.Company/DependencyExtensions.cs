using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imagile.Data.Company;
public static class DependencyExtensions
{
    public static IServiceCollection AddCompanyData(this IServiceCollection services, bool useCaching)
    {

        if (useCaching)
            services.TryAddSingleton<ICompanyDbContextProvider, CachedCompanyDbContextProvider>();
        else
            services.TryAddSingleton<ICompanyDbContextProvider, CompanyDbContextProvider>();
        return services;
    }
}
