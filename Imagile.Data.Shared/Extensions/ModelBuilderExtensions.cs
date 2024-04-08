
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
/// <summary>
/// Extensions for the model builder. Since this class library is seen by the company library, these extensions are available to all EF projects.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    /// Sets the default decimal precision and scale
    /// </summary>
    /// <param name="modelBuilder"></param>
    /// <param name="defaultPrecision"></param>
    /// <param name="defaultScale"></param>
    public static void SetDecimalDefaultPrecisionAndScale(this ModelBuilder modelBuilder, short defaultPrecision, short defaultScale)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(x => x.GetProperties())
                     .Where(x => x.ClrType == typeof(decimal) || x.ClrType == typeof(decimal?)))
        {
            var columnAttribute = property?.PropertyInfo?.GetCustomAttributes<ColumnAttribute>().FirstOrDefault();
            var precisionAttribute = property?.PropertyInfo?.GetCustomAttributes<PrecisionAttribute>().FirstOrDefault();
            if (columnAttribute == null && precisionAttribute == null)
            {
                property?.SetColumnType($"decimal({defaultPrecision},{defaultScale})");
            }
        }
    }
    /// <summary>
    /// Sets the default DateTimeOffset precision.
    /// </summary>
    /// <param name="modelBuilder"></param>
    /// <param name="defaultPrecision"></param>
    public static void SetDateTimeOffsetDefaultPrecision(this ModelBuilder modelBuilder, short defaultPrecision)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(x => x.GetProperties())
                     .Where(x => x.ClrType == typeof(DateTimeOffset) || x.ClrType == typeof(DateTimeOffset?)))
        {
            var columnAttribute = property?.PropertyInfo?.GetCustomAttributes<ColumnAttribute>().FirstOrDefault();
            var precisionAttribute = property?.PropertyInfo?.GetCustomAttributes<PrecisionAttribute>().FirstOrDefault();
            if (columnAttribute == null && precisionAttribute == null)
            {
                property?.SetColumnType($"datetimeoffset({defaultPrecision})");
            }
        }
    }
}
