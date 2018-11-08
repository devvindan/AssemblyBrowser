using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLibrary.ResultStructure
{
    // structure for result representation
    public class Result
    {
        public List<Namespace> Namespaces { set; get; }

        public Result()
        {
            Namespaces = new List<Namespace>();
        }
    }
}
