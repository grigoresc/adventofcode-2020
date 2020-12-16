using System.IO;
using System.Linq;
using Xunit;
using aoc.utils;
using System.Collections.Generic;
using System;
using aoc.utils;

namespace day_16
{
    public class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine(solve1(File.ReadAllText("day-16.input.txt")));
        }

        private static long solve1(string input)
        {
            Read(input, out var nts, out var ru, out var ru_dep);
            var res = nts.Select(s =>
            {
                var sum = s.Split(",").Select(n => long.Parse(n)).Where(n => ru.All(r => !(r.Item1 <= n && n <= r.Item2))).Sum();
                return sum;
            }).Sum();

            return res;
        }

        private static long solve2(string input)
        {
            Read(input, out var nts, out var ru, out var ru_dep);

            long dest = -1;
            var res = nts.First(s =>
            {
                var invalid = s.Split(",").Select(n => long.Parse(n)).Where(n => ru.All(r => !(r.Item1 <= n && n <= r.Item2))).Count() > 0;
                if (invalid)
                    return false;

                var rule = ru_dep.Select((o, idx) =>
                {
                    //s.Split(",").Select(n => long.Parse(n)).Where((n, idx) => )
                    //o
                    return o;
                });
                return true;
            });

            return dest;
        }

        private static void Read(string input, out IEnumerable<string> nts, out (long, long)[] ru, out (long, long)[] ru_dep)
        {
            var (fi, mid, last, _) = input.Split(Environment.NewLine + Environment.NewLine);
            var rus = fi.Split(Environment.NewLine);
            var yts = mid.Split(Environment.NewLine)[1];
            nts = last.Split(Environment.NewLine).Skip(1);
            ru = Rules(rus);
            ru_dep = Rules(rus.Where(s => s.StartsWith("departure")).ToArray());
        }

        private static (long, long)[] Rules(string[] rus)
        {
            (long, long)[] ru;
            var ru1 = rus.Select(
                (string s, int pos) =>
                {
                    (string f, string r, IList<string> _) = s.Split(": ");
                    (string r1s, string r2s, IList<string> _) = r.Split(" or ");
                    (string n1s, string m1s, IList<string> _) = r1s.Split("-");
                    (string n2s, string m2s, IList<string> _) = r2s.Split("-");
                    return (long.Parse(n1s), long.Parse(m1s));
                });
            var ru2 = rus.Select(
                (string s, int pos) =>
                {
                    (string f, string r, IList<string> _) = s.Split(": ");
                    (string r1s, string r2s, IList<string> _) = r.Split(" or ");
                    (string n1s, string m1s, IList<string> _) = r1s.Split("-");
                    (string n2s, string m2s, IList<string> _) = r2s.Split("-");
                    return (long.Parse(n2s), long.Parse(m2s));
                });
            ru = ru1.Concat(ru2).ToArray();
            return ru;
        }
    }
}
