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
            Solve2(File.ReadAllText("day-20.input.txt"));
        }

        public class Pic
        {
            public char[][] M { get; private set; }
            public string[] Edges { get; private set; }
            public int Number { get; private set; }

            public Pic(int number, char[][] ma)
            {
                Number = number;
                M = ma;
                var n = String.Join("", ma[0]);
                var s = String.Join("", ma[ma.Length - 1]);
                var w = String.Join("", ma.Select(line => line[0]));
                var e = String.Join("", ma.Select(line => line[line.Length - 1]));
                // System.Console.WriteLine();
                // System.Console.WriteLine(n);
                // System.Console.WriteLine(e);
                // System.Console.WriteLine(s);
                // System.Console.WriteLine(w);
                Edges = new[] { n, w, s, e };
            }

            public string Reverse(int edge)
            {
                return String.Join("", Edges[edge].Reverse());
            }

            public Pic Rotate()
            {
                var N = this.M.Length;
                char[][] newM = new char[N][];
                for (int i = 0; i < N; i++)
                    newM[i] = new char[N];//it works in this situatio, since it's a square..
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < N; j++)
                        newM[j][N - i - 1] = M[i][j];

                var ret = new Pic(this.Number, newM);
                return ret;
            }

            public Pic Flip()
            {
                var N = this.M.Length;
                //todo rotate here!
                char[][] newM = new char[N][];
                for (int i = 0; i < N; i++)
                    newM[i] = new char[N];//it works in this situatio, since it's a square..
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < N; j++)
                        newM[j][i] = M[i][j];

                var ret = new Pic(this.Number, newM);
                return ret;
            }

            public Pic Copy()
            {
                var N = this.M.Length;
                //todo rotate here!
                char[][] newM = new char[N][];
                for (int i = 0; i < N; i++)
                    newM[i] = new char[N];//it works in this situatio, since it's a square..
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < N; j++)
                        newM[i][j] = M[i][j];

                var ret = new Pic(this.Number, newM);
                return ret;
            }
        }

        public struct Piece
        {
            public int Number;
            public bool Flipped;
            public int Rotated;//0,1,2,3
        }


        private static long Solve2(string input)
        {
            long ret = 0L;

            var inputs = input.Split(Environment.NewLine + Environment.NewLine);

            var pics = new Dictionary<int, Pic>();

            Read(inputs, pics);

            var len = (int)Math.Sqrt(pics.Count());
            System.Console.WriteLine(len);

            Piece[,] puzzle = new Piece[len, len];
            Dictionary<int, Pic> inner;
            var cnt = 0;
            FindPossibleMatches(pics, out ret, out inner, puzzle, cnt++);
            System.Console.WriteLine("sln1=" + ret);
            while (inner.Count() > 1)
                FindPossibleMatches(inner, out ret, out inner, puzzle, cnt++);

            Console.WriteLine(ret);
            return ret;
        }

        static Pic Match(Pic pic, Pic mpic)
        {
            for (int r = 0; r < 4; r++)
            {
                var f = TranformToMatchEdge(pic, mpic, r, out var comparingEdge);
                if (f != null)
                    return f;
            }
            return null;
        }

        private static Pic TranformToMatchEdge(Pic pic, Pic mpic, int r, out int comparingEdge)
        {
            var match = false;
            var current = mpic.Copy();

            if (r == 0)
                comparingEdge = 2;
            else if (r == 1)
                comparingEdge = 3;
            else if (r == 2)
                comparingEdge = 2;
            else if (r == 3)
                comparingEdge = 1;
            else
                throw new Exception("should not be here!");

            for (int rotation = 0; rotation < 4; rotation++)
            {
                var reverse = current.Flip();

                if (pic.Edges[r] == current.Edges[comparingEdge])
                {
                    // System.Console.WriteLine($"match with {mpic.Number} on {r} and {mr}");
                    if (match)
                        throw new Exception("already match!");
                    return current;
                    // Pic found = new Pic{Number=mpic.Numbe,r}
                }
                else if (pic.Edges[r] == reverse.Edges[comparingEdge])
                {
                    if (match)
                        throw new Exception("already match!");

                    return current;
                    // System.Console.WriteLine($"match with {mpic.Number} on {r} and {mr}/R");
                }

                current = current.Rotate();
            }
            return null;
        }

        static void FindPossibleMatches(Dictionary<int, Pic> pics, out long prod, out Dictionary<int, Pic> inner, Piece[,] puzzle, int iteration)
        {
            System.Console.WriteLine("possible matches");
            prod = 1;
            var corners = new Dictionary<int, Pic>();
            var edges = new Dictionary<int, Pic>();
            inner = new Dictionary<int, Pic>();

            foreach (var (n, pic) in pics)
            {
                // System.Console.WriteLine($"pic={pic.Number}");
                var cnt = 0;
                foreach (var (mn, mpic) in pics)
                    if (pic.Number != mpic.Number)
                    {
                        if (Match(pic, mpic) != null)
                            cnt++;
                    }
                if (cnt == 2)
                {
                    corners.Add(pic.Number, pic);
                    prod *= pic.Number;
                    // System.Console.WriteLine($"{pic.Number}");
                }
                else if (cnt == 3)
                {
                    edges.Add(pic.Number, pic);

                }
                else if (cnt == 4)
                {
                    inner.Add(pic.Number, pic);
                }
                else
                    throw new Exception("shouldnt reach here!");
            }
            // System.Console.WriteLine($"{corners.Count()}:{edges.Count()}:{inner.Count()}");

            //fill the puzzle
            // if (iteration == 0)
            // {
            //     //top,left corner
            //     puzzle[iteration, iteration].Number = corners.ElementAt(0).Key;
            //     //top edges
            //     for ()

            // }

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
                pics.Add(picno, new Pic(picno, ma));
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
