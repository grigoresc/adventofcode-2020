using System.IO;
using System.Linq;
using Xunit;
using aoc.utils;
using System.Collections.Generic;
using System;

namespace day_17
{
    public class Program
    {

        static void Main(string[] args)
        {
            Solve();
        }

        public static void Solve()
        {
            solve2(File.ReadAllLines("day-19.input.txt"));
        }

        private static long solve2(string[] vs)
        {
            var ret = 0L;
            foreach (var line in vs)
            {
                var l = line.Replace(" ", "");
                //l = string.Join("", l.Reverse().ToArray()).Replace(")", "T").Replace("(", ")").Replace("T", "(");
                var e = calc(l);
                ret += e;
                Console.WriteLine(e);

            }
            Console.WriteLine(ret);
            return ret;
        }

        private static string operand(string l, out int length)
        {
            if (l[0] != '(')
            {
                length = 1;
                return l[0].ToString();
            }
            else
            {
                var c = 0;
                var o = 1;
                var pos = 0;
                while (c != o)
                {
                    pos++;
                    if (l[pos] == '(') o++;
                    if (l[pos] == ')') c++;
                }
                length = pos + 1;
                return l.Substring(1, pos - 1);
            }
        }

        private static long calc(string l)
        {
            var leftexpr = operand(l, out var leftle);
            var left = l.StartsWith("(") ? calc(leftexpr) : long.Parse(leftexpr);

            var oppos = leftle;

            var rightpos = oppos + 1;

            var result = left;
            while (oppos < l.Length && l[oppos] == '+')
            {
                var righexp = operand(l.Substring(rightpos), out var righle);
                var right = calc(righexp);
                result += right;
                oppos = rightpos + righle;
                rightpos = oppos + 1;
            }

            if (rightpos < l.Length)
            {
                Assert.Equal('*', l[oppos]);
                var right = calc(l.Substring(rightpos));
                result *= right;
            }
            return result;
        }
    }
}
