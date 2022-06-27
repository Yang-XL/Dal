using Yxl.Dapper.Extensions.Core;
using Yxl.Dapper.Extensions.SqlDialect;

namespace Yxl.Dapper.Extensions.Metadata
{

    public interface IUpdateFiled
    {
        IFiled Filed { get; }

        object Value { get; }

        SqlInfo GetSql(ISqlDialect sqlDialect);
    }

    public class UpdateFiled : IUpdateFiled
    {
        public UpdateFiled(IFiled filed, object value)
        {
            Filed = filed;
            Value = value;
        }

        public object Value { get; set; }

        public IFiled Filed { get; }

        public SqlInfo GetSql(ISqlDialect sqlDialect)
        {
            var sqlInfo = new SqlInfo();
            var parameterName = $"{sqlDialect.ParameterPrefix}{Filed.Name}_U";
            var sql = $"{Filed.Name}={sqlInfo.AddParameter(parameterName, Value)}";
            sqlInfo.Append(sql);
            return sqlInfo;
        }

    }
}
