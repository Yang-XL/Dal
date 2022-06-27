using System;
using System.Collections.Generic;
using System.Text;
using Yxl.Dal.Aggregate;
using Yxl.Dal.Attributes;

namespace Mock.Entitys.DapperTest
{
    [DB("dapper_test")]
    public class UserDbEntity : IEntity
    {
    }
}
