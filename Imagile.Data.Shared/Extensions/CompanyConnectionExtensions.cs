using Imagile.Data.Shared.Connection;
using Imagile.Data.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imagile.Data.Shared.Extensions;

public static class CompanyConnectionExtensions
{
    public static IQueryable<ImagileCompanyDatabaseConnection> ToModels(this IQueryable<CompanyConnection> query) =>
        query.Select(x => new ImagileCompanyDatabaseConnection
        {
            CompanyId = x.CompanyId,
            DataSource = x.DatabaseShard!.DataSource,
            InitialCatalog = x.DatabaseShard.InitialCatalog,
        });
}