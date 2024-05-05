using Imagile.Data.Company.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Imagile.Data.Company;

public class CompanyDbContext(DbContextOptions<CompanyDbContext> options, int? companyId, int? personId) : DbContext(options)
{
    public int? CompanyId { get; set; } = companyId;
    public int? PersonId { get; set; } = personId;

    public DbSet<Person> Persons { get; set; } = null!;
}