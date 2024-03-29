﻿using System;

namespace Yxl.Dapper.Extensions.Enum
{
    [Flags]
    public enum IgnoreEnum
    {
        /// <summary>
        /// 更新忽略
        /// </summary>
        Update,
        /// <summary>
        /// 插入忽略 如自增主键
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
