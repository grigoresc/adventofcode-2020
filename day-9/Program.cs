using System.IO;
using System.Linq;
using Xunit;
using aoc.utils;
using System.Collections.Generic;
using System;

namespace day_9
{
    public class Program
    {
        static void Main(string[] args)
        {
            Solve();
        }
        public static void Solve()
        {
            Solve1(File.ReadAllLines("day-9.sample.txt"), 5);
            Solve1(File.ReadAllLines("day-9.input.txt"), 25);
            Solve2(File.ReadAllLines("day-9.sample.txt"), 5);
            Solve2(File.ReadAllLines("day-9.input.txt"), 25);
        }

        private static long Solve1(string[] lines, int p)
        {
            long acc = Run1(lines, out var pos, p);
            var ret = acc;
            Console.WriteLine(ret);
            return ret;
        }

        private static long Run1(string[] lines, out int pos, int p)
        {
            var a = lines.Select(o => long.Parse(o)).ToArray();
            pos = -1;

            for (int x = p; x < a.Length; x++)
            {
                var e = a[x];
                var found = false;
                for (int i = 0; i < p; i++)
                    for (int j = i + 1; j < p; j++)
                        if (e == a[x - i - 1] + a[x - j - 1])
                        {
                            found = true;
                            break;
                        }
                if (!found)
                {
                    pos = x;
                    return e;
                }
            }
            return 0;
        }

        private static long Solve2(string[] lines, int p)
        {
            Run1(lines, out var pos, p);
            var ret = Run2(lines, pos);
            Console.WriteLine(ret);
            return ret;
        }

        private static long Run2(string[] lines, int pos)
        {
            var a = lines.Select(o => long.Parse(o)).ToArray();
            for (int x = 0; x < pos; x++)
                for (int len = 1; len < pos + 1 - x; len++)
                {
                    var sum = a.Skip(x).Take(len).Sum();
                    if (sum == a[pos])
                    {
                        return a.Skip(x).Take(len).Min() + a.Skip(x).Take(len).Max();
                    }
                }
            return 0;
        }
    }
}
