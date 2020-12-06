using System;
using System.IO;
using Xunit;
using System.Linq;
using aoc.utils;

namespace day_2
{
    public class Program
    {
        struct Policy
        {
            public int min;
            public int max;
            public char letter;
            public string password;
        }

        static Policy ToPolicy(string l)
        {
            Policy pol;
            (string policy, string password, _) = l.Split(": ");
            var (interval, letter, _) = policy.Split(" ");
            var (min, max, _) = interval.Split("-");
            pol.min = int.Parse(min);
            pol.max = int.Parse(max);
            pol.letter = letter[0];
            pol.password = password;

            return pol;

        }

        static void Main(string[] args)
        {
            Solve();
        }

        public static void Solve()
        {
            var numbers_sample = File.ReadAllLines("day-2.sample.txt").Select(o => ToPolicy(o)).ToArray();
            var numbers = File.ReadAllLines("day-2.input.txt").Select(o => ToPolicy(o)).ToArray();

            Assert.Equal(2, compute(numbers_sample));
            Assert.Equal(467, compute(numbers));


            Assert.Equal(1, compute2(numbers_sample));
            Assert.Equal(441, compute2(numbers));
        }

        static int compute(Policy[] ps)
        {
            int cnt = 0;
            foreach (var p in ps)
            {
                var rep = p.password.Count(c => c == p.letter);
                if (p.min <= rep && rep <= p.max)
                    cnt++;
            }

            System.Console.WriteLine(cnt);
            return cnt;

        }

        static int compute2(Policy[] ps)
        {
            int cnt = 0;
            foreach (var p in ps)
            {
                var one = p.password[p.min - 1] == p.letter;
                var two = p.password[p.max - 1] == p.letter;
                // System.Console.WriteLine($"{one}|{two}|{p.password}|{p.letter}|{p.min}|{p.max}");
                if (one ^ two)
                    cnt++;
            }

            System.Console.WriteLine(cnt);
            return cnt;

        }
    }
}
