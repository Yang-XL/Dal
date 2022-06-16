namespace Yxl.Dapper.Extensions.SqlWhere
{
    public interface IBetweenWhere : ISqlWhere
    {
        /// <summary>
        /// 
        /// </summary>
        object From { get; set; }
        /// <summary>
        /// 
        /// </summary>
        object To { get; set; }
    }
}
