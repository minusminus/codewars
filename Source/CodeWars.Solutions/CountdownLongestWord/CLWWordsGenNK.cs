using System;

namespace CountdownLongestWord
{
    public class CLWWordsGenNK
    {
        //private readonly string _sbase;
        private int[] _idx;
        private char[] _strchars;
        private char[] _sbasearr;

        private void Init(char[] sarr)
        {
            _sbasearr = sarr;
            _idx = new int[_sbasearr.Length + 1];
        }

        public CLWWordsGenNK(string sbase)
        {
            Init(sbase.ToCharArray());
        }

        public CLWWordsGenNK(char[] sarr)
        {
            Init(sarr);
        }

        private string GetMaskedString(int k)
        {
            for (int i = 0; i < k; i++) _strchars[i] = _sbasearr[_idx[i]];
            return new string(_strchars);
        }

        public void GenerateValues(int k, Action<string> processString )
        {
            _strchars = new char[k];
            _idx[k] = _sbasearr.Length;
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
