using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public class ReflectionGetAllMethods
    {
        public string[] GetMethodNames(object TestObject)
        {
            if(TestObject==null)
                return new string[0];

            //https://msdn.microsoft.com/en-us/library/4d848zkb(v=vs.110).aspx
            return TestObject.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).Select(i => i.Name).ToArray();
        }
    }
}
