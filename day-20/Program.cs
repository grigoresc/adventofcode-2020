using System.IO;
using System.Linq;
using Xunit;
using aoc.utils;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace day_20
{
    public class Program
    {

        static void Main(string[] args)
        {
            Solve();
        }

        public static void Solve()
        {
            Solve1(File.ReadAllText("day-20.sample.txt"));
        }

        private static long Solve1(string input)
        {
            long ret = 0L;
            Console.WriteLine(ret);
            return ret;
        }

    }

}