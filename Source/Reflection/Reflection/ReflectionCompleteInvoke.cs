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

            Type t = Type.GetType(typeName);    //fully qualified name, np "PMOMX,1d03e0e7, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null"
            if (t==null)
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())   //prosta nazwa typu, np "testClassCI"
                {
                    t = assembly.DefinedTypes.FirstOrDefault(x => x.Name == typeName);
                    if (t != null) break;
                }
            if (t == null) return null;

            //var cparams = t.GetConstructors().First().GetParameters();
            //object[] paramstbl = new object[cparams.Count()];
            //for (int i = 0; i < cparams.Count(); i++)
            //{
            //    var param = cparams[i];
            //    paramstbl[i] = Activator.CreateInstance(param.ParameterType);
            //}
            object[] paramstbl = new object[t.GetConstructors().First().GetParameters().Count()];
            var obj = Activator.CreateInstance(t, paramstbl);

            MethodInfo mi = t.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static |
                                         BindingFlags.Public | BindingFlags.NonPublic)
                .Where(x => x.ReturnType == typeof (string))
                .FirstOrDefault(x => x.GetParameters().Count() == 0);
            if (mi == null) return null;

            return (string)mi.Invoke(obj, null);
        }
    }
}
