using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Yxl.Dapper.Extensions.Core;
using Yxl.Dapper.Extensions.SqlDialect;

namespace Yxl.Dapper.Extensions.Uitls.ExpressionsTree
{

    public sealed class WhereExpression : ExpressionVisitor
    {
        #region sql指令


        public SqlInfo Sql { get; private set; } = new SqlInfo();


        private string _tempFieldName;

        private string TempFieldName
        {
            get => _prefix + _tempFieldName + ParameterCount;
            set => _tempFieldName = value;
        }

        private string ParamName => _parameterPrefix + TempFieldName;

        private readonly string _prefix;

        private readonly char _parameterPrefix;

        private readonly char _closeQuote;

        private readonly char _openQuote;

        private int ParameterCount { get; set; }
        #endregion

        #region 执行解析

        /// <inheritdoc />
        /// <summary>
        /// 执行解析
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="prefix">字段前缀</param>
        /// <param name="providerOption"></param>
        /// <returns></returns>
        public WhereExpression(LambdaExpression expression, string prefix, ISqlDialect sqlDialect)
        {
            _prefix = prefix;
            _parameterPrefix = sqlDialect.ParameterPrefix;
            _openQuote = sqlDialect.OpenQuote;
            _closeQuote = sqlDialect.CloseQuote;
            var exp = TrimExpression.Trim(expression);
            Visit(exp);
        }
        #endregion

        #region 访问成员表达式

        /// <inheritdoc />
        /// <summary>
        /// 访问成员表达式
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitMember(MemberExpression node)
        {
            //TODO:
            Sql.Append(_openQuote + node.Member.GetColumnAttributeName() + _closeQuote);
            TempFieldName = node.Member.Name;
            ParameterCount++;
            return node;
        }

        #endregion

        #region 访问二元表达式
        /// <inheritdoc />
        /// <summary>
        /// 访问二元表达式
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitBinary(BinaryExpression node)
        {
            Sql.Append("(");
            Visit(node.Left);

            Sql.Append(node.GetExpressionType());

            Visit(node.Right);
            Sql.Append(")");

            return node;
        }
        #endregion

        #region 访问常量表达式
        /// <inheritdoc />
        /// <summary>
        /// 访问常量表达式
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitConstant(ConstantExpression node)
        {
            SetParam(node.Value);

            return node;
        }
        #endregion

        #region 访问方法表达式
        /// <inheritdoc />
        /// <summary>
        /// 访问方法表达式
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == "Contains" && typeof(IEnumerable).IsAssignableFrom(node.Method.DeclaringType) &&
                node.Method.DeclaringType != typeof(string))
                In(node);
            else if (node.Method.Name == "Equals")
                Equal(node);
            else
                Like(node);

            return node;
        }

        #endregion

        private void SetParam(object value)
        {
            if (value != null)
            {
                if (!string.IsNullOrWhiteSpace(TempFieldName))
                {
                    Sql.Append(ParamName);
                    Sql.AddParameter(TempFieldName, value);
                }
            }
            else
            {
                Sql.Append("NULL");
            }
        }

        private void Like(MethodCallExpression node)
        {
            Visit(node.Object);
            Sql.Append(string.Format(" LIKE {0}", ParamName));
            switch (node.Method.Name)
            {
                case "StartsWith":
                    {
                        var argumentExpression = (ConstantExpression)node.Arguments[0];
                        Sql.AddParameter(TempFieldName, argumentExpression.Value + "%");
                    }
                    break;
                case "EndsWith":
                    {
                        var argumentExpression = (ConstantExpression)node.Arguments[0];
                        Sql.AddParameter(TempFieldName, "%" + argumentExpression.Value);
                    }
                    break;
                case "Contains":
                    {
                        var argumentExpression = (ConstantExpression)node.Arguments[0];
                        Sql.AddParameter(TempFieldName, "%" + argumentExpression.Value + "%");
                    }
                    break;
                default:
                    throw new NotSupportedException("the expression is no support this function");
            }
        }

        private void Equal(MethodCallExpression node)
        {
            Visit(node.Object);
            Sql.Append(string.Format(" = {0}", ParamName));
            var argumentExpression = node.Arguments[0].ToConvertAndGetValue();
            Sql.AddParameter(TempFieldName, argumentExpression);
        }

        private void In(MethodCallExpression node)
        {
            var arrayValue = (IList)((ConstantExpression)node.Object).Value;
            if (arrayValue.Count == 0)
            {
                Sql.Append(" 1 = 2");
                return;
            }
            Visit(node.Arguments[0]);
            Sql.Append(string.Format(" IN {0}", ParamName));
            Sql.AddParameter(TempFieldName, arrayValue);
        }
    }
}
