using System;
using System.Collections.Generic;
using Yxl.Dal.Context;

namespace Yxl.Dal.Options
{
    /// <summary>
    /// 读写分离 目前支持 主从配置
    /// </summary>
    public class ReadWriteConnectionOptions
    {

        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 写库
        /// </summary>

        public DbOptions WriteDbOptions { get; set; }
        /// <summary>
        /// 
        /// 读库
        /// </summary>
        public IEnumerable<ReadDbOptions> ReadDbOptions { get; set; }

        /// <summary>
        /// 读写分离选择器
        /// </summary>
        internal  IConnectionWrapper Wrapper { get; private set; }


        public ReadWriteConnectionOptions EnableReadWriteSeparation(Func<IConnectionWrapper> func)
        {
            Wrapper = func();
            return this;
        }

        public ReadWriteConnectionOptions EnableReadWriteSeparation()
        {
            return EnableReadWriteSeparation(() => new ConnectionWrapper(this));
        }
    }
}
