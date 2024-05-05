using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Imagile.Data.Shared.Connection;

public class ImagileDatabaseConnection
{
    public ImagileDatabaseConnection()
    {
        
    }
    public ImagileDatabaseConnection(string connectionString)
    {
        var builder = new SqlConnectionStringBuilder(connectionString);
        builder.DataSource = builder.DataSource;
        builder.InitialCatalog = builder.InitialCatalog;
    }

    public string DataSource { get; set; } = string.Empty;
    public string InitialCatalog { get; set; } = string.Empty;
}