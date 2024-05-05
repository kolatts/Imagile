using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imagile.Domain.Exceptions;

public class CompanyNotFoundException(int companyId) : Exception
{
    public override string Message => $"Company was not found with ID {CompanyId}";
    public int CompanyId { get; set; } = companyId;

}

public class DatabaseShardNotFoundException(int databaseShardId) : Exception
{
    public override string Message => $"Database Shard was not found with ID {DatabaseShardId}";
    public int DatabaseShardId { get; set; } = databaseShardId;

}