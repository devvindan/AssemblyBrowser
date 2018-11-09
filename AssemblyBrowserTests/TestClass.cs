using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssemblyBrowserLibrary.ResultStructure;
using AssemblyBrowserLibrary;
using System.Linq;
using System.Collections.Generic;


namespace AssemblyBrowserTests
{
    [TestClass]
    public class TestClass
    {

        private Result result;

        public List<Dictionary<int, bool>> uselessList;

        private Type testClassType;
        private string dllPath = "./AssemblyBrowserTests.dll";

        [TestInitialize]
        public void Initialize()
        {
            AssemblyReader asmInfoProcessor = new AssemblyReader();
            result = asmInfoProcessor.Read(dllPath);
            testClassType = typeof(TestClass);
        }


        [TestMethod]
        public void TestGenericListAndModifiers()
        {
            List<Field> classFields = result.Namespaces[0].DataTypes[0].fields;

            foreach (Field f in classFields)
            {
                if (f.name == "uselessList")
                {
                    Assert.AreEqual("private List`1<Dictionary`2<Int32 Boolean >>", f.type);
                }
            }

        }

        [TestMethod]
        public void TestOtherDatatypes()
        {

            List<Datatype> types = result.Namespaces[0].DataTypes;

            foreach (Datatype type in types)
            {
                Console.WriteLine(type.Name);
                Assert.IsTrue((type.Name == "public sealed delegate uselessDelegate") ||
                    (type.Name == "public sealed struct uselessStruct") ||
                    (type.Name == "public sealed enum uselessEnum") ||
                    (type.Name == "public abstract interface uselessInterface") || 
                    (type.Name == "public  class TestClass") || 
                    (type.Name == "private sealed class <>c"));

            }

            Assert.IsTrue(types.Count > 5);

        }

        // sanity check
        [TestMethod]
        public void TestNamespacesIsNotNull()
        {
            Assert.IsNotNull(result.Namespaces);
        }

        // sanity check
        [TestMethod]
        public void TestNamespacesCount()
        {
            Assert.IsTrue(result.Namespaces.Count > 0);
        }

        // Test namespace name (equals to current namespace)
        [TestMethod]
        public void TestNamespaceName()
        {
            Assert.AreEqual(result.Namespaces[0].Name, nameof(AssemblyBrowserTests));
        }

        // sanity check for datatype count
        [TestMethod]
        public void TestNamespacesTypesCount()
        {
            foreach (Namespace ns in result.Namespaces)
            {
                Assert.IsTrue(ns.DataTypes.Count > 0);
            }
        }

        // check our classes and result's number of fields
        [TestMethod]
        public void TestTypeFieldsCount()
        {
            int actualCount = result.Namespaces[0].DataTypes[0].fields.Count;
            int expectedCount = testClassType.GetFields().Length;
            Assert.AreEqual(actualCount, expectedCount);
        }

        // same check for properties
        [TestMethod]
        public void TestTypePropertiesCount()
        {
            int actualCount = result.Namespaces[0].DataTypes[0].properties.Count;
            int expectedCount = testClassType.GetProperties().Length;

            Assert.AreEqual(actualCount, expectedCount);
        }

        // same check for methods
        [TestMethod]
        public void TestTypeMethodsCount()
        {
            int actualCount = result.Namespaces[0].DataTypes[0].methods.Count;
            int expectedCount = testClassType.GetMethods().Where(item => !item.IsSpecialName).Count();
            Assert.AreEqual(actualCount, expectedCount);
        }
    }



        public delegate void uselessDelegate();


        public struct uselessStruct
        {

        }

        public enum uselessEnum
        {

        }

        public interface uselessInterface
        {

        }



}
