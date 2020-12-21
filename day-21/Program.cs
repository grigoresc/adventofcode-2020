using System.IO;
using System.Linq;
using Xunit;
using aoc.utils;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace day_21
{
    public class Program
    {

        static void Main(string[] args)
        {
            var reg = new Regex("a b c (contains z y z)");
            var string = "a b c (contains z y z)";
            var m = reg.Match(string);

            // Solve(File.ReadAllLines("day-21.sample.txt"));
        }

        public static void Solve(string [] lines)
        {

        }

    }

}
