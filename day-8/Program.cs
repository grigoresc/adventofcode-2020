using System;
using System.IO;
using aoc.utils;
using System.Linq;

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
            Solve2(File.ReadAllLines("day-8.input.txt"));
            //8
            //1331
            //8
            //1121
        }

        private static int Solve1(string[] lines)
        {
            int acc = Run(lines);
            var ret = acc;
            Console.WriteLine(ret);
            return ret;
        }

        private static int Run(string[] lines)
        {
            var ex = new int[lines.Length];
            var acc = 0;
            var pos = 0;
            while (ex[pos] == 0)
            {
                ex[pos] = 1;
                var (cmd, var, _) = lines[pos].Split(" ");
                switch (cmd)
                {
                    case "nop":
                        pos++;
                        break;
                    case
                        "jmp":
                        pos += int.Parse(var);
                        break;
                    case
                        "acc":
                        acc += int.Parse(var);
                        pos++;
                        break;
                }


            }

            return acc;
        }

        private static int Solve2(string[] lines)
        {
            for (int i = 0; i < lines.Length - 1; i++)
            {
                int pos;
                int acc = Run(lines, i, out pos);
                if (pos == lines.Length)
                {
                    var ret = acc;
                    Console.WriteLine(ret);
                    return ret;
                }
            }
            throw new Exception("shouldnt be here");
        }

        private static int Run(string[] lines, int s, out int pos)
        {
            var ex = new int[lines.Length];
            var acc = 0;
            pos = 0;
            while (ex[pos] == 0)
            {
                ex[pos] = 1;
                var (cmd, var, _) = lines[pos].Split(" ");
                if (pos == s)
                {
                    if (cmd == "nop") cmd = "jmp";
                    else if (cmd == "jmp") cmd = "nop";

                }
                switch (cmd)
                {
                    case "nop":
                        pos++;
                        break;
                    case
                        "jmp":
                        pos += int.Parse(var);
                        break;
                    case
                        "acc":
                        acc += int.Parse(var);
                        pos++;
                        break;
                }
                if (pos >= lines.Length)
                    break;

            }

            return acc;
        }


    }
}
