using System.Runtime.Serialization;

namespace GetTheLoop
{
    public class GTL
    {
        //https://msdn.microsoft.com/pl-pl/library/system.runtime.serialization.objectidgenerator.getid(v=vs.110).aspx
        public int getLoopSize(LDNode startNode)
        {
            ObjectIDGenerator idgen = new ObjectIDGenerator();
            bool firstTime;

            LDNode n = startNode.next;
            int pos = 2;
            while (true)
            {
                long instanceID = idgen.GetId(n, out firstTime);
                if (!firstTime)
                {
                    return pos - (int) instanceID;// + 1;
                }
                //Console.WriteLine($"{instanceID}");
                n = n.next;
                pos++;
            }
        }
    }
}
