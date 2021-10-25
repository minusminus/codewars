using System;
using System.Text;

namespace BE
{
    public class BinomialData
    {
        public Int64 a, b, n;
        public char x;
    }

    /// <summary>
    /// Algorytm:
    /// 1. parsowany jest ciag wejsciowy w stalym formacie
    /// 2. dla kazdego wspolczynnika wielomianu (liczonego z trojkata pitagorasa) generowany jest kolejny element (potega) wyniku
    /// </summary>
    public class BE
    {
        public BinomialData ParseBinomial(string btxt)
        {
            BinomialData res = new BinomialData();

            //(ax+b)^n
            string[] arr = btxt.Replace("(", string.Empty).Replace(")", string.Empty).Split('^');
            res.n = Int64.Parse(arr[1]);

            //ax+b
            res.b = 1;
            string[] arr2 = arr[0].Split('+');
            if (arr2.Length == 1)
            {
                res.b = -1;
                arr2 = arr[0].Split('-');
            }
            res.b *= Int64.Parse(arr2[arr2.Length - 1]);

            //ax
            string s = arr[0].Remove(arr[0].Length - arr2[arr2.Length - 1].Length - 1);
            res.x = s[s.Length - 1];
            s = s.Remove(s.Length - 1);
            if (!Int64.TryParse(s, out res.a))
                res.a = (s == "-") ? -1 : 1;

            return res;
        }

        public Int64[] GenerateCoefs(Int64 n)
        {
            Int64[] res = new Int64[n + 1];

            res[0] = 1;
            for (Int64 x = 0; x < n; x++)
                res[x + 1] = res[x] * (n - x) / (x + 1);

            return res;
        }

        public string GetSingleElement(BinomialData data, Int64 coef, int index)
        {
            if (index == data.n)
            {
                Int64 lastc = (Int64)Math.Pow(data.b, data.n);
                if (lastc == 0) return "";
                return (lastc > 0 ? "+" : "") + lastc.ToString();
            }

            Int64 c = coef * (Int64)(Math.Pow(data.a, data.n - index) * Math.Pow(data.b, index));
            if (c == 0) return "";

            string spow = "";
            if (data.n - index > 0)
                spow = data.x.ToString() + ((data.n - index > 1) ? "^" + (data.n - index).ToString() : "");

            if (c == 1) return spow;
            if (c == -1) return "-" + spow;
            return (((c > 0) && (index > 0)) ? "+" : "") + c.ToString() + spow;
        }

        public string GetSolution(BinomialData data, Func<BinomialData, Int64, int, string> singleElementGenerator)
        {
            StringBuilder res = new StringBuilder();

            Int64 currcoef = 1;
            res.Append(singleElementGenerator(data, currcoef, 0));
            for (int x = 0; x < data.n; x++)
            {
                currcoef = currcoef * (data.n - x) / (x + 1);
                res.Append(singleElementGenerator(data, currcoef, x + 1));
            }

            return res.ToString().Trim();
        }

        public string Expand(string expr)
        {
            BinomialData data = ParseBinomial(expr);
            if (data.n == 0) return "1";

            //Int64[] coefs = GenerateCoefs(data.n);
            //StringBuilder res = new StringBuilder();
            //for (int i = 0; i < coefs.Length; i++)
            //    res.Append(GetSingleElement(data, coefs[i], i));
            //return res.ToString().Trim();

            return GetSolution(data, GetSingleElement);
        }
    }
}
