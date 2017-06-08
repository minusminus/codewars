using System;
using NUnit.Framework;
using Shouldly;

namespace NReflection
{
    public class testClass
    {
        public string Output1()
        {
            return "Output";
        }

        public string Output2()
        {
            return "It";
        }
    }

    public class testClassRefl
    {
        public string TonysLastname()
        {
            return "Stark";
        }
        public string Output()
        {
            return "Test-Output";
        }

        public int AddInts(int i1, int i2)
        {
            return i1 + i2;
        }
    }

    public class MyTest
    {
        public string s1 = "abcd";

        public int add(int i1, int i2)
        {
            return i1 + i2;
        }

        public string func1()
        {
            return "xyz";
        }

        public string func2(string s)
        {
            return s;
        }

        public string prop1 => "eee";

        public int prop2 => 1234;
    }

    [TestFixture]
    public class NReflectionAddMemberResults
    {
        Reflection.ReflectionAddMemberResults _pobj = new Reflection.ReflectionAddMemberResults();

        [Test]
        public void BasicKataNullTest()
        {
            _pobj.ConcatStringMembers(null).ShouldBe("");
        }

        [Test]
        public void BasicKataObjectTest()
        {
            var testObject = new testClass();
            _pobj.ConcatStringMembers(testObject).ShouldBe("OutputIt");
        }

        [Test]
        public void BasicKataExmplTest()
        {
            var testObject = new testClassRefl();
            _pobj.ConcatStringMembers(testObject).ShouldBe("Test-OutputStark");
        }

        [Test]
        public void MyTestTest()
        {
            var obj = new MyTest();
            _pobj.ConcatStringMembers(obj).ShouldBe("abcdeeexyz");
        }
    }
}
