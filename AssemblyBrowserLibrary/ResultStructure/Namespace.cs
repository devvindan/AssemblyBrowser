using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblyBrowserLibrary.ResultStructure
{
    // result structure for namespace
    public class Namespace
    {
        public string Name { set; get; }

        public List<Datatype> DataTypes { set; get; }

        public Namespace(string name)
        {
            Name = name;
            DataTypes = new List<Datatype>();
        }
    }
}
