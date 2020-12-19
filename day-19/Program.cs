using System.IO;
using System.Linq;
using Xunit;
using aoc.utils;
using System.Collections.Generic;
using System;

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
            solve1(File.ReadAllText("day-19.input.txt"));
            //solve1(File.ReadAllText("day-19.sample2.p2.txt"));
        }

        private static long solve1(string vs)
        {
            var ret = 0L;
            var (fi, la, _) = vs.Split(Environment.NewLine + Environment.NewLine);
            var ru = fi.Split(Environment.NewLine);
            var rules = ru.Select(s =>
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


            var all = construct(rules, 0);
            //Console.WriteLine(all.Count());
            //var a8 = construct(rules, 8);
            //var a11 = construct(rules, 11);
            //var a42 = construct(rules, 42);
            //var a31 = construct(rules, 31);
            var li = la.Split(Environment.NewLine);

            var allstrings = all.Select(m =>
            {
                var chars = m.Select(r => rules[r].cha.GetValueOrDefault().ToString()).ToArray();
                return string.Join("", chars);
            }).ToArray();
            var m = li.Where(l => allstrings.Contains(l));
            ret = m.Count();

            Console.WriteLine(ret);
            return ret;
        }

        private static long[][] construct(Dictionary<long, Rule> rules, long pos)
        {
            var r = rules[pos];

            if (r.cha.HasValue)
                return new[] { new[] { pos } };

            var ret = new List<long[]>();
            foreach (var ru in r.rules)
            {
                long[][] s = null;

                foreach (var ruin in ru)
                {
                    var li = construct(rules, ruin);

                    if (s == null)
                    {
                        s = li;
                        continue;
                    }

                    s = (from first in s
                         from second in li
                         select first.Concat(second).ToArray()).ToArray();
                }
                ret.AddRange(s);
            }
            return ret.ToArray();
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
