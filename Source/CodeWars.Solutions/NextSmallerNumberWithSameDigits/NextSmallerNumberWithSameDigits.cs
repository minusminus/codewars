using System.Collections.Generic;

namespace NextSmallerNumberWithSameDigits
{
    public class NextSmallerNumberWithSameDigits
    {
        private List<int> NumToList(long n)
        {
            List<int> res = new List<int>();
            while (n > 0)
            {
                res.Add((int)(n % 10));
                n /= 10;
            }
            return res;
        }

        private long ListToNum(List<int> nums)
        {
            long res = 0;
            long m10 = 1;
            for (int i = 0; i < nums.Count; i++)
            {
                res += (long)(nums[i] * m10);
                m10 *= 10;
            }
            return res;
        }

        //szuka najlepszej zamiany i zwraca indeks przed zmieniona pozycja
        //po kazdym znalezionym indeksie szukany jest lepszy wynik w zmniejszonym zakresie tablicy
        private int FindChange(List<int> numbers)
        {
            int changefrom = -1;
            int currchange = numbers.Count - 1;
            for (int i = 0; i <= currchange; i++)
            {
                int border = currchange;
                if ((numbers[i] == 0) && (currchange == numbers.Count - 1)) border--;
                for (int j = i + 1; j <= border; j++)
                {
                    if (numbers[i] < numbers[j])
                    {
                        //numbers.Insert(j + 1, numbers[i]);
                        //numbers.RemoveAt(i);
                        //return j;
                        changefrom = i;
                        currchange = j - 1;
                        break;
                    }
                }
            }

            if (changefrom > -1)
            {
                numbers.Insert(currchange + 2, numbers[changefrom]);
                numbers.RemoveAt(changefrom);
            }
            return currchange;
        }

        public long NextSmaller(long n)
        {
            if (n < 10) return -1;
            List<int> numbers = NumToList(n);

            int changeindex = FindChange(numbers);
            if(changeindex < numbers.Count-1)
            {
                numbers.Sort(0, changeindex+1, null);
                return ListToNum(numbers);
            }
            return -1;
        }
    }
}
