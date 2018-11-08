using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AssemblyBrowserLibrary.ResultStructure
{
    public class Property
    {
        public string name;
        public string type;

        public Property(PropertyInfo propertyInfo)
        {
            name = propertyInfo.Name;
            type = propertyInfo.PropertyType.Name;
        }
    }
}
