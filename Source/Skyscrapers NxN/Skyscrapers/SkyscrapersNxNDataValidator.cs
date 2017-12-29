using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscrapers
{
    public class SkyscrapersNxNDataValidator
    {
        private readonly int _n;

        public SkyscrapersNxNDataValidator(int N)
        {
            _n = N;
        }

        private bool CheckDataReduced(SkyscraperData d)
        {
            for (int i = 0; i < _n; i++)
                for (int j = 0; j < _n; j++)
                    if (d.CountBits(i, j) != 1) return false;
            return true;
        }

        private bool CheckDataElements(SkyscraperData d)
        {
            for (int i = 0; i < _n; i++)
            {
                int mask = 0, mask2 = 0;
                for (int j = 0; j < _n; j++)
                {
                    mask |= d.Data[i, j];
                    mask2 |= d.Data[j, i];
                }
                if ((mask != SkyscraperData.InitialValues[_n]) || (mask2 != SkyscraperData.InitialValues[_n]))
                    return false;
            }
            return true;
        }

        private bool CheckDataUniqueElements(SkyscraperData d)
        {
            for (int i = 0; i < _n; i++)
            {
                int mask = 0, mask2 = 0;
                int cnt = 0, cnt2 = 0;
                for (int j = 0; j < _n; j++)
                {
                    if (d.CountBits(i, j) == 1)
                    {
                        mask ^= d.Data[i, j];
                        cnt++;
                    }
                    if (d.CountBits(j, i) == 1)
                    {
                        mask2 ^= d.Data[j, i];
                        cnt2++;
                    }
                }
                if ((d.CountBits(mask) != cnt) || (d.CountBits(mask2) != cnt2))
                    return false;
            }
            return true;
        }

        private void CheckDataConstraintsSingleCheck(ref int v, ref int highest, int dataval)
        {
            if (dataval > highest)
            {
                v++;
                highest = dataval;
            }
        }

        private bool CheckDataConstraints(SkyscraperData d, int[] constraints)
        {
            int v;
            int highest;
            for (int i = 0; i < _n; i++)
            {
                //poziomo
                if (constraints[4 * _n - 1 - i] != 0)
                {
                    v = 1;
                    highest = d.Data[i, 0];
                    for (int j = 0; j < _n; j++)
                        CheckDataConstraintsSingleCheck(ref v, ref highest, d.Data[i, j]);
                    if (constraints[4 * _n - 1 - i] != v) return false;
                }
                if (constraints[_n + i] != 0)
                {
                    v = 1;
                    highest = d.Data[i, _n - 1];
                    for (int j = _n - 1; j >= 0; j--)
                        CheckDataConstraintsSingleCheck(ref v, ref highest, d.Data[i, j]);
                    if (constraints[_n + i] != v) return false;
                }
                //pionowo
                if (constraints[i] != 0)
                {
                    v = 1;
                    highest = d.Data[0, i];
                    for (int j = 0; j < _n; j++)
                        CheckDataConstraintsSingleCheck(ref v, ref highest, d.Data[j, i]);
                    if (constraints[i] != v) return false;
                }
                if (constraints[3 * _n - 1 - i] != 0)
                {
                    v = 1;
                    highest = d.Data[_n - 1, i];
                    for (int j = _n - 1; j >= 0; j--)
                        CheckDataConstraintsSingleCheck(ref v, ref highest, d.Data[j, i]);
                    if (constraints[3 * _n - 1 - i] != v) return false;
                }
            }
            return true;
        }

        public bool CheckData(SkyscraperData d, int[] constraints, ref bool incorrectElements)
        {
            incorrectElements = false;
            if (!CheckDataUniqueElements(d))
            {
                SkyscrapersCounters.IncorrectDataCount++;
                //Console.WriteLine($"incorrect data count: {SkyscrapersCounters.IncorrectDataCount}");
                incorrectElements = true;
                return false;
            }
            if (!CheckDataReduced(d)) return false;
            //if (!CheckDataElements(d))
            //{
            //    SkyscrapersCounters.IncorrectDataCount++;
            //    Console.WriteLine($"incorrect data count: {SkyscrapersCounters.IncorrectDataCount}");
            //    incorrectElements = true;
            //    return false;
            //}
            return CheckDataConstraints(d, constraints);
        }
    }
}
