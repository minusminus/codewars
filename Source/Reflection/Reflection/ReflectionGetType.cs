using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public class ReflectionGetType
    {
        public void GetTypes(List<Tuple<object, Type>> objectTypes)
        {
            for(int i=0; i<objectTypes.Count; i++)
                if (objectTypes[i].Item1 != null)
                    objectTypes[i] = new Tuple<object, Type>(objectTypes[i].Item1, objectTypes[i].Item1.GetType());
        }
    }
}
