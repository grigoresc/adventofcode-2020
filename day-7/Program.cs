using System.IO;
using System.Linq;
using Xunit;
using aoc.utils;
using System.Collections.Generic;
using System;

namespace day_7
{
    struct Bag
    {
        public string name;
        public IList<int> cnt;
        public IList<string> inside;
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Solve();
        }

        static public void Solve()
        {
            Assert.Equal(4, Solve1(File.ReadAllText("day-7.sample.txt")));
            Assert.Equal(302, Solve1(File.ReadAllText("day-7.input.txt")));

            Assert.Equal(32, Solve2(File.ReadAllText("day-7.sample.txt")));
            Assert.Equal(126, Solve2(File.ReadAllText("day-7.sample2.txt")));
            Assert.Equal(4165, Solve2(File.ReadAllText("day-7.input.txt")));

        }

        private static Dictionary<string, Bag> Parse(string input)
        {
            var a = input.Split("\r\n");

            var all = a.Select(g =>
            {
                var (bn, bags, _) = g.Split(" contain ");
                var lb = bags.Substring(0, bags.Length - 1).Split(", ");
                var lbs = lb.Select(o =>
                {
                    if (o.EndsWith("bag"))
                        o += "s";

                    var (no, _) = o.Split(" ");
                    var name = o.Replace(no + " ", "");
                    if (no == "no")
                        return (0, "No");
                    else
                        return (int.Parse(no), name);
                }).ToList();

                Bag b;
                b.name = bn;
                b.inside = lbs.Select(o => o.Item2).ToList();
                b.cnt = lbs.Select(o => o.Item1).ToList();

                return b;
            }).ToDictionary(o => o.name);

            return all;
        }
        static int Solve1(string input)
        {
            var all = Parse(input);

            var src = "shiny gold bags";

            var ret = all.Where(o =>
            {
                return Contain(o.Value, all, src);

            }).Count();

            Console.WriteLine(ret);
            return ret;
        }
        static int Solve2(string input)
        {
            var all = Parse(input);

            var src = "shiny gold bags";
            var ret = Sum(all[src], all);
            ret -= 1;

            Console.WriteLine(ret);
            return ret;
        }

        private static int Sum(Bag o, Dictionary<string, Bag> all)
        {
            var cnt = 0;
            for (int i = 0; i < o.inside.Count; i++)
            {
                var cb = o.inside[i];
                if (all.ContainsKey(cb))
                {
                    var chi = all[cb];
                    cnt += o.cnt[i] * Sum(chi, all);
                }
            }
            return cnt + 1;
        }

        private static bool Contain(Bag o, Dictionary<string, Bag> all, string src)
        {
            foreach (var cb in o.inside)
            {
                if (all.ContainsKey(cb))
                {
                    var chi = all[cb];
                    if (Contain(chi, all, src))
                        return true;
                }
            }
            return o.inside.Where(o => o == src).Count() > 0;
        }
    }
}
