using System;

namespace Yxl.Dal.Options
{
    public class DbOptions
    {
        /// <summary>
        /// 链接ID
        /// </summary>
        public string ConnectionId { get; } = Guid.NewGuid().ToString("N");

        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;
    }

    public class ReadDbOptions : DbOptions
    {
        /// <summary>
        /// 权重
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// 当前权重
        /// </summary>
        internal int CurrentWeight { get; set; }
        /// <summary>
        /// 有效权重        /// 
        /// </summary>
        internal int EffectiveWeight { get; set; }
    }
}
