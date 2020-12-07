using System;
using System.IO;
using System.Linq;
using Xunit;

namespace day_6
{
    public class Program
    {
        static void Main(string[] args)
        {
            Solve();
        }

        static public void Solve()
        {
            aoc.utils.Log.WriteLine("");
            Assert.Equal(11, Solve1(File.ReadAllText("day-6.sample.txt")));
            Assert.Equal(6382, Solve1(File.ReadAllText("day-6.input.txt")));
            Assert.Equal(6, Solve2(File.ReadAllText("day-6.sample.txt")));
            Assert.Equal(3197, Solve2(File.ReadAllText("day-6.input.txt")));
        }

        static int Solve1(string input)
        {
            var a = input.Split(Environment.NewLine + Environment.NewLine);
            aoc.utils.Log.WriteLine(a.Length);

            var ret = a.Select(g =>
            {
                var all = g.Replace(Environment.NewLine, "");
                var cnt = all.Distinct<char>().Count();
                return cnt;
            }).Sum();

            aoc.utils.Log.WriteLine($"ret:{ret}");
            return ret;
        }

        static int Solve2(string input)
        {

            var a = input.Split(Environment.NewLine + Environment.NewLine);
            aoc.utils.Log.WriteLine(a.Length);

            var ret = a.Select(g =>
              {
                  var pers = g.Split(Environment.NewLine);
                  var first = pers[0];

                  var all = g.Replace(Environment.NewLine, "");
                  var cnt = 0;
                  foreach (var a in first)
                  {
                      if (all.Count(c => c == a) == pers.Length)
                          cnt++;
                  }

                  return cnt;
              }).Sum();

            aoc.utils.Log.WriteLine($"ret:{ret}");
            return ret;
        }
    }
}
