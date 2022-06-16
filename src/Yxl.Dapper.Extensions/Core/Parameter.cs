using System.Data;

namespace Yxl.Dapper.Extensions.Core
{
    /// <summary>
    /// 参考 DapperExtensions
    /// https://github.com/tmsmith/Dapper-Extensions/blob/master/DapperExtensions/Mapper/Parameter.cs
    /// https://github.com/tmsmith/Dapper-Extensions/blob/master/DapperExtensions/DapperImplementor.cs #GetDynamicParameters Method
    /// </summary>
    public class Parameter
    {
        public Parameter()
        {

        }
        public Parameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
        public DbType? DbType { get; set; }
        public ParameterDirection? ParameterDirection { get; set; }
        public int? Size { get; set; }
        public byte? Precision { get; set; }
        public byte? Scale { get; set; }
    }
}
