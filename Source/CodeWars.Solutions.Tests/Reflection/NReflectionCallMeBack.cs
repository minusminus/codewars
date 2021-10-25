using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;

namespace NReflection
{
    [TestFixture]
    public class NReflectionCallMeBack
    {
        Reflection.ReflectionCallMeBack _pobj = new Reflection.ReflectionCallMeBack();

        private static bool setting;
        public static void Activate()
        {
            setting = true;
        }

        [Test]
        public void Test()
        {
            _pobj.Activator();
            setting.ShouldBeTrue();
        }
    }
}
