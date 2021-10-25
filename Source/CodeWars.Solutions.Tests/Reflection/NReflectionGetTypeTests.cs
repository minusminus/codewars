using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;

namespace NReflection
{
    [TestFixture]
    public class NReflectionGetTypeTests
    {
        Reflection.ReflectionGetType _pobj = new Reflection.ReflectionGetType();

        private void DoTestCheck(List<Tuple<object, Type>> testlist)
        {
            _pobj.GetTypes(testlist);
            foreach (var tuple in testlist)
                tuple.Item2.ShouldBe(tuple.Item1.GetType());
        }

        [Test]
        public void SimpleTest()
        {
            List<Tuple<object, Type>> testlist = new List<Tuple<object, Type>>() { new Tuple<object, Type>(_pobj, null) };
            DoTestCheck(testlist);
        }

        [Test]
        public void SelfTest()
        {
            NReflectionGetTypeTests tobj = new NReflectionGetTypeTests();
            List<Tuple<object, Type>> testlist = new List<Tuple<object, Type>>() {new Tuple<object, Type>(tobj, null)};
            DoTestCheck(testlist);

            List<int> tobj2 = new List<int>();
            testlist = new List<Tuple<object, Type>>() { new Tuple<object, Type>(tobj, null) };
            DoTestCheck(testlist);
        }

        [Test]
        public void NullTest()
        {
            List<Tuple<object, Type>> testlist = new List<Tuple<object, Type>>() { new Tuple<object, Type>(null, null) };
            _pobj.GetTypes(testlist);
            foreach (var tuple in testlist)
            {
                tuple.Item2.ShouldBeNull();
            }

        }
    }
}