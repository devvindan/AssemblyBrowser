using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AssemblyBrowserLibrary.ResultStructure;
using System.Reflection;

namespace AssemblyBrowserLibrary
{
    public class AssemblyReader
    {
        private Result _result;

        public AssemblyReader()
        {

        }

        // read assembly info to result datasctructure
        public Result Read(string path)
        {
            _result = new Result();

            Assembly asm = Assembly.LoadFrom(path);

            //working with namespaces
            foreach (var type in asm.DefinedTypes)
            {
                // prevent from adding namespaces already in the result list, add namespaces
                if (_result.Namespaces.Find(x => x.Name == type.Namespace) == null
                    && type.Namespace != null)
                {
                    _result.Namespaces.Add(new Namespace(type.Namespace));
                }
            }

            //working with dataTypes in each namespace
            foreach (var ns in _result.Namespaces)
            {
                // for each namespace add corresponding datatype info
                foreach (var type in asm.DefinedTypes.Where(x => x.Namespace == ns.Name))
                {
                    ns.DataTypes.Add(new Datatype(type));
                }
            }

            return _result;
        }
    }
}
