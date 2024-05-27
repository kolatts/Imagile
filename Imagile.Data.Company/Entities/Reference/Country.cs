using Imagile.Data.Company.Interfaces;
using Imagile.Domain.Reference;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imagile.Data.Company.Entities.Reference;
public class Country : IReferenceEntity<Country>
{

    [StringLength(200)]
    public string? Name { get; set; }

    [StringLength(2)]
    [Column(TypeName = "char(2)")]
    [Unicode(false)]
    public string CountryCodeId { get; set; } = string.Empty;

    [StringLength(2)]
    [Column(TypeName = "char(2)")]
    [Unicode(false)]
    public string Alpha2Code { get; set; } = string.Empty;

    [StringLength(3)]
    [Column(TypeName = "char(3)")]
    [Unicode(false)]
    public string Alpha3Code { get; set; } = string.Empty;

    [StringLength(3)]
    [Column(TypeName = "char(3)")]
    [Unicode(false)]
    public string NumericCode { get; set; } = string.Empty;

    [StringLength(5)]
    [Unicode(false)]
    public string? PhoneDialingCodes { get; set; }


    public Country(CountryModel model)
    {
        Name = model.Name;
        Alpha2Code = CountryCodeId = model.Alpha2Code;
        Alpha3Code = model.Alpha3Code;
        NumericCode = model.NumericCode;
        PhoneDialingCodes = model.PhoneDialingCode;
    }

    public Country()
    {

    }
    public static List<Country> GetSeedData() => CountryModel.All.Select(x => new Country(x))
        .GroupBy(x => x.CountryCodeId)
        .Select(x => x.First())
        .ToList();
}
