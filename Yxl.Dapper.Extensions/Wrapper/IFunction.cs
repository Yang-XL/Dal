using Yxl.Dapper.Extensions.Enum;

namespace Yxl.Dapper.Extensions.Wrapper
{
    public interface IFunction<TColumn, out Children> : IWrapper
    {
        /// <summary>
        /// 分组
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        Children GroupBy(params TColumn[] columns);
        /// <summary>
        /// 正序
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        Children OrderByAsc(params TColumn[] columns);
        /// <summary>
        /// 倒叙
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        Children OrderByDesc(params TColumn[] columns);
        /// <summary>
        /// 自定义
        /// </summary>
        /// <param name="sortDirection"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        Children OrderBy(SortDirection sortDirection, params TColumn[] columns);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        Children Top(int top);


    }

}
