using System;

namespace Yxl.Dal.Common.Enum
{
    [Flags]
    public enum FillStrategyEnum
    {
        /// <summary>
        /// 更新填充
        /// </summary>
        Update,
        /// <summary>
        /// 插入填充
        /// </summary>
        Insert,
        /// <summary>
        /// 全部填充
        /// </summary>
        All = Update | Insert
    }
}
