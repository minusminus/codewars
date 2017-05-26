using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC
{
    /// <summary>
    /// algorytm:
    /// tablica monet posortowana od min do max
    /// ciagi monet dzielacych kwote zbudowane od najmniejszej do najwiekszej
    /// 
    /// tablica dp [kolejne wartosci, kolejne monety rozpoczynajace ciagi]
    /// dp okresla dla danej kwoty ile jest mozliwych unikalnych kombinacji ciagow budujacych rozpoczynajacych sie od danej monety
    /// w zwiazku z czym dana wartosc w dp to suma fragmentu dp[wartosc - moneta, moneta] do dp[wartosc - moneta, koniec]
    /// 
    /// rozwiazaniem jest suma dp[kwota, *]
    /// 
    /// UWAGA: ograniczenie wyniku do int wynika z definicji zadania (klasy do uzupelnienia w codewars)
    /// </summary>
    public class CCC
    {
        //implementacja zaklada ze utworzona tablica dp jest wypelniona 0
        private int DPProcess(int money, int[] sortedcoins, int[,] dp)
        {
            //for (int m = 1; m < sortedcoins[0]; m++)
            //    for (int j = 0; j < sortedcoins.Length; j++)
            //        dp[m, j] = 0;

            for (int m = sortedcoins[0]; m <= money; m++)
            {
                //for (int i = 0; i < sortedcoins.Length; i++)
                for (int i = 0; (i < sortedcoins.Length) && (m>=sortedcoins[i]); i++)
                {
                    int row = m - sortedcoins[i];
                    if (row == 0)
                    {
                        dp[m, i] = 1;
                        //for (int j = i + 1; j < sortedcoins.Length; j++)
                        //    dp[m, j] = 0;
                    }
                    else //if (row > 0)
                    {
                        //int sum = 0;
                        //for (int j = i; j < sortedcoins.Length; j++)
                        //    sum += dp[row, j];
                        //dp[m, i] = sum;

                        //szybsze niz wersja powyzej (dazy do 548ms w SpeedTest w wersji release, wyzsze do 600ms)
                        int sum = dp[row, i];
                        for (int j = i + 1; j < sortedcoins.Length; j++)
                            sum += dp[row, j];
                        dp[m, i] = sum;

                        //wolniejsze bez dodatkowej zmiennej ok 30%
                        //dp[m, i] = dp[row, i];
                        //for (int j = i + 1; j < sortedcoins.Length; j++)
                        //    dp[m, i] += dp[row, j];
                    }
                }
            }

            int res = 0;
            for (int j = 0; j < sortedcoins.Length; j++)
                res += dp[money, j];
            return res;
        }

        public int CountCombinations(int money, int[] coins)
        {
            int[] sortedcoins = coins.Where(x => (x > 0) && (x <= money))
                .Distinct()
                .OrderBy(x => x)
                .ToArray();
            if (sortedcoins.Length == 0) return 0;
            int[,] dp = new int[money + 1, sortedcoins.Length];

            return DPProcess(money, sortedcoins, dp);
        }
    }
}
