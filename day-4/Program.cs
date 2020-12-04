using System;
using System.IO;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace day_4
{
    class Program
    {


        static char[][] readmatrix(string[] input)
        {
            char[][] matrix = new char[input.Length][];
            for (int i = 0; i < input.Length; i++)
            {
                matrix[i] = new char[input[i].Length];
                for (int j = 0; j < input[i].Length; j++)
                    matrix[i][j] = input[i][j];
            }
            matrix.ShowMatrix();
            return matrix;
        }
        // static void Main(string[] args)
        // {

        //     var str = File.ReadAllText("input.txt");

        //     // Compute(readmatrix(File.ReadAllLines("sample.txt")));//7
        //     var ps = str.Split("\n\n");
        //     System.Console.WriteLine(ps.Length);
        //     var cnt = 0;
        //     foreach (var p in ps)
        //     {
        //         var vals = p.Replace("\n", " ").Split(" ").Select(s =>
        //         {
        //             var kv = s.Split(":");
        //             return kv[0];
        //         });

        //         var req = new[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
        //         var ok = true;
        //         foreach (var r in req)
        //             if (!vals.Contains(r)) ok = false;

        //         if (ok)
        //             cnt++;
        //     }
        //     System.Console.WriteLine(cnt);//245
        //     // Compute(readmatrix(File.ReadAllLines("input.txt")));//292
        //     // Compute2(readmatrix(File.ReadAllLines("sample.txt")));//336
        //     // Compute2(readmatrix(File.ReadAllLines("input.txt")));//9354744432
        // }
        static void Main(string[] args)
        {

            var str = File.ReadAllText("input.txt");

            // Compute(readmatrix(File.ReadAllLines("sample.txt")));//7
            var ps = str.Split("\n\n");
            System.Console.WriteLine(ps.Length);
            var cnt = 0;
            foreach (var p in ps)
            {
                System.Console.WriteLine(p.Replace("\n", " "));
                var vals = p.Replace("\n", " ").Split(" ").Select(s =>

                {
                    var kv = s.Split(":");
                    return new Tuple<string, string>(kv[0], kv[1]);
                });

                var req = new[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
                var oktotal = true;
                foreach (var r in req)
                {
                    var ok = true;
                    var pair = vals.SingleOrDefault(o => o.Item1 == r);

                    if (pair == null) ok = false;
                    else
                    {
                        var value = pair.Item2;
                        switch (r)
                        {
                            case "byr":
                                ok = findrange(ok, value, 1920, 2002);
                                break;
                            case "iyr":
                                ok = findrange(ok, value, 2010, 2020);
                                break;
                            case "eyr":
                                ok = findrange(ok, value, 2020, 2030);
                                break;
                            case "hgt":
                                if (matches(ok, value, "^[0-9]{3,3}cm$"))
                                {
                                    ok = findrange(ok, value.Replace("cm", ""), 150, 193);
                                }
                                else
                                if (matches(ok, value, "^([0-9]{2,2}in)$"))
                                {
                                    ok = findrange(ok, value.Replace("in", ""), 59, 76);
                                }
                                else
                                {
                                    ok = false;
                                }
                                break;
                            case "hcl":
                                ok = matches(ok, value, "^#[0-9a-f]{6,6}$");
                                break;
                            case "ecl":
                                ok = matches(ok, value, "^(amb|blu|brn|gry|grn|hzl|oth)$");
                                break;
                            case "pid":
                                ok = matches(ok, value, "^[0-9]{9,9}$");
                                break;
                        }
                        System.Console.WriteLine($"{r}:{value} {ok}");
                    }


                    oktotal = oktotal && ok;
                    // if (!ok)
                    //     break;
                }
                System.Console.WriteLine(oktotal);
                if (oktotal)
                    cnt++;
            }
            System.Console.WriteLine(ps.Length);//133
            System.Console.WriteLine(cnt);//245
            // Compute(readmatrix(File.ReadAllLines("input.txt")));//292
            // Compute2(readmatrix(File.ReadAllLines("sample.txt")));//336
            // Compute2(readmatrix(File.ReadAllLines("input.txt")));//9354744432
        }

        private static bool matches(bool ok, string value, string reg)
        {
            Regex rx = new Regex(reg);
            if (!rx.Match(value).Success)
                ok = false;
            return ok;
        }

        private static bool findrange(bool ok, string value, int min, int max)
        {
            if (!int.TryParse(value, out var byr))
                ok = false;
            else if (!(min <= byr && byr <= max))
                ok = false;
            return ok;
        }

        static double Compute(char[][] matrix)
        {
            return Compute_(matrix, 1, 3);

        }

        static long Compute2(char[][] matrix)
        {
            long ret = Compute_(matrix, 1, 1)
                * Compute_(matrix, 1, 3)
                * Compute_(matrix, 1, 5)
                * Compute_(matrix, 1, 7)
                * Compute_(matrix, 2, 1);
            System.Console.WriteLine(ret);
            return ret;
        }

        static long Compute_(char[][] matrix, int iinc, int jinc)
        {
            var ilen = matrix.Length;
            var jlen = matrix[0].Length;

            var i = 0;
            var j = 0;
            var cnt = 0;

            do
            {
                // System.Console.WriteLine($"{i} {j}");
                i += iinc;
                j += jinc;
                // if (j >= jlen)
                {
                    j = j % jlen;
                    // i = 0;
                }
                if (matrix[i][j] == '#')
                    cnt++;

            } while (i != ilen - 1);

            System.Console.WriteLine(cnt);
            return cnt;
        }
    }
}
