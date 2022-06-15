using System.Collections.Generic;

namespace Yxl.Dapper.Extensions
{
    /// <summary>
    /// Abstract Query Expression
    /// </summary>
    internal abstract class QueryExpression
    {
        /// <summary>
        /// Query Expression Node Type
        /// </summary>
        public QueryExpressionType NodeType { get; set; }

        /// <summary>
        /// Operator OR/AND
        /// </summary>
        public string LinkingOperator { get; set; }

        public override string ToString()
        {
            return $"[NodeType:{this.NodeType}, LinkingOperator:{LinkingOperator}]";
        }
    }

    /// <summary>
    /// Query Expression Node Type
    /// </summary>
    internal enum QueryExpressionType
    {
        Parameter = 0,
        Binary = 1,
    }

    /// <inheritdoc />
    /// <summary>
    /// Class that models the data structure in coverting the expression tree into SQL and Params.
    /// `Parameter` Query Expression
    /// </summary>
    internal class QueryParameterExpression : QueryExpression
    {
        public QueryParameterExpression()
        {
            NodeType = QueryExpressionType.Parameter;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="QueryParameterExpression " /> class.
        /// </summary>
        /// <param name="linkingOperator">The linking operator.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="queryOperator">The query operator.</param>
        /// <param name="nestedProperty">Signilize if it is nested property.</param>
        internal QueryParameterExpression(string linkingOperator,
            string propertyName, object propertyValue,
            string queryOperator, bool nestedProperty) : this()
        {
            LinkingOperator = linkingOperator;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            QueryOperator = queryOperator;
            NestedProperty = nestedProperty;
        }

        public string PropertyName { get; set; }
        public object PropertyValue { get; set; }
        public string QueryOperator { get; set; }
        public bool NestedProperty { get; set; }

        public override string ToString()
        {
            return
                $"[{base.ToString()}, PropertyName:{PropertyName}, PropertyValue:{PropertyValue}, QueryOperator:{QueryOperator}, NestedProperty:{NestedProperty}]";
        }
    }

    /// <inheritdoc />
    /// <summary>
    /// `Binary` Query Expression
    /// </summary>
    internal class QueryBinaryExpression : QueryExpression
    {
        public QueryBinaryExpression()
        {
            NodeType = QueryExpressionType.Binary;
        }

        public List<QueryExpression> Nodes { get; set; }

        public override string ToString()
        {
            return $"[{base.ToString()} ({string.Join(",", Nodes)})]";
        }
    }
}
