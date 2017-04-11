using System;
using System.Collections.Generic;
using System.Linq;
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
            for (int i = 0; i < k; i++)
                _idx[i] = i;
            int maxpos = _sbase.Length - 1;
            int cpos = k - 1;

            while (cpos >= 0)
            {
                while ((cpos >= 0) && (_idx[cpos] == maxpos))
                {
                    //jezeli ostatni dotarl do swojej ostatniej pozycji to cofamy sie w lewo
                    cpos--;
                    maxpos--;
                }
                for (int il = cpos; il >= 0; il--) //przesuniecie wszystkich flag o 1 w prawo
                {
                    string s = GetMaskedString(k);
                    //tu przetworzenie wygenerowanego stringa
                    processString(s);
                    _idx[il]++;
                }
                for (int il = cpos - 1; il >= 0; il--) //cofniecie flag oprocz ostatniej
                    _idx[il]--;
            }
        }
    }
}
