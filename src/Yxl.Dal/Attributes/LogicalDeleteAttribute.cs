using System;

namespace Yxl.Dal.Attributes
{
    /// <summary>
    /// 目前只支持逻辑删除字段类型为bool
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class LogicalDeleteAttribute : Attribute
    {


    }
}
