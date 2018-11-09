using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;


namespace AssemblyBrowserLibrary.ResultStructure
{
    // Result structure for fields
    public class Field
    {
        public string name;
        public string type;

        // recursive generic type builder
        private string GetGenericType(Type[] t)
        {
            string result = "";
            foreach (var genericType in t)
            {
                if (genericType.IsGenericType)
                    result += genericType.Name + "<" + GetGenericType(genericType.GenericTypeArguments) + ">";
                else
                    result += genericType.Name + " ";
            }

            return result;
        }

        public Field(FieldInfo fieldInfo)
        {
            name = fieldInfo.Name;
            type = TypeInfoDecoder.GetTypeModifiers(fieldInfo.GetType());

            // in case the type is Generic<T>
            if (fieldInfo.FieldType.IsGenericType)
                type += fieldInfo.FieldType.Name + "<" +
                    GetGenericType(fieldInfo.FieldType.GenericTypeArguments) + ">";
            else
                type += fieldInfo.FieldType.Name;
        }

    }
}
