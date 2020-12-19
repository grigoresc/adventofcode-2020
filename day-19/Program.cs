using System.IO;
using System.Linq;
using Xunit;
using aoc.utils;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace day_19
{
    public class Program
    {

        static void Main(string[] args)
        {
            Solve();
        }

        public static void Solve()
        {
            Assert.Equal(12, Solve2(File.ReadAllText("day-19.sample2.p2.txt")));
            Assert.Equal(414, Solve2(File.ReadAllText("day-19.input.txt")));
            //solve1(File.ReadAllText("day-19.sample2.p2.txt"));
        }

        private static long Solve2(string input)
        {
            Dictionary<long, Rule> rules;
            string[] lines;
            Parse(input, out rules, out lines);

            var all42_reg = ConstructRegex(rules, 42);
            var all31_reg = ConstructRegex(rules, 31);

            var allstrings_reg_a = new List<string>();

            //12 max 12 chunks; reg has 1+2 min; so we may have either 10*1+2 or 1+2*5
            for (var rleft = 1; rleft <= 10; rleft++)
            {
                for (var rright = 1; rright <= 5; rright++)
                {
                    var allstrings_reg = $"^({all42_reg}){{{rleft}}}({all42_reg}){{{rright}}}({all31_reg}{{{rright}}})$";
                    allstrings_reg_a.Add(allstrings_reg);
                }
            }
            var sln = new List<int>();
            long ret = 0L;
            foreach (var o in allstrings_reg_a)
            {
                var r = new Regex(o);
                var f = lines
                            .Select((o, idx) => (o, idx))
                            .Where((s, idx) => !sln.Contains(idx) && r.IsMatch(s.o))
                            .Select(o => o.idx).ToList();
                if (f.Count() > 0)
                    sln.AddRange(f);
            };

            ret = sln.Count();

            Console.WriteLine(ret);
            return ret;
        }

        private static void Parse(string vs, out Dictionary<long, Rule> rules, out string[] li)
        {

            var (fi, la, _) = vs.Split(Environment.NewLine + Environment.NewLine);
            var ru = fi.Split(Environment.NewLine);
            rules = ru.Select(s =>
            {
                var (pos, v, _) = s.Split(": ");
                long[][] vl = null;
                char? c = null;
                if (v.Contains("\""))
                {
                    c = v[1];
                }
                else
                {
                    vl = v.Split(" | ").Select(s =>
                    {
                        return s.Split(" ").Select(long.Parse).ToArray();
                    }).ToArray();
                }
                return new Rule(long.Parse(pos), vl, c);
            }).ToDictionary(k => k.pos, v => v);
            li = la.Split(Environment.NewLine);
        }

        private static string ConstructRegex(Dictionary<long, Rule> rules, long pos)
        {
            var r = rules[pos];

            if (r.cha.HasValue)
                return rules[pos].cha.ToString();

            var ret = new List<string>();
            foreach (var ru in r.rules)
            {

                var s = "";
                foreach (var ruin in ru)
                {
                    var li = ConstructRegex(rules, ruin);

                    s += li;
                }
                s = "(" + s + ")";
                ret.Add(s);
            }
            return "(" + string.Join("|", ret) + ")";
        }
    }

    internal struct Rule
    {
        public long pos;
        public long[][] rules;
        public char? cha;

        public Rule(long pos, long[][] rules, char? cha)
        {
            this.pos = pos;
            this.rules = rules;
            this.cha = cha;
        }

    }
}