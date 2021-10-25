using System;
using System.Linq;
using System.Diagnostics;
using System.Reflection;

namespace Reflection
{
    public class ReflectionCallMeBack
    {
        public void Activator()
        {
            StackTrace st = new StackTrace();
            Type t = st.GetFrame(1).GetMethod().DeclaringType;

            MethodInfo mi = t.GetMethods(BindingFlags.Instance | BindingFlags.Static |
                                         BindingFlags.Public | BindingFlags.NonPublic)
                .FirstOrDefault(x => x.GetParameters().Count() == 0);
            mi.Invoke(null, null);
        }
    }
}
