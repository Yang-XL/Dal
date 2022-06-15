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
            var parameterName = $"{sqlDialect.ParameterPrefix}U_{Filed.Name}";
            var sql = $"{Filed.Name}={parameterName}";
            var result = new SqlInfo(sql);
            result.AddParameter(new Parameter(parameterName, Value));
            return result;
        }

    }
}
