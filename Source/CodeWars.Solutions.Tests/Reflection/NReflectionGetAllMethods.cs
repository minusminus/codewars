using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using System.Linq;
//using System.Reflection;

namespace NReflection
{
    public class Refl
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new Refl().Output());
            Console.WriteLine(new Refl().AddInts123(1, 2));
        }

        public string Output()
        {
            return "Test-Output";
        }

        public int AddInts123(int i1, int i2)
        {
            return i1 + i2;
        }
    }

    [TestFixture]
    public class NReflectionGetAllMethods
    {
        Reflection.ReflectionGetAllMethods _pobj = new Reflection.ReflectionGetAllMethods();

        [Test]
        public void NullTest()
        {
            Assert.AreEqual(0, _pobj.GetMethodNames(null).Length);
        }

        [Test]
        public void NewObjectTest()
        {
            var testObject = new object();
            var methodNameArray = _pobj.GetMethodNames(testObject);
            Assert.IsTrue(methodNameArray.Contains("ToString"));
        }


        [Test]
        public void TestReflClass()
        {
            var obj = new Refl();
            string[] res = _pobj.GetMethodNames(obj);
            res.ShouldBe(new string[] { "Main", "Output", "AddInts123", "ToString", "Equals", "GetHashCode", "GetType", "Finalize", "MemberwiseClone" });
        }

    }
}
