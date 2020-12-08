using System.IO;
using System.Linq;
using Xunit;
using aoc.utils;
using System.Collections.Generic;
using System;

namespace day_9
{
    public class Program
    {
        static void Main(string[] args)
        {
            Solve();
        }
        public static void Solve()
        {
            Solve1(File.ReadAllLines("day-9.sample.txt"));
            Solve1(File.ReadAllLines("day-9.input.txt"));
            Solve2(File.ReadAllLines("day-9.sample.txt"));
            Solve2(File.ReadAllLines("day-9.input.txt"));
        }

        private static int Solve1(string[] lines)
        {
            int acc = Run1(lines);
            var ret = acc;
            Console.WriteLine(ret);
            return ret;
        }

        private static int Run1(string[] lines)
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
                int acc = Run2(lines, i, out pos);
                if (pos == lines.Length)
                {
                    var ret = acc;
                    Console.WriteLine(ret);
                    return ret;
                }
            }
            throw new Exception("shouldnt be here");
        }

        private static int Run2(string[] lines, int s, out int pos)
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
