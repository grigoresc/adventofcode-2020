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
            Solve1(File.ReadAllText("sample.txt"));
            //Assert.Equal(2, Solve1(File.ReadAllText("sample.txt")));
        }

        private static int Solve1(string str)
        {
            var ps = str.Split("\n\n");
            var cnt = 0;

            System.Console.WriteLine(cnt);
            return cnt;
        }


    }
}
