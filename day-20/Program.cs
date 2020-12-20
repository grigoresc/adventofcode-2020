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
            Solve1(File.ReadAllText("day-20.input.txt"));
        }

        public class Pic
        {
            public char[][] M;
            public string[] Edges;
            public int Number;
            public string Reverse(int edge)
            {
                return String.Join("", Edges[edge].Reverse());
            }
        }

        public struct Piece
        {
            public int Number;
            public bool Flipped;
            public int Rotated;//0,1,2,3
        }


        private static long Solve1(string input)
        {
            long ret = 0L;

            var inputs = input.Split(Environment.NewLine + Environment.NewLine);

            var pics = new Dictionary<int, Pic>();

            Read(inputs, pics);

            FindPossibleMatches(pics, out ret);

            Console.WriteLine(ret);
            return ret;
        }

        static void FindPossibleMatches(Dictionary<int, Pic> pics, out long prod)
        {
            System.Console.WriteLine("possible matches");
            prod = 1;
            foreach (var (n, pic) in pics)
            {
                // System.Console.WriteLine($"pic={pic.Number}");
                var cnt = 0;
                foreach (var (mn, mpic) in pics)
                    if (pic.Number != mpic.Number)
                    {
                        var match = false;
                        for (int r = 0; r < 4; r++)
                            for (int mr = 0; mr < 4; mr++)
                                if (pic.Edges[r] == mpic.Edges[mr])
                                {
                                    // System.Console.WriteLine($"match with {mpic.Number} on {r} and {mr}");
                                    match = true;
                                }
                                else if (pic.Edges[r] == mpic.Reverse(mr))
                                {

                                    match = true;
                                    // System.Console.WriteLine($"match with {mpic.Number} on {r} and {mr}/R");
                                }
                        if (match)
                            cnt++;
                    }
                if (cnt == 2)
                {

                    prod *= pic.Number;
                    System.Console.WriteLine($"{pic.Number}");
                }
            }
        }

        private static void Read(string[] inputs, Dictionary<int, Pic> pics)
        {
            foreach (var m in inputs)
            {
                (var fi, var la) = m.Split(Environment.NewLine);
                var picno = int.Parse(fi.Replace("Tile ", "").Replace(":", ""));
                var ma = readmatrix(la.ToArray());
                System.Console.WriteLine(picno);
                ma.ShowMatrix();
                var n = String.Join("", ma[0]);
                var s = String.Join("", ma[ma.Length - 1]);
                var w = String.Join("", ma.Select(line => line[0]));
                var e = String.Join("", ma.Select(line => line[line.Length - 1]));
                System.Console.WriteLine();
                System.Console.WriteLine(n);
                System.Console.WriteLine(e);
                System.Console.WriteLine(s);
                System.Console.WriteLine(w);
                pics.Add(picno, new Pic { Number = picno, M = ma, Edges = new[] { n, w, s, e } });
            }
        }

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
    }
}
