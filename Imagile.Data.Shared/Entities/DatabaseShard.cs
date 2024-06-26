﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Imagile.Data.Shared.Entities;

public class DatabaseShard
{
    public DatabaseShard()
    {
        
    }

    public DatabaseShard(string dataSource, string initialCatalog)
    {
        
    }
    public int DatabaseShardId { get; set; }
    [Unicode(false)]
    [StringLength(512)]
    public string DataSource { get; set; } = string.Empty;
    [Unicode(false)]
    [StringLength(512)] 
    public string InitialCatalog { get; set; } = string.Empty;
    public bool IsDedicated { get; set; }
    public ICollection<CompanyConnection> CompanyConnections { get; set; } = [];
}