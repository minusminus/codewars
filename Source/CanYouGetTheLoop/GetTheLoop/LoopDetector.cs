using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GetTheLoop
{
    public class LDNode
    {
        //public string ID;
        public LDNode next;

        public LDNode()
        {
            //ID = Guid.NewGuid().ToString();
        }
    }

    public class LoopDetector
    {
        private static LDNode createNodesChain(int length, out LDNode last)
        {
            LDNode start = new LDNode();
            last = start;
            for (int i = 1; i < length; i++)
            {
                last.next = new LDNode();
                last = last.next;
            }
            return start;
        }

        public static LDNode createChain(int tailsize, int loopsize)
        {
            LDNode start, last;
            start = createNodesChain(tailsize, out last);

            LDNode loopstart, looplast;
            loopstart = createNodesChain(loopsize-1, out looplast);

            last.next = loopstart;
            looplast.next = loopstart;

            return start;
        }
    }
}
