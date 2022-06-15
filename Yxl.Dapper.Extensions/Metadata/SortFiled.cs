using Yxl.Dapper.Extensions.Enum;
using Yxl.Dapper.Extensions.SqlDialect;

namespace Yxl.Dapper.Extensions.Metadata
{
    public interface ISortFiled
    {
        IFiled Filed { get; }

        SortDirection Sort { get; }

        string GetSql(ISqlDialect sqlDialect);
    }

    public class SortFiled : ISortFiled
    {
        public SortFiled(IFiled file, SortDirection sort)
        {
            Filed = file;
            Sort = sort;
        }

        public SortDirection Sort { get; set; }

        public string SortString
        {
            get
            {
                switch (Sort)
                {
                    case SortDirection.ASC:
                        return "ASC";

                    case SortDirection.DESC:
                        return "DESC";
                    default:
                        return "ASC";
                }
            }
        }

        public IFiled Filed { get; }

        public string GetSql(ISqlDialect sqlDialect)
        {
            return $"{Filed.GetSelectColumnSql(sqlDialect)} {SortString}";
        }
    }
}
