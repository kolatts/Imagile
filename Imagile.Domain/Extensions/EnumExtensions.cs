using Imagile.Domain.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Imagile.Domain.Extensions;

/// <summary>
/// General extensions on enum values 
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// This method can be used to get all the Enums specified by <see cref="RequiresAttribute{TEnum}" />.
    /// This is not recursive, and only returns the direct RequiresAttribute values.
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static IEnumerable<TEnum> GetRequired<TEnum>(this TEnum value)
        where TEnum : struct, Enum
    {
        var attribute = value.GetAttributeFirstOrDefault<RequiresAttribute<TEnum>, TEnum>();
        var required = attribute?.Required.Where(x => !Equals(x, value)).ToList() ?? [];
        return required;
    }   
    /// <summary>
    /// Gets the included values of the enum, based on the [Includes<T></T>] attribute or derived attributes.
    /// This runs recursively to get all included values.
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static IEnumerable<TEnum> GetIncluded<TEnum>(this TEnum value)
        where TEnum : struct, Enum
    {
        return value.GetIncluded([value])
            .Where(x => !Equals(x, value));
    }

    private static IEnumerable<TEnum> GetIncluded<TEnum>(this TEnum value, HashSet<TEnum> visited)
        where TEnum : struct, Enum
    {
        visited.Add(value);
        var attribute = value.GetAttributeFirstOrDefault<IncludesAttribute<TEnum>, TEnum>();
        var included = attribute?.Includes.Where(x => !Equals(x, value)).ToList() ?? [];
        included.RemoveAll(visited.Contains);
        var recursivelyIncluded = Array.Empty<TEnum>();
        foreach (var include in included)
        {
            recursivelyIncluded = recursivelyIncluded.Union(include.GetIncluded(visited)).ToArray();
        }
        return visited.Distinct();
    }
    /// <summary>
    /// Wraps the System.Enum.IsDefined for easier syntax, to check if a value (usually cast from int) is valid.
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsDefined<TEnum>(this TEnum value)
        where TEnum : Enum
    {
        return System.Enum.IsDefined(value.GetType(), value);
    }
    /// <summary>
    /// Checks whether the value supplied is a valid combination of the flags
    /// </summary>
    /// <typeparam name="TEnum">Must use the FlagsAttribute decorator.</typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsFlagsEnumDefined<TEnum>(this TEnum value)
        where TEnum : Enum
    {
        var type = typeof(TEnum);
        if (!type.GetCustomAttributes(true).Any(attribute => attribute is FlagsAttribute))
        {
            throw new ArgumentException($"{type.Name} is not using the [Flags] attribute; use of this attribute is required.");
        }
        var numericValue = Convert.ToInt64(value);
        if (numericValue < 0) { throw new ArgumentException("Negative values cannot be valid Enums"); }
        Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList().ForEach(validValue =>
        {
            if (value.HasFlag(validValue))
                numericValue &= ~Convert.ToInt64(validValue); //bitwise operation to remove the flag. Cannot perform on T directly.
        });
        return numericValue == 0;
    }
    /// <summary>
    /// Provides the [Description] attribute's value, or the name if no Description.
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetDescription<TEnum>(this TEnum value)
        where TEnum : Enum
    {
        return GetAttributeFirstOrDefault<DescriptionAttribute, TEnum>(value)?.Description ?? value.ToString();
    }
    /// <summary>
    /// Gets the [Display] attribute's name property
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetDisplayName<TEnum>(this TEnum value)
        where TEnum : Enum
    {
        return GetAttributeFirstOrDefault<DisplayAttribute, TEnum>(value)?.GetName()
               ?? GetAttributeFirstOrDefault<DisplayNameAttribute, TEnum>(value)?.DisplayName
               ?? value.ToString();
    }

    /// <summary>
    /// Provides the [Category] attribute's value
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="value"></param>
    /// <param name="valueIfNone"></param>
    /// <returns></returns>
    public static string? GetCategory<TEnum>(this TEnum value, string? valueIfNone = "[None]")
        where TEnum : Enum
    {
        return GetAttributeFirstOrDefault<CategoryAttribute, TEnum>(value)?.Category ?? valueIfNone;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TAttribute? GetAttributeFirstOrDefault<TAttribute, TEnum>(this TEnum value)
        where TAttribute : Attribute
        where TEnum : Enum
    {
        var type = value.GetType();
        var memberInfos = type.GetMember(value.ToString());
        return memberInfos.FirstOrDefault()
            ?.GetCustomAttributes(typeof(TAttribute), false)
            ?.Cast<TAttribute>()
            .FirstOrDefault();
    }

    public static IEnumerable<TAttribute> GetAttributes<TAttribute, TEnum>(this TEnum value)
        where TAttribute : Attribute
        where TEnum : Enum
    {
        var type = value.GetType();
        var memberInfos = type.GetMember(value.ToString());
        return memberInfos.First()
            .GetCustomAttributes(typeof(TAttribute), false)
            .Cast<TAttribute>();
    }


    /// <summary>
    /// Try to parse an enum value from a [Description] attribute input value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumType"></param>
    /// <param name="description"></param>
    /// <param name="result"></param>
    /// <exception cref="InvalidOperationException">If multiple description value matches are found</exception>
    /// <returns></returns>
    public static bool TryParseDescription<T>(this Type enumType, string description, out T? result) where T : struct, Enum
    {
        if (enumType != typeof(T)) throw new ArgumentException($"The Type Argument {typeof(T).Name} must be the same as the ended Type {enumType.Name}");
        var match = Enum.GetValues<T>()
            .Select(x => new { enumValue = x, description = x.GetDescription() })
            .Where(x =>
                string.Equals(description, x.description, StringComparison.InvariantCultureIgnoreCase))
            .Select(x => x.enumValue)
            .Cast<T?>()
            .ToList();
        result = match.SingleOrDefault();
        return match.Count != 0;
    }
    /// <summary>
    /// Returns enum value if a description match is found
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumType"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">When the description is not found in the enum members</exception>
    public static T ParseDescription<T>(this Type enumType, string description)
        where T : struct, Enum
    {
        if (TryParseDescription<T>(enumType, description, out var result) && result != null) return result.Value;
        throw new ArgumentException($"Description is not in type {enumType.Name}.");
    }
    /// <summary>
    /// Returns enum value if a description match is found, or a null value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumType"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    public static T? ParseDescriptionOrNull<T>(this Type enumType, string? description)
        where T : struct, Enum
    {
        if (TryParseDescription<T>(enumType, description ?? string.Empty, out var result) && result != null) return result.Value;
        return null;
    }

    //public static List<DropDownListModel<T>> GetDropDownListModels<T>()
    //where T : struct, Enum
    //{
    //    return Enum.GetValues<T>().ToDropDownListModels();

    //}

    //public static List<DropDownListModel<T>> ToDropDownListModels<T>(this IEnumerable<T> enumValues)
    //where T : struct, Enum
    //=> enumValues.Select(x => new DropDownListModel<T>()
    //{
    //    Value = x,
    //    Text = x.GetDisplayName()
    //})
    //    .ToList();



}