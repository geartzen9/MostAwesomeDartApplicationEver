using DartApplicationLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass()]
    public class UnitTest1
    {
        [TestMethod()]
        public void TestFunctionUnitTest()
        {
            //Arrange
            TestClass testClass = new TestClass();

            //Act
            int result = testClass.TestFunction(1);

            //Assert
            Assert.AreEqual(2, result);
        }
    }
}