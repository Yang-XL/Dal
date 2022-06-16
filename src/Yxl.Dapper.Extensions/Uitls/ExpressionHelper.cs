using Yxl.Dapper.Extensions.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Yxl.Dapper.Extensions.Uitls
{

    public static class ExpressionHelper
    {
        public static object GetProperty(Expression lambda)
        {
            Expression expr = lambda;
            for (; ; )
            {
                switch (expr.NodeType)
                {
                    case ExpressionType.Lambda:
                        expr = ((LambdaExpression)expr).Body;
                        break;
                    case ExpressionType.Convert:
                        expr = ((UnaryExpression)expr).Operand;
                        break;
                    case ExpressionType.MemberAccess:
                        MemberExpression memberExpression = (MemberExpression)expr;
                        MemberInfo mi = memberExpression.Member;

                        if (memberExpression.Expression is MemberExpression)
                        {
                            expr = memberExpression.Expression;
                            break;
                        }
                        return mi.GetCustomAttribute<ColumnAttribute>()?.Name ?? mi.Name;
                    case ExpressionType.Call:
                        return ((MethodCallExpression)expr).Arguments;
                    default:
                        return null;
                }
            }
        }
        /// <summary>
        /// 表达式解析
        /// 目前支持  
        /// x=>x.Id   
        /// x=>new{ x.Id,x.Name}
        /// </summary>
        /// <param name="lambda"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static IList<MemberInfo> GetMemberInfo(Expression lambda)
        {
            Expression expr = lambda;
            List<MemberInfo> memberInfos = new List<MemberInfo>();
            bool stop = true;
            while (stop)
            {
                switch (expr.NodeType)
                {
                    case ExpressionType.Lambda:
                        expr = ((LambdaExpression)expr).Body;
                        break;
                    case ExpressionType.Convert:
                        expr = ((UnaryExpression)expr).Operand;
                        break;
                    case ExpressionType.MemberAccess:
                        MemberExpression memberExpression = (MemberExpression)expr;
                        MemberInfo mi = memberExpression.Member;
                        if (memberExpression.Expression is MemberExpression)
                        {
                            expr = memberExpression.Expression;
                            break;
                        }
                        memberInfos.Add(mi);
                        stop = false;
                        break;
                    case ExpressionType.New:
                        NewExpression newExpression = (NewExpression)expr;
                        foreach (var item in newExpression.Arguments)
                        {
                            memberInfos.AddRange(GetMemberInfo(item));
                        }
                        stop = false;
                        break;
                    default:
                        throw new NotSupportedException(expr.NodeType.ToString());
                }
            }
            return memberInfos;
        }

        private static object GetValue(Expression member, out string parameterName)
        {
            parameterName = null;
            if (member == null)
                return null;

            switch (member)
            {
                case MemberExpression memberExpression:
                    var instanceValue = GetValue(memberExpression.Expression, out parameterName);
                    try
                    {
                        switch (memberExpression.Member)
                        {
                            case FieldInfo fieldInfo:
                                parameterName = (parameterName != null ? parameterName + "_" : "") + fieldInfo.Name;
                                return fieldInfo.GetValue(instanceValue);

                            case PropertyInfo propertyInfo:
                                parameterName = (parameterName != null ? parameterName + "_" : "") + propertyInfo.Name;
                                return propertyInfo.GetValue(instanceValue);
                        }
                    }
                    catch
                    {
                        // Try again when we compile the delegate
                    }

                    break;

                case ConstantExpression constantExpression:
                    return constantExpression.Value;

                case MethodCallExpression methodCallExpression:
                    parameterName = methodCallExpression.Method.Name;
                    break;

                case UnaryExpression unaryExpression
                    when (unaryExpression.NodeType == ExpressionType.Convert
                        || unaryExpression.NodeType == ExpressionType.ConvertChecked)
                    && unaryExpression.Type.UnwrapNullableType() == unaryExpression.Operand.Type:
                    return GetValue(unaryExpression.Operand, out parameterName);
            }

            var objectMember = Expression.Convert(member, typeof(object));
            var getterLambda = Expression.Lambda<Func<object>>(objectMember);
            var getter = getterLambda.Compile();
            return getter();
        }

        public static Type UnwrapNullableType(this Type type) => Nullable.GetUnderlyingType(type) ?? type;


        public static Func<PropertyInfo, bool> GetPrimitivePropertiesPredicate()
        {
            return p => p.CanWrite && (p.PropertyType.IsValueType || p.GetCustomAttributes<ColumnAttribute>().Any() || p.PropertyType == typeof(string) || p.PropertyType == typeof(byte[]));
        }


        public static IFiled GetFiled(Expression expression, Type type)
        {
            var field = (MemberExpression)expression;
            var prop = type.GetProperty(field.Member.Name);
            if (prop == null || prop.GetCustomAttribute<NotMappedAttribute>() != null)
                return null;
            return prop.CreateFiled(type);

        }

        public static IList<IFiled> GetColumns<T>(Expression<Func<T, object>> expr)
        {
            var restul = new List<IFiled>();
            var type = typeof(T);
            if (expr.Body.NodeType == ExpressionType.Lambda)
            {
                if (expr.Body is UnaryExpression lambdaUnary)
                {
                    var expression = lambdaUnary.Operand as MemberExpression;
                    var filed = GetFiled(expression, type);
                    if (filed != null)
                        restul.Add(GetFiled(expression, type));

                }
            }
            else
            {
                var cols = (expr.Body as NewExpression)?.Arguments;
                if (cols != null)
                {
                    foreach (var expression in cols)
                    {
                        var prop = GetFiled(expression, type);
                        if (prop == null)
                            continue;
                        restul.Add(GetFiled(expression, type));
                    }
                }
            }

            return restul;
        }


    }
}
