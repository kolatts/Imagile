using Imagile.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imagile.Data.Shared.Extensions;
public static class SqlConnectionStringBuilderExtensions
{
    public static void AddLocalAuthentication(this SqlConnectionStringBuilder builder)
    {
        if (builder.DataSource.Contains("localhost", StringComparison.InvariantCultureIgnoreCase))
        {
            builder.TrustServerCertificate = true;
            builder.UserID = "sa";
            builder.Password = "Temp1234!";
        }
    }
}
