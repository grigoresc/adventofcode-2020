using System.IO;
using System.Linq;
using Xunit;
using aoc.utils;
using System.Collections.Generic;
using System;

namespace day_16
{
    public class Program
    {

        static void Main(string[] args)
        {
            Assert.Equal(27898, solve1(File.ReadAllText("day-16.input.txt")));
            Assert.Equal(2766491048287, solve2(File.ReadAllText("day-16.input.txt")));
        }

        private static long solve1(string input)
        {
            Read(input, out var yts, out var nts, out var ru);
            var res = nts.Select(s =>
            {
                var sum = s.Split(",").Select(n => long.Parse(n)).Where(n => ru.All(r => !(r.Item1 <= n && n <= r.Item2
                            ||
                            r.Item3 <= n && n <= r.Item4))).Sum();
                return sum;
            }).Sum();

            Console.WriteLine(res);
            return res;
        }

        private static long solve2(string input)
        {
            Read(input, out var yts, out var nts, out var ru);
            var vld = nts.Select(s =>
            {
                var ret = s.Split(",").Select(n => long.Parse(n)).Select(n =>
                 {
                     var flds = ru.Select((r, idx) => (r, idx)).Where(o =>
                            o.r.Item1 <= n && n <= o.r.Item2
                            ||
                            o.r.Item3 <= n && n <= o.r.Item4
                     ).Select(o => o.idx);
                     return flds.ToArray();
                 });
                return ret.ToArray();
            }).Where(a => a.All(e => e.Length > 0)).ToArray();

            //find common validators
            var all = Enumerable.Range(0, ru.Length);
            var commons = all.Select(pos =>
            {
                var common = vld.Select(a => a[pos])
                    .Aggregate(all, (l, r) => l.Intersect(r).ToArray());
                return common;
            }).ToArray();

            //process
            var vld_per_pos = new int[ru.Length];

            do
            {
                //todo - other way to do that select?
                var onlyones = commons.Select((o, idx) => (o, idx)).Where(a => a.o.Count() == 1);

                if (onlyones.Count() >= 1)
                {
                    var onlyone = onlyones.First();
                    var vld_idx = onlyone.o.First();
                    vld_per_pos[onlyone.idx] = vld_idx;
                    for (int i = 0; i < commons.Length; i++)
                        commons[i] = commons[i].Except(new[] { vld_idx }).ToArray();
                }
                if (onlyones.Count() == 0)
                    break;

            } while (true);
            var ret = 1L;
            for (int pos = 0; pos < vld_per_pos.Length; pos++)
                if (ru[vld_per_pos[pos]].Item5.StartsWith("departure"))
                    ret *= yts[pos];
            Console.WriteLine(ret);
            return ret;
        }

        private static void Read(string input, out long[] yts, out IEnumerable<string> nts, out (long, long, long, long, string)[] ru)
        {
            var (fi, mid, last, _) = input.Split(Environment.NewLine + Environment.NewLine);
            var rus = fi.Split(Environment.NewLine);
            yts = mid.Split(Environment.NewLine)[1].Split(",").Select(long.Parse).ToArray();
            //todo parse here - and return arrays of long
            nts = last.Split(Environment.NewLine).Skip(1);
            ru = ParseRules(rus);
        }

        private static (long, long, long, long, string)[] ParseRules(string[] rus)
        {
            var ru = rus.Select(
                (string s, int pos) =>
                {
                    (string f, string r, IList<string> _) = s.Split(": ");
                    (string r1s, string r2s, IList<string> _) = r.Split(" or ");
                    (string n1s, string m1s, IList<string> _) = r1s.Split("-");
                    (string n2s, string m2s, IList<string> _) = r2s.Split("-");
                    return (long.Parse(n1s), long.Parse(m1s), long.Parse(n2s), long.Parse(m2s), f);
                });
            return ru.ToArray();
        }
    }
}
