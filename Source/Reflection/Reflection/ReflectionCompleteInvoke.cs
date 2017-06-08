using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public class ReflectionCompleteInvoke
    {
        public string InvokeMethod(string typeName)
        {
            if (String.IsNullOrEmpty(typeName)) return typeName;

            //PMOMX,1d03e0e7, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

            Type t = null;
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                t = assembly.DefinedTypes.FirstOrDefault(x => x.Name == typeName);
                if (t != null) break;
            }
            //if (t == null) Console.WriteLine("type null");
            if (t == null) return null;

            MethodInfo mi = t.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static |
                                         BindingFlags.Public | BindingFlags.NonPublic)
                .Where(x => x.ReturnType == typeof (string))
                .FirstOrDefault(x => x.GetParameters().Count() == 0);
            if (mi == null) return null;

            var obj = Activator.CreateInstance(t);

            return (string)mi.Invoke(obj, null);
        }
    }
}
