using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CountdownLongestWord
{
    public class CLWWordsGenNK
    {
        private readonly string _sbase;
        private readonly int[] _idx;

        public CLWWordsGenNK(string sbase)
        {
            _sbase = sbase;
            _idx = new int[_sbase.Length];
        }

        private string GetMaskedString(int k)
        {
            //return _idx.Select(i => _sbase[i]).ToString();
            string res = "";
            for (int i = 0; i < k; i++) res += _sbase[_idx[i]];
            return res;
        }

        public void GenerateValues(int k, Action<string> processString )
        {
            for (int i = 0; i < _idx.Length; i++) _idx[i] = 0;
            IntGenValues(k, k, _sbase.Length-1, processString);
            //for (int i = 0; i < k; i++)
            //    _idx[i] = i;
            //int maxpos = _sbase.Length - 1;
            //int cpos = k - 1;

            //string s = GetMaskedString(k);  //pierwszy geenrowany ciag
            //processString(s);
            //while (cpos >= 0)
            //{
            //    while ((cpos >= 0) && (_idx[cpos] == maxpos))
            //    {
            //        //jezeli ostatni dotarl do swojej ostatniej pozycji to cofamy sie w lewo
            //        cpos--;
            //        maxpos--;
            //    }
            //    for (int il = cpos; il >= 0; il--) //przesuniecie wszystkich flag o 1 w prawo
            //    {
            //        _idx[il]++;
            //        s = GetMaskedString(k);
            //        processString(s);   
            //    }
            //    for (int il = cpos - 1; il >= 0; il--) //cofniecie flag oprocz ostatniej, tylko te dla ktorych nastepna nie jest na koncu
            //        if (_idx[il + 1] < maxpos)
            //            _idx[il]--;
            //}
        }

        private void IntGenValues(int totalk, int k, int maxpos, Action<string> processString)
        {
            if (k == 1)
            {
                if (totalk == 1) maxpos++;
                for (int i = 0; i < maxpos; i++)
                {
                    _idx[0] = i;
                    string s = GetMaskedString(totalk);
                    processString(s);
                }
            }
            else
            {
                for (int i = 0; i < k; i++) _idx[i] = i;
                string s = GetMaskedString(totalk);
                processString(s);

                while (_idx[k-1] < maxpos)
                {
                    _idx[k-1]++;
                    IntGenValues(totalk, k - 1, _idx[k-1], processString);
                }
            }
        }
    }
}
