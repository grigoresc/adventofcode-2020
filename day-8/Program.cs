using System;
using System.IO;

namespace day_8
{
    public class Program
    {
        static void Main(string[] args)
        {
            Solve();
        }

        public static void Solve()
        {

            Solve1(File.ReadAllLines("day-8.sample.txt"));
            Solve1(File.ReadAllLines("day-8.input.txt"));
            Solve2(File.ReadAllLines("day-8.sample.txt"));
            Solve2(File.ReadAllLines("day-8.sample.txt"));
            Solve2(File.ReadAllLines("day-8.input.txt"));
        }
        private static int Solve1(string[] lines)
        {
            var ret = 0;
            Console.WriteLine(ret);
            return ret;
        }
        private static int Solve2(string[] lines)
        {
            var ret = 0;
            Console.WriteLine(ret);
            return ret;
        }


    }
}
