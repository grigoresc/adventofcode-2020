using System.IO;

namespace day_6
{
    public class Program
    {
        static void Main(string[] args)
        {
            Solve1();
            Solve2();
        }

        static public void Solve1()
        {
            aoc.utils.Log.WriteLine("--------------------------------------------------------");

            var input = File.ReadAllLines("input.txt");

            //aoc.utils.Log.WriteLines(input);
            aoc.utils.Log.WriteLine(input.Length);
        }
        static public void Solve2()
        {

        }
    }
}
