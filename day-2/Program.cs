using System;
using System.IO;
using Xunit;
using System.Linq;

namespace day_2
{
    class Program
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

            var pol_and_pass = l.Split(": ");
            var int_and_letter = pol_and_pass[0].Split(" ");
            var min_and_max = int_and_letter[0].Split("-");
            pol.min = int.Parse(min_and_max[0]);
            pol.max = int.Parse(min_and_max[1]);
            pol.letter = int_and_letter[1][0];
            pol.password = pol_and_pass[1];

            return pol;

        }

        static void Main(string[] args)
        {
            var numbers_sample = File.ReadAllLines("sample.txt").Select(o => ToPolicy(o)).ToArray();
            var numbers = File.ReadAllLines("input.txt").Select(o => ToPolicy(o)).ToArray();

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
