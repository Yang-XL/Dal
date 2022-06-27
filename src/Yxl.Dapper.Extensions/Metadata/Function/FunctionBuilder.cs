using System;
using System.Collections.Generic;
using System.Text;

namespace Yxl.Dapper.Extensions.Metadata.Function
{
    public class FunctionBuilder
    {
        public static IFiled Count()
        {


            return new Filed($"COUNT(*)") { FunctionColumn = true };
        }

        public static IFiled Top(int top)
        {


            return new Filed($"COUNT({top})", null) { FunctionColumn = true };
        }
    }
}
