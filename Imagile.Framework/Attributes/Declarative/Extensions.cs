using Imagile.Domain.Attributes.Declarative;
using Imagile.Domain.Extensions;

namespace Imagile.Framework.Attributes.Declarative;
public static class Extensions
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


    public static IEnumerable<TEnumRequired> GetRequired<TEnumSource, TEnumRequired>(this TEnumSource value)
        where TEnumSource : struct, Enum
        where TEnumRequired : struct, Enum
    {
        var attribute = value.GetAttributeFirstOrDefault<RequiresAttribute<TEnumRequired>, TEnumSource>();
        var required = attribute?.Required.ToList() ?? [];
        return required;
    }
    /// <summary>
    /// Gets the included values of the enum, based on the [Includes<T></T>] attribute or derived attributes.
    /// This runs recursively to get all included values, but must be of the same TEnum type.
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

        return visited;
    }
}
