﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;


namespace AssemblyBrowserLibrary.ResultStructure
{
    // result structure for methods
    public class Method
    {
        public string name;
        public string signature;

        public Method(MethodInfo methodInfo)
        {
            signature = TypeInfoDecoder.GetTypeModifiers(methodInfo.GetType()) + methodInfo.ToString();
        }

    }
}
