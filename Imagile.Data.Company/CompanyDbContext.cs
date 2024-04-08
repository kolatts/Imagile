using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Imagile.Data.Company
{
    public interface IEntityChangeService
    {

    }
    public class CompanyDbContext : DbContext
    {

    }

    public interface ICompanyIdEntity
    {
        [Required]  
        public int? CompanyId { get; set; }
    }
    public class Person
    {

        public int PersonId { get; set; }
        [Required]
        [StringLength(255)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string? LastName { get; set; }
        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string? Email { get; set; }


    }
}
