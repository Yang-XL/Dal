using System.ComponentModel;

namespace Yxl.Dapper.Extensions.Enum
{
    public enum Operator
    {
        /// <summary>
        /// Equal to
        /// </summary>
        Eq,
        /// <summary>
        /// 
        /// </summary>
        Ne,

        /// <summary>
        /// Greater than
        /// </summary>
        Gt,

        /// <summary>
        /// Greater than or equal to
        /// </summary>
        Ge,

        /// <summary>
        /// Less than
        /// </summary>
        Lt,

        /// <summary>
        /// Less than or equal to
        /// </summary>
        Le,

        /// <summary>
        /// Like (You can use % in the value to do wilcard searching)
        /// </summary>
        Like,
        /// <summary>
        /// 
        /// </summary>
        NotLike,
        /// <summary>
        /// 
        /// </summary>
        StartsWith,
        /// <summary>
        /// 
        /// </summary>
        NotStartsWith,
        /// <summary>
        /// 
        /// </summary>
        EndWith,
        /// <summary>
        /// 
        /// </summary>
        NotEndWith,

        /// <summary>
        /// Contains a value of an data object.
        /// </summary>
        In,
        /// <summary>
        /// 
        /// </summary>
        NotIn,
        /// <summary>
        /// 
        /// </summary>
        Null,
        /// <summary>
        /// 
        /// </summary>
        NotNull,
        /// <summary>
        /// 
        /// </summary>
        Exists,
        /// <summary>
        /// 
        /// </summary>
        NotExists,
        /// <summary>
        /// 
        /// </summary>
        Having,
        /// <summary>
        /// 
        /// </summary>
        GroupBy,

    }

    /// <summary>
    /// Operator to use when joining predicates in a PredicateGroup.
    /// </summary>
    public enum GroupOperator
    {
        And,
        Or
    }
}
