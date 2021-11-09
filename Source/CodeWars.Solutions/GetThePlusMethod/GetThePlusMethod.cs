/*
 * GetThePlusMethod
 * https://www.codewars.com/kata/5630de6e99308f1fc8000061
 */
using System;

namespace CodeWars.Solutions.GetThePlusMethod
{
    public class GetThePlusMethod
    {
        public static Func<int, int> MethodFunc(int i) => 
            new Func<int, int>((x) => x + i);
    }
}
