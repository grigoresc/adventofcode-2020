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
            var e = buildreg("ceva", "bun", 2);
            Regex r = new Regex(e);
            var m1 = r.Match("cevabun");
            m1 = r.Match("cevacevabun");
            m1 = r.Match("cevacevabun");
            m1 = r.Match("cevacevacevabun");
        }

        public static void Solve()
        {
            solve1(File.ReadAllText("day-19.input.txt"));
            //solve1(File.ReadAllText("day-19.sample2.p2.txt"));
        }

        static string buildreg(string s42, string s31, int reps)
        {
            return $"^({s42}){{1,}}({s42}){{{reps}}}({s31}){{{reps}}}$";
        }
        private static long solve1(string input)
        {
            long ret;
            Dictionary<long, Rule> rules;
            string[] li;
            Parse(input, out rules, out li);
            ret = 0L;

            var liminle = li.Select(l => l.Length).Min();
            var limaxle = li.Select(l => l.Length).Max();
            var limi = li.Where(l => l.Length == liminle).Count();
            var linonmi = li.Where(l => l.Length > liminle).Count();


            var all42_reg = ConstructRegex(rules, 42);
            var all31_reg = ConstructRegex(rules, 31);

            //12 chunks max; reg has 1+2 min;
            var allstrings_reg_a = new List<string>();

            for (var rleft = 1; rleft <= 10; rleft++)
            {
                for (var rright = 1; rright <= 5; rright++)
                {
                    var allstrings_reg = $"^({all42_reg}){{{rleft}}}({all42_reg}){{{rright}}}({all31_reg}{{{rright}}})$";
                    allstrings_reg_a.Add(allstrings_reg);
                }
            }
            var sln = new List<int>();
            foreach (var o in allstrings_reg_a)
            {
                var r = new Regex(o);
                var f = li
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

        private static string[] ExtractStrings(Dictionary<long, Rule> rules, long[][] all)
        {
            return all.Select(m =>
            {
                return Match(m, rules);
            }).ToArray();
        }

        private static bool IsEndingRule(Dictionary<long, Rule> rules, long o)
        {
            return (rules[o].pos == 110 || rules[o].pos == 39);
        }

        private static string Match(long[] m, Dictionary<long, Rule> rules)
        {
            var chars = m.Where(m => IsEndingRule(rules, m)).Select(r => rules[r].cha.GetValueOrDefault().ToString()).ToArray();
            return string.Join("", chars);
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

        private static long[][] Construct(Dictionary<long, Rule> rules, long pos)
        {
            var r = rules[pos];

            if (r.cha.HasValue)
                return new[] { new[] { pos } };

            var ret = new List<long[]>();
            foreach (var ru in r.rules)
            {
                long[][] s = new[] { new[] { pos } };

                foreach (var ruin in ru)
                {
                    var li = Construct(rules, ruin);

                    s = Combine(s, li);
                }
                ret.AddRange(s);
            }
            return ret.ToArray();
        }

        private static long[][] Combine(long[][] s, long[][] li)
        {
            s = (from first in s
                 from second in li
                 select first.Concat(second).ToArray()).ToArray();
            return s;
        }
        private static string[] CombineAndRegex(string[] s, string[] li, Func<string, string, string> applyRegex)
        {
            s = (from first in s
                 from second in li
                 select applyRegex(first, second)).ToArray();
            return s;
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

        public override bool Equals(object obj)
        {
            return obj is Rule other &&
                   pos == other.pos &&
                   EqualityComparer<long[][]>.Default.Equals(rules, other.rules) &&
                   cha == other.cha;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(pos, rules, cha);
        }

        public void Deconstruct(out long pos, out long[][] rules, out char? cha)
        {
            pos = this.pos;
            rules = this.rules;
            cha = this.cha;
        }

        public static implicit operator (long pos, long[][] rules, char? cha)(Rule value)
        {
            return (value.pos, value.rules, value.cha);
        }

        public static implicit operator Rule((long pos, long[][] rules, char? cha) value)
        {
            return new Rule(value.pos, value.rules, value.cha);
        }
    }
}
