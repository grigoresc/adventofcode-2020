using System.IO;
using System.Linq;
using Xunit;
using aoc.utils;
using System.Collections.Generic;
using System;

namespace day_17
{
    public class Program
    {

        static void Main(string[] args)
        {
            Solve();
        }

        public static void Solve()
        {
            solve1(readmatrix(File.ReadAllLines("day-17.input.txt")));
        }


        private static long solve1(char[][] m)
        {
            var ret = 0L;
            m.ShowMatrix();
            Console.WriteLine();
            char[][][] t = new char[][][] { m };

            for (int i = 0; i < 6; i++)
            {
                t = Expand(t);
                t = Transform(t, out ret);
            }
            Console.WriteLine(ret);
            return ret;
        }
        static List<Tuple<int, int, int>> getdirs()
        {
            var r = new List<Tuple<int, int, int>>();
            r.Add(new Tuple<int, int, int>(0, -1, 0));
            r.Add(new Tuple<int, int, int>(0, +1, 0));
            r.Add(new Tuple<int, int, int>(0, 0, -1));
            r.Add(new Tuple<int, int, int>(0, 0, +1));
            r.Add(new Tuple<int, int, int>(0, -1, -1));
            r.Add(new Tuple<int, int, int>(0, -1, +1));
            r.Add(new Tuple<int, int, int>(0, +1, -1));
            r.Add(new Tuple<int, int, int>(0, +1, 1));

            r.Add(new Tuple<int, int, int>(-1, 0, 0));
            r.Add(new Tuple<int, int, int>(-1, -1, 0));
            r.Add(new Tuple<int, int, int>(-1, +1, 0));
            r.Add(new Tuple<int, int, int>(-1, 0, -1));
            r.Add(new Tuple<int, int, int>(-1, 0, +1));
            r.Add(new Tuple<int, int, int>(-1, -1, -1));
            r.Add(new Tuple<int, int, int>(-1, -1, +1));
            r.Add(new Tuple<int, int, int>(-1, +1, -1));
            r.Add(new Tuple<int, int, int>(-1, +1, 1));

            r.Add(new Tuple<int, int, int>(+1, 0, 0));
            r.Add(new Tuple<int, int, int>(+1, -1, 0));
            r.Add(new Tuple<int, int, int>(+1, +1, 0));
            r.Add(new Tuple<int, int, int>(+1, 0, -1));
            r.Add(new Tuple<int, int, int>(+1, 0, +1));
            r.Add(new Tuple<int, int, int>(+1, -1, -1));
            r.Add(new Tuple<int, int, int>(+1, -1, +1));
            r.Add(new Tuple<int, int, int>(+1, +1, -1));
            r.Add(new Tuple<int, int, int>(+1, +1, 1));

            return r;
        }
        public static char[][][] Transform(char[][][] m, out long active)
        {
            int zmax = m.Length;
            int lmax = m[0].Length;
            int cmax = m[0][0].Length;
            var newm = new char[zmax][][];
            active = 0;

            for (int z = 0; z < zmax; z++)
            {
                var mslice = new char[lmax][];
                for (int i = 0; i < lmax; i++)
                {
                    mslice[i] = new char[cmax];

                    for (int c = 0; c < cmax; c++)
                    {
                        mslice[i][c] = '.';
                        var dirs = getdirs();

                        var n = dirs
                            .Select(o => (z + o.Item1, i + o.Item2, c + o.Item3))
                            .Where(o => 0 <= o.Item1 && o.Item1 < zmax
                                && 0 <= o.Item2 && o.Item2 < lmax
                                && 0 <= o.Item3 && o.Item3 < cmax
                            );

                        var cntActive = n.Where(o => m[o.Item1][o.Item2][o.Item3] == '#').Count();
                        var cntInactive = n.Where(o => m[o.Item1][o.Item2][o.Item3] == '.').Count();

                        var val = m[z][i][c];
                        if (val == '#')
                        {
                            if (cntActive != 2 && cntActive != 3)
                                val = '.';
                        }
                        else
                        {
                            if (cntActive == 3)
                                val = '#';
                        }
                        mslice[i][c] = val;
                        if (val == '#') active++;
                    }
                }

                newm[z] = mslice;
            }


            foreach (var s in newm)
            {
                Console.WriteLine("transformed:");
                s.ShowMatrix();
            }
            //Console.Read();
            return newm;
        }

        public static char[][][] Expand(char[][][] m)
        {
            int zmax = m.Length + 2;
            int lmax = m[0].Length + 2;
            int cmax = m[0][0].Length + 2;
            var newm = new char[zmax][][];

            for (int z = 0; z < zmax; z++)
            {
                var mslice = new char[lmax][];
                for (int i = 0; i < lmax; i++)
                {
                    mslice[i] = new char[cmax];

                    for (int c = 0; c < cmax; c++)
                        mslice[i][c] = '.';
                }

                newm[z] = mslice;
            }

            //copy
            for (int z = 1; z < zmax - 1; z++)
            {
                for (int l = 1; l < lmax - 1; l++)
                {
                    for (int c = 1; c < cmax - 1; c++)
                    {
                        char val = m[z - 1][l - 1][c - 1];
                        newm[z][l][c] = val;
                    }
                }
            }


            foreach (var s in newm)
            {
                Console.WriteLine("slice:");
                s.ShowMatrix();
            }
            //Console.Read();
            return newm;
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
            //matrix.ShowMatrix();
            return matrix;
        }
    }
}
