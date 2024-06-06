using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imagile.Domain.Attributes.Declarative;
[AttributeUsage(AttributeTargets.Field)]
public class IncludesAttribute<TEnum>(params TEnum[] includes) : Attribute
{
    public TEnum[] Includes { get; protected set; } = includes;
}
