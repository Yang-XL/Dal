using Yxl.Dapper.Extensions.Enum;
using Yxl.Dapper.Extensions.Metadata;
using Yxl.Dapper.Extensions.SqlDialect;
using Yxl.Dapper.Extensions.Uitls;
using System.Collections.Generic;
using System.Linq;

namespace Yxl.Dapper.Extensions.Wrapper.Impl
{

    public abstract class BaseQueryWrapper<TColumn, Children> :
        Compare<TColumn, Children>, IQueryWrapper<TColumn, Children>
        where Children : class, IQueryWrapper<TColumn, Children> ,new ()
        
    {
        public BaseQueryWrapper()
        {
        }

        #region store
        protected IList<IFiled> GroupByFiles { get; set; } = new List<IFiled>();

        protected IList<ISortFiled> SortFileds { get; set; } = new List<ISortFiled>();

        protected IList<IFiled> Fileds { get; set; } = new List<IFiled>();

        protected int TopNumber { get; set; }

        protected ITable Table { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
        #endregion


        public virtual Children Select(params Filed[] fileds)
        {
            TryAddFiled(fileds);
            return this as Children;
        }
        protected virtual void AddFiled(IFiled filed)
        {
            Fileds.Add(filed);
        }

        protected virtual bool TryAddFiled(params Filed[] filed)
        {
            if (filed == null) return false;
            foreach (var item in filed)
            {
                Fileds.Add(item);
            }
            return true;
        }


        public Children GroupBy(params TColumn[] columns)
        {
            foreach (var item in columns)
            {
                GroupByFiles.Add(GetColumn(item));
            }
            return this as Children;
        }

        public Children OrderByAsc(params TColumn[] columns)
        {
            return OrderBy(SortDirection.ASC, columns);
        }

        public Children OrderByDesc(params TColumn[] columns)
        {
            return OrderBy(SortDirection.DESC, columns);
        }

        public Children OrderBy(SortDirection sortDirection, params TColumn[] columns)
        {
            foreach (var item in columns)
            {
                SortFileds.Add(new SortFiled(GetColumn(item), sortDirection));
            }
            return this as Children;
        }

        public Children Top(int top)
        {
            TopNumber = top;
            return this as Children;
        }

        public virtual IEnumerable<IFiled> AllFiled()
        {
            return new List<IFiled>() { new Filed("*") };
        }

        private IList<IFiled> SelectFileds()
        {
            return Fileds.Any() ? Fileds : AllFiled().ToList();
        }
        public override SqlInfo GetSql(ISqlDialect sqlDialect)
        {
            var result = new SqlInfo();
            result.Append("SELECT");
            #region Filed
            result.Append(Fileds.GetSqlSelect(sqlDialect).ToString());
            result.Append("FROM");
            result.Append(Table.GetTableName(sqlDialect));
            #endregion

            #region SqlWhere
            var sqlWhere = GetSqlWhere(sqlDialect);
            if (!string.IsNullOrWhiteSpace(sqlWhere.Sql))
            {
                result.Append("WHERE");
                result.Append(sqlWhere);
            }
            #endregion

            #region Group By
            if (GroupByFiles.Count > 0)
            {
                result.Append("GROUP BY");
                result.Append(GroupByFiles.GetSqlWhere(sqlDialect).ToString());
            }
            #endregion

            #region OrderBy
            var sortSql = SortFileds.GetSql(sqlDialect);
            if (sortSql?.Length > 0)
            {
                result.Append("ORDER BY");
            }
            result.Append(sortSql.ToString());
            #endregion

            if (TopNumber > 0)
            {
                result.Append(sqlDialect.TopSql(TopNumber));
            }
            if (PageIndex > 0 && PageSize > 0)
            {
                IDictionary<string, object> param = new Dictionary<string, object>();
                var pageSql = sqlDialect.GetPageSql(PageIndex, PageSize, ref param);
                result.Append(pageSql, param);
            }
            return result;
        }

        
    }

}
