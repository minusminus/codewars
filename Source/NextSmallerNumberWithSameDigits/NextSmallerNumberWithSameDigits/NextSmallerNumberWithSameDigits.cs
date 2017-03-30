using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private int FindChange(List<int> numbers)
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                int border = numbers.Count;
                if (numbers[i] == 0) border--;
                for (int j = i + 1; j < border; j++)
                {
                    if (numbers[i] < numbers[j])
                    {
                        numbers.Insert(j + 1, numbers[i]);
                        numbers.RemoveAt(i);
                        return j;
                    }
                }
            }
            return -1;
        }

        public long NextSmaller(long n)
        {
            if (n < 10) return -1;
            List<int> numbers = NumToList(n);

            int changeindex = FindChange(numbers);
            if (changeindex > -1)
            {
                numbers.Sort(0, changeindex, null);
                return ListToNum(numbers);
            }
            return -1;
        }
    }
}
