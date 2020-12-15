using System.IO;
using System.Linq;
using Xunit;
using aoc.utils;
using System.Collections.Generic;
using System;

namespace day_14
{
    public class Program
    {


        static void Main(string[] args)
        {
            //Console.WriteLine(solve1(File.ReadAllLines("day-14.sample.txt")));
            Console.WriteLine(solve1(File.ReadAllLines("day-14.input.txt")));
            //Console.WriteLine(solve2(File.ReadAllLines("day-14.sample2.only2nd.txt")));
            Console.WriteLine(solve2(File.ReadAllLines("day-14.input.txt")));
        }

        private static long solve1(string[] vs)
        {
            var ram = new Dictionary<long, long>();
            long zeros = 0L, ones = 0L;


            foreach (var line in vs)
            {
                var (le, ri, _) = line.Split(" = ");
                if (!le.Contains("mem"))
                {
                    ProcessMask(ri, out zeros, out ones);
                    //Console.WriteLine(Convert.ToString(zeros, 2));
                    //Console.WriteLine(Convert.ToString(ones, 2));
                    continue;
                }
                var addr = long.Parse(le.Replace("mem[", "").Replace("]", ""));
                var val = long.Parse(ri);
                //Console.WriteLine(val);
                val = (val & zeros) | ones;
                //Console.WriteLine($"{addr}:{val}");
                ram[addr] = val;
            }

            return ram.Sum(o => o.Value);
        }
        private static long solve2(string[] vs)
        {
            var ram = new Dictionary<long, long>();
            List<long> zerosl = new List<long>();
            List<long> onesl = new List<long>();


            foreach (var line in vs)
            {
                var (le, ri, _) = line.Split(" = ");

                if (!le.Contains("mem"))
                {
                    //Console.WriteLine($"mask={ri}");
                    zerosl = new List<long>();
                    onesl = new List<long>();

                    var masks = new List<string>();
                    masks.Add("");

                    foreach (var c in ri)
                    {
                        if (c == '0')
                        {
                            masks = masks.Select(m => m + "x").ToList();
                        }
                        else if (c == '1')
                        {
                            masks = masks.Select(m => m + "1").ToList();
                        }
                        else
                        {
                            var masks0 = masks.Select(m => m + "0").ToList();
                            var masks1 = masks.Select(m => m + "1").ToList();
                            masks = masks0.Concat(masks1).ToList();
                        }
                    }

                    foreach (var mask in masks)
                    {
                        ProcessMask(mask, out long zeros, out long ones);
                        zerosl.Add(zeros);
                        onesl.Add(ones);
                        //Console.WriteLine(Convert.ToString(zeros, 2));
                        //Console.WriteLine(Convert.ToString(ones, 2));
                    }

                    continue;
                }

                var addr = long.Parse(le.Replace("mem[", "").Replace("]", ""));
                var val = long.Parse(ri);
                //Console.WriteLine($"op:{addr}:{val}");

                for (int i = 0; i < zerosl.Count; i++)
                {
                    addr = (addr & zerosl[i]) | onesl[i];
                    //Console.WriteLine($"->{addr}:{val}");
                    ram[addr] = val;
                }
            }

            return ram.Sum(o => o.Value);
        }

        private static void ProcessMask(string mask, out long zeros, out long ones)
        {
            zeros = 0L;
            ones = 0L;
            for (int i = 0; i < mask.Length; i++)
            {

                if (mask[i] == '0')
                {
                }
                else if (mask[i] == '1')
                {
                    zeros = zeros | 1;
                    ones = ones | 1;
                }
                else
                {
                    zeros = zeros | 1;
                }

                if (i == mask.Length - 1)
                    continue;
                zeros = zeros << 1;
                ones = ones << 1;
            }
        }

    }
}
