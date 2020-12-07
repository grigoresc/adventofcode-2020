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

            Solve2(File.ReadAllText("day-7.input.txt"));//32,126,4165
        }

        static int Solve1(string input)
        {
            IEnumerable<Tuple<string, bag>> all = Parse(input);

            var src = "shiny gold bags";

            var ret = all.Where(o =>
            {
                return Contain(1, o, all, src);

            }).Count();

            aoc.utils.Log.WriteLine($"ret:{ret}");

            return all.Count();
        }

        private static IEnumerable<Tuple<string, bag>> Parse(string input)
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
                    //Log.WriteLine(name);
                    if (no == "no")
                        return (0, "No");
                    else
                        return (int.Parse(no), name);
                }).ToList();

                bag b;
                b.name = bn;
                b.inside = lbs.Select(o => o.Item2).ToList();
                b.cnt = lbs.Select(o => o.Item1).ToList();

                return new Tuple<string, bag>(bn, b);
            });
            return all;
        }

        static int Solve2(string input)
        {
            IEnumerable<Tuple<string, bag>> all = Parse(input);

            var src = "shiny gold bags";
            var ret = Sum(1, all.FirstOrDefault(x => x.Item2.name == src), all, src);
            ret -= 1;
            aoc.utils.Log.WriteLine($"ret: {ret}");

            return all.Count();
        }

        private static int Sum(int lev, Tuple<string, bag> o, IEnumerable<Tuple<string, bag>> all, string src)
        {
            Console.WriteLine(lev + " " + o.Item1);
            var cnt = 0;
            for (int i = 0; i < o.Item2.inside.Count; i++)
            {

                var chi = all.FirstOrDefault(x => x.Item1 == o.Item2.inside[i]);

                if (chi != null)
                    cnt += o.Item2.cnt[i] * Sum(lev + 1, chi, all, src);
            }
            return cnt + 1;
        }

        private static bool Contain(int lev, Tuple<string, bag> o, IEnumerable<Tuple<string, bag>> all, string src)
        {
            Console.WriteLine(lev + " " + o.Item1);
            foreach (var co in o.Item2.inside)
            {
                var chi = all.FirstOrDefault(o => o.Item1 == co);
                if (chi != null)
                    if (Contain(lev + 1, chi, all, src))
                        return true;
            }
            return o.Item2.inside.Where(o => o == src).Count() > 0;
        }
    }
}
