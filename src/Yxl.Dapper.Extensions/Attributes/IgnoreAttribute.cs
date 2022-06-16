using Yxl.Dapper.Extensions.Enum;
using System;

namespace Yxl.Dapper.Extensions.Attributes
{
    /// <inheritdoc />
    /// <summary>
    ///     Ignore property attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class IgnoreAttribute : Attribute
    {
        public IgnoreEnum Ignore;

        public IgnoreAttribute(IgnoreEnum ignoreEnum = IgnoreEnum.All)
        {
            Ignore = ignoreEnum;
        }
    }
}
