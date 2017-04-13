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
            _idx = new int[_sbase.Length + 1];
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
            //for (int i = 0; i < k; i++) _idx[i] = 0;
            _idx[k] = _sbase.Length;
            IntGenValues(k, k, processString);
        }

        /// <summary>
        /// Generuje kolejne maski kombinacji (n po k)
        /// Tablica indeksow jest dlugosci k+1, w ostatnim elemencie jest straznik o wartosci dlugosci stringa
        /// Dla kazdego przesuniecia indeksu o jeden generowane sa wszystkie kombinacje na mniejszych indeksach
        /// Przyklad (5 po 3):
        /// 11100
        /// 11010
        /// 10110
        /// 01110
        /// 11001
        /// 10101
        /// 01101
        /// 10011
        /// 01011
        /// 00111
        /// </summary>
        /// <param name="totalk"></param>
        /// <param name="k"></param>
        /// <param name="processString"></param>
        private void IntGenValues(int totalk, int k, Action<string> processString)
        {
            if (k == 1)
            {
                for (int i = 0; i < _idx[k]; i++)
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

                while (_idx[k-1] < _idx[k]-1)
                {
                    _idx[k-1]++;
                    IntGenValues(totalk, k - 1, processString);
                }
            }
        }
    }
}
