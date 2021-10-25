using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reflection
{
    public class ReflectionAddMemberResults
    {
        public string ConcatStringMembers(object TestObject)
        {
            if (TestObject == null) return "";

            List<string> res = new List<string>();

            res.AddRange(
                TestObject.GetType()
                    .GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static |
                                BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(x => x.ReturnType == typeof (string))
                    .Where(x => x.GetParameters().Count() == 0)
                    .Select(x => (string) x.Invoke(TestObject, null))
                );

            res.AddRange(
                TestObject.GetType()
                    .GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static |
                               BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(x => x.FieldType == typeof (string))
                    .Select(x => (string) x.GetValue(TestObject))
                );

            res.Sort((x, y) =>
            {
                if (x == y) return 0;
                if (x.Length == y.Length) return x.CompareTo(y);
                return y.Length - x.Length;
            });
            return string.Join("", res);
        }
    }
}
