using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssemblyBrowserLibrary.ResultStructure;
using AssemblyBrowserLibrary;
using System.Linq;


namespace AssemblyBrowserTests
{
    [TestClass]
    public class TestClass
    {
        private Result result;
        private Type testClassType;
        private string dllPath = "./AssemblyBrowserTests.dll";

        [TestInitialize]
        public void Initialize()
        {
            AssemblyReader asmInfoProcessor = new AssemblyReader();
            result = asmInfoProcessor.Read(dllPath);
            testClassType = typeof(TestClass);
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
}
