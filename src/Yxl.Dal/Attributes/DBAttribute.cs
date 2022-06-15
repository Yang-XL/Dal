using System;

namespace Yxl.Dal.Attributes
{
    public class DBAttribute : Attribute
    {
        public string Name { get; set; }

        public DBAttribute(string name)
        {
            Name = name;
        }
    }
}
