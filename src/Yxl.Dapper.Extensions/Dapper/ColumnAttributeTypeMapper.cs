using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Yxl.Dapper.Extensions.Metadata;
using Yxl.Dapper.Extensions.Uitls;
using static Dapper.SqlMapper;

namespace Yxl.Dapper.Extensions.Dapper
{
    internal class ColumnAttributeTypeMapper : DefaultTypeMap
    {
        private readonly IEnumerable<IFiled> Fileds;
        public ColumnAttributeTypeMapper(Type t) : base(t)
        {
            Fileds = t.CreateFiles();
        }

        public override IMemberMap GetMember(string columnName)
        {
            var file = Fileds.FirstOrDefault(a => columnName.Equals(a.Name));
            if (file == null)
            {
                return base.GetMember(columnName);
            }
            return new SimpleMemberMap(columnName, file.MetaData);
        }
    }

    public static class DapperExtenstion
    {
        public static void UseDapper()
        {
            SetTypeMap((type) => new ColumnAttributeTypeMapper(type));
        }
    }
}
