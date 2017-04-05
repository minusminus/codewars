using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountdownLongestWord
{
    public class CLWWordsGenNK
    {
        private string _sbase;
        private bool[] _flags;
        private int[] _idx;
        private int _k;

        public void Init(string sbase, int k)
        {
            _sbase = sbase;
            _k = k;
            _flags = new bool[_sbase.Length];
            for (int i = 0; i < _flags.Length; i++)
                _flags[i] = (i < _k);
            _idx = new int[_k];
            for (int i = 0; i < _k; i++)
                _idx[i] = i;
        }

        private string GetMaskedString()
        {
            return "";
        }

        public void GenerateValues()
        {
            int cpos = _k - 1;

            for (int p = _idx[cpos]; p < _sbase.Length; p++)
            {
                string s = GetMaskedString();
                //przetworzenie wygenerowanego stringa
            }

        }

        //public string NextVal()
        //{
        //    string res = "";
        //    return res;
        //}
    }
}
