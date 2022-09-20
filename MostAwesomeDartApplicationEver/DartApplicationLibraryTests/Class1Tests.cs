using Microsoft.VisualStudio.TestTools.UnitTesting;
using DartApplicationLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartApplicationLibrary
{
    [TestClass()]
    public class Class1Tests
    {
        [TestMethod()]
        public void TestFunctionUnitTest()
        {
            DartApplicationLibrary.Class1 classTest = new DartApplicationLibrary.Class1();

            int result = classTest.TestFunction(1);

            Assert.AreEqual(2, result);
        }
    }
}