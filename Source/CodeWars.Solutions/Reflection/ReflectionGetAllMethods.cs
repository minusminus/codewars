using System.Linq;
using System.Reflection;

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
