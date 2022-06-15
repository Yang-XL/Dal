using System.ComponentModel;

namespace Yxl.Dapper.Extensions.Enum
{
    /// <summary>
    ///  Possible sorting Direction
    /// </summary>
    public enum SortDirection
    {
        /// <summary>
        /// Ascending
        /// </summary>
        [Description("ASC")]
        ASC,

        /// <summary>
        /// Descending
        /// </summary>
        [Description("DESC")]
        DESC
    }
}
