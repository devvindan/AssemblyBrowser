using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AssemblyBrowserLibrary.ResultStructure
{
    // common datatype result structure
    public class Datatype
    {
        public string Name { set; get; }
        public string DataTypeInfo { set; get; }

        // common datatype contains fields, methods and properties
        public List<Field> fields;
        public List<Property> properties;
        public List<Method> methods;

        // get datatype fields
        private void GetFields(Type typeInfo)
        {
            var fields = typeInfo.GetFields();

            foreach (var field in fields)
            {
                this.fields.Add(new Field(field));
            }
        }

        // get datatype properties
        private void GetProperties(Type typeInfo)
        {
            var properties = typeInfo.GetProperties();

            foreach (var property in properties)
            {
                this.properties.Add(new Property(property));
            }
        }

        // get datatype methods
        private void GetMethods(Type typeInfo)
        {
            var methods = typeInfo.GetMethods();

            foreach (var method in methods)
            {
                if (!method.IsSpecialName)
                {
                    this.methods.Add(new Method(method));
                }
            }
        }

        // get datatype info
        private void CollectTypeInfo()
        {
            string info = "\tFields:\n\t\t";

            foreach (Field f in fields)
                info += f.type + " " + f.name + "\n\t\t";

            info += "\n\tProperties\n\t\t";

            foreach (Property p in properties)
                info += p.type + " " + p.name + "\n\t\t";

            info += "\n\tMethods\n\t\t";

            foreach (Method m in methods)
                info += m.signature + m.name + "\n\t\t";

            DataTypeInfo = info;
        }

        public Datatype(TypeInfo typeInfo)
        {
            Name = TypeInfoDecoder.GetAtributes(typeInfo) + typeInfo.Name;

            fields = new List<Field>();
            properties = new List<Property>();
            methods = new List<Method>();

            GetFields(typeInfo);
            GetProperties(typeInfo);
            GetMethods(typeInfo);

            CollectTypeInfo();
        }


    }
}
