using System;
using System.Collections.Generic;
using System.Text;

namespace Yxl.Dal.Common.Enum
{
    [Flags]
    public enum IgnoreEnum
    {
        /// <summary>
        /// 更新忽略
        /// </summary>
        Update,
        /// <summary>
        /// 插入忽略
        /// </summary>
        Insert,
        /// <summary>
        /// 查找忽略
        /// </summary>
        Select,
        /// <summary>
        /// 全部忽略
        /// </summary>
        All = Update | Insert | Select
    }
}
