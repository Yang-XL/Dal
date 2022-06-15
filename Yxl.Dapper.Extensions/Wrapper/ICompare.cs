using Yxl.Dapper.Extensions.Metadata;
using System;
using System.Collections;

namespace Yxl.Dapper.Extensions.Wrapper
{
    public interface ICompare<TColumn, out Children> : INested<Children> where Children : ICompare<TColumn, Children>
    {
        /// <summary>
        /// 等于
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        Children Eq(TColumn columnName, object val);
        /// <summary>
        /// 不等于
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        Children Ne(TColumn columnName, object val);
        /// <summary>
        /// between
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        Children Between(TColumn columnName, object from, object to);
        /// <summary>
        /// in
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Children In(TColumn columnName, IEnumerable data);
        /// <summary>
        /// like
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        Children Like(TColumn columnName, string val);
        /// <summary>
        ///  like 'xxx%'
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        Children StartsWith(TColumn columnName, string val);
        /// <summary>
        /// like '%xxx'
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        Children EndWith(TColumn columnName, string val);
        /// <summary>
        /// 大于
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        Children Gt(TColumn columnName, object val);
        /// <summary>
        /// 大于等于
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        Children Ge(TColumn columnName, object val);
        /// <summary>
        /// 小于
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        Children Lt(TColumn columnName, object val);
        /// <summary>
        /// 小于等于
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        Children Le(TColumn columnName, object val);
    }


}
