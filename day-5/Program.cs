using System;
using System.IO;
using Xunit;
using System.Linq;
using System.Text.RegularExpressions;


namespace day_5
{
    class Program
    {

        static void Main(string[] args)
        {
            // Solve("BFFFBBFRRR");
            // Solve("FFFBBBFRRR");
            // Solve("BBFFBBFRLL");

            //Solve1(File.ReadAllLines("sample.txt"));
            Solve1(File.ReadAllLines("input.txt"));
            //Assert.Equal(2, Solve1(File.ReadAllText("sample.txt")));
        }
        private static int Solve1(string[] str)
        {
            System.Console.WriteLine("len=" + str.Length);
            var ret = str.Select(o => Solve(o)).Max();
            System.Console.WriteLine(ret);
            return ret;

        }
        private static int Solve(string str)
        {
            var ret = Row(str) * 8 + Column(str);
            System.Console.WriteLine(ret);
            return ret;
        }
        private static int Row(string str)
        {
            var x = 0;
            var y = 127;

            for (int i = 0; i < 7; i++)
                if (str[i] == 'F')
                    y -= (int)Math.Pow(2, 7 - i - 1);
                else
                    x += (int)Math.Pow(2, 7 - i - 1);

            System.Console.WriteLine(x);
            //System.Console.WriteLine(y);

            return x;
        }
        private static int Column(string str)
        {
            var x = 0;
            var y = 3;

            for (int i = 0; i < 3; i++)
                if (str[i + 7] == 'L')
                    y -= (int)Math.Pow(2, 3 - i - 1);
                else
                    x += (int)Math.Pow(2, 3 - i - 1);

            System.Console.WriteLine(x);
            //System.Console.WriteLine(y);

            return x;
        }

    }
}
