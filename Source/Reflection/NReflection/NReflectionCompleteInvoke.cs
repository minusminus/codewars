using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using System.Linq;
using System.Reflection;

namespace NReflection
{
    public class testClassCI
    {
        public string returnvalue = "abcd";

        public string testmethod()
        {
            return returnvalue;
        }
    }

    public class testClassCIConstructor
    {
        public testClassCIConstructor(string text)
        {
            testvalue = text;
        }

        public string testvalue = "xyz";
        public string doit()
        {
            return testvalue;
        }
    }

    public class NReflectionCompleteInvoke
    {
        Reflection.ReflectionCompleteInvoke _pobj = new Reflection.ReflectionCompleteInvoke();
        testClassCI Helper = new testClassCI();

        [Test]
        public void NullTest()
        {
            _pobj.InvokeMethod(null).ShouldBeNull();
        }

        [Test]
        public void EmptyTest()
        {
            _pobj.InvokeMethod("").ShouldBe("");
        }

        [Test]
        public void UnknownTypeTest()
        {
            _pobj.InvokeMethod("unknownType").ShouldBeNull();
        }

        [Test]
        public void SmallObjectTest()
        {
            _pobj.InvokeMethod("testClassCI").ShouldBe(Helper.returnvalue);
        }

        [Test]
        public void AssemblyQualifiedNameTest()
        {
            Console.WriteLine(Helper.GetType().AssemblyQualifiedName);
            _pobj.InvokeMethod(Helper.GetType().AssemblyQualifiedName).ShouldBe(Helper.returnvalue);
        }

        [Test]
        public void NoDefaultConstrucotrTest()
        {
            testClassCIConstructor tc = new testClassCIConstructor(null);
            _pobj.InvokeMethod(tc.GetType().AssemblyQualifiedName).ShouldBe(tc.testvalue);
        }
    }
}
