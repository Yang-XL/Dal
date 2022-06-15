using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Yxl.Dapper.Extensions.Enum
{
    public static class EnumExtensions
    {
        public static string Description(this System.Enum value)
        {
            var memInfo = value.GetType().GetMember(value.ToString());

            if (memInfo?.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs?.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return value.ToString();
        }

        public static string ToSql(this GroupOperator value)
        {
            switch (value)
            {
                case GroupOperator.Or:
                    return "OR";
                case GroupOperator.And:
                    return "AND";
                default:
                    throw new NotSupportedException("not support operator");
            }
        }

        public static string ToSql(this SortDirection value)
        {
            switch (value)
            {
                case SortDirection.ASC:
                    return "ASC";
                case SortDirection.DESC:
                    return "DESC";
                default:
                    throw new NotSupportedException("not support operator");
            }
        }
        public static string ToSql(this SortDirection value,string columnNames)
        {
            switch (value)
            {
                case SortDirection.ASC:
                    return $"ORDER BY {columnNames} ASC ";
                case SortDirection.DESC:
                    return $"ORDER BY {columnNames} DESC ";
                default:
                    throw new NotSupportedException("not support operator");
            }
        }

        public static string GetString(this Operator op)
        {
            switch (op)
            {
                case Operator.Eq:
                    return "=";
                case Operator.Ne:
                    return "<>";
                case Operator.Gt:
                    return ">";
                case Operator.Ge:
                    return ">=";
                case Operator.Lt:
                    return "<";
                case Operator.Le:
                    return "<=";
                case Operator.Like:
                case Operator.StartsWith:
                case Operator.EndWith:
                    return "LIKE";
                case Operator.NotLike:
                case Operator.NotStartsWith:
                case Operator.NotEndWith:
                    return "NOT LIKE";
                case Operator.In:
                    return "IN";
                case Operator.NotIn:
                    return "NOT IN";
                case Operator.Null:
                    return "IS NULL";
                case Operator.NotNull:
                    return "IS NOT NULL";
                default:
                    throw new NotSupportedException("not support operator");
            }
        }


        public static string GetStringFormat(this Operator op)
        {
            var str = op.GetString();

            switch (op)
            {
                //case Operator.Like:
                //case Operator.NotLike:
                //    return str + " {0}";
                //case Operator.StartsWith:
                //case Operator.NotStartsWith:
                //    return str + " {0}%";
                //case Operator.EndWith:
                //case Operator.NotEndWith:
                //    return str + " %{0}";
                case Operator.Null:
                case Operator.NotNull:
                    return str;
                default:
                    return str + " {0}";
            }

        }
    }
}
