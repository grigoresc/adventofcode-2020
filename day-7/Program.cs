using System.IO;
using System.Linq;
using Xunit;
using aoc.utils;
using System.Collections;
using System.Collections.Generic;
using System;

namespace day_7
{
    struct bag
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
            aoc.utils.Log.WriteLine("-------------------------------------");
            Assert.Equal(4, Solve1(File.ReadAllText("day-7.sample.txt")));
            //Assert.Equal(302, Solve1(File.ReadAllText("day-7.input.txt")));

            Assert.Equal(32, Solve2(File.ReadAllText("day-7.sample.txt")));//32,126,4165
            Assert.Equal(126, Solve2(File.ReadAllText("day-7.sample2.txt")));//32,126,4165
            Assert.Equal(4165, Solve2(File.ReadAllText("day-7.input.txt")));//32,126,4165

        }

        static int Solve1(string input)
        {
            var all = Parse(input);

            var src = "shiny gold bags";

            var ret = all.Where(o =>
            {
                return Contain(1, o.Value, all, src);

            }).Count();

            aoc.utils.Log.WriteLine($"ret:{ret}");

            return ret;
        }

        private static Dictionary<string, bag> Parse(string input)
        {
            var a = input.Split("\r\n");
            aoc.utils.Log.WriteLine(a.Length);

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

                bag b;
                b.name = bn;
                b.inside = lbs.Select(o => o.Item2).ToList();
                b.cnt = lbs.Select(o => o.Item1).ToList();

                return b;
            }).ToDictionary(o => o.name);

            return all;
        }

        static int Solve2(string input)
        {
            var all = Parse(input);

            var src = "shiny gold bags";
            var ret = Sum(1, all[src], all, src);
            ret -= 1;
            aoc.utils.Log.WriteLine($"ret: {ret}");

            return ret;
        }

        private static int Sum(int lev, bag o, Dictionary<string, bag> all, string src)
        {
            Console.WriteLine(lev + " " + o.name);
            var cnt = 0;
            for (int i = 0; i < o.inside.Count; i++)
            {

                var cb = o.inside[i];
                if (all.ContainsKey(cb))
                {
                    var chi = all[cb];
                    cnt += o.cnt[i] * Sum(lev + 1, chi, all, src);
                }
            }
            return cnt + 1;
        }

        private static bool Contain(int lev, bag o, Dictionary<string, bag> all, string src)
        {
            Console.WriteLine(lev + " " + o.name);
            foreach (var cb in o.inside)
            {
                if (all.ContainsKey(cb))
                {
                    var chi = all[cb];
                    if (Contain(lev + 1, chi, all, src))
                        return true;
                }
            }
            return o.inside.Where(o => o == src).Count() > 0;
        }
    }
}
