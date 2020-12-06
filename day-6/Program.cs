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
            Assert.Equal(11, Solve1(File.ReadAllText("sample.txt")));
            Assert.Equal(6382, Solve1(File.ReadAllText("input.txt")));
            Assert.Equal(6, Solve2(File.ReadAllText("sample.txt")));
            Assert.Equal(3197, Solve2(File.ReadAllText("input.txt")));
        }

        static int Solve1(string input)
        {
            var a = input.Split("\r\n\r\n");
            aoc.utils.Log.WriteLine(a.Length);

            var ret = a.Select(g =>
            {
                var all = g.Replace("\r\n", "");
                var cnt = all.Distinct<char>().Count();
                return cnt;
            }).Sum();

            aoc.utils.Log.WriteLine($"ret:{ret}");
            return ret;
        }

        static int Solve2(string input)
        {

            var a = input.Split("\r\n\r\n");
            aoc.utils.Log.WriteLine(a.Length);

            var ret = a.Select(g =>
              {
                  var pers = g.Split("\r\n");
                  var first = pers[0];

                  var all = g.Replace("\r\n", "");
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
