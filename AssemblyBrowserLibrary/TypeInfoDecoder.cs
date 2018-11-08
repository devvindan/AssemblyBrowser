using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLibrary
{
    public static class TypeInfoDecoder
    {

        // get string version of type modifiers (public, private, etc..)
        public static string GetTypeModifiers(Type typeInfo)
        {
            if (typeInfo.IsNestedPrivate) return "private ";
            if (typeInfo.IsNestedFamily) return "protected ";
            if (typeInfo.IsNestedAssembly) return "internal ";
            if (typeInfo.IsNestedFamORAssem) return "protected internal ";
            if (typeInfo.IsNestedFamANDAssem) return "protected private ";
            if (typeInfo.IsNestedPublic || typeInfo.IsPublic) return "public ";

            if (typeInfo.IsNotPublic)
                return "private ";
            else
                return "public ";
        }

        // string version of attributes (static, sealed, etc..)
        private static string GetTypeAtributes(Type typeInfo)
        {
            if (typeInfo.IsAbstract && typeInfo.IsSealed) return "static ";
            if (typeInfo.IsSealed) return "sealed ";
            if (typeInfo.IsAbstract) return "abstract ";

            return " ";

        }

        // string version of the class (inteface, struct, etc..)
        private static string GetTypeClass(Type typeInfo)
        {
            if (typeInfo.IsInterface) return "interface ";
            if (typeInfo.IsValueType) return "struct ";
            if (typeInfo.IsEnum) return "enum ";
            if (typeInfo.BaseType == typeof(MulticastDelegate)) return "delegate ";
            if (typeInfo.IsClass) return "class ";

            return "";

        }

        // get all information 
        public static string GetAtributes(Type typeInfo)
        {
            return GetTypeModifiers(typeInfo) + GetTypeAtributes(typeInfo) + GetTypeClass(typeInfo);
        }

    }
}
