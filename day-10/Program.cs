using System.IO;
using System.Linq;
using Xunit;
using aoc.utils;
using System.Collections.Generic;
using System;

namespace day_10
{
    public class Program
    {
        static long[] c;
        static void Main(string[] args)
        {
            Solve();
        }
        public static void Solve()
        {
            Solve(File.ReadAllLines("day-10.input.txt"));
            Solve2(File.ReadAllLines("day-10.input.txt"));
        }
        private static long Solve(string[] lines)
        {
            long ret = 1;
            var a = lines.Select(o => long.Parse(o)).OrderBy(o => o).ToArray();
            //a.WriteLines();
            ret = a.Length;
            var pos = 0;
            long prev = 0;
            var ones = 0;
            var threes = 0;
            while (pos < a.Length)
            {
                var diff = a[pos] - prev;
                if (diff == 1)
                    ones++;
                else if (diff == 3)
                    threes++;
                prev = a[pos];

                pos++;
            }
            threes += 1;
            //Console.WriteLine($"{ones}:{threes}");
            ret = ones * threes;
            Console.WriteLine(ret);
            return ret;
        }
        private static long Solve2(string[] lines)
        {
            var a = lines.Select(o => long.Parse(o)).OrderBy(o => o).ToArray();
            c = new long[a.Length].Select(o => (long)-1).ToArray();

            //a.WriteLines();
            var ret = calc(a, 0) + calc(a, 1) + calc(a, 2);
            Console.WriteLine(ret);
            return ret;
        }

        private static long calc(long[] a, int pos)
        {
            if (pos == a.Length - 1)
                return 1;

            long cnt = 0;

            if (a[pos + 1] - a[pos] <= 3)
                cnt += c[pos + 1] != -1 ? c[pos + 1] : calc(a, pos + 1);

            if (pos < a.Length - 2 && a[pos + 2] - a[pos] <= 3)
                cnt += c[pos + 2] != -1 ? c[pos + 2] : calc(a, pos + 2);

            if (pos < a.Length - 3 && a[pos + 3] - a[pos] <= 3)
                cnt += c[pos + 3] != -1 ? c[pos + 3] : calc(a, pos + 3);

            c[pos] = cnt;
            return cnt;
        }
    }
}
