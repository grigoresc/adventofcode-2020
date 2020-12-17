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
            Assert.Equal(322, solve1(readmatrix(File.ReadAllLines("day-17.input.txt"))));
            Assert.Equal(2000, solve2(readmatrix(File.ReadAllLines("day-17.input.txt"))));
        }

        private static long solve1(char[][] m)
        {
            return solve(m, getdirs1);
        }

        private static long solve2(char[][] m)
        {
            return solve(m, getdirs2);
        }

        private static long solve(char[][] m, Func<List<Tuple<int, int, int, int>>> getdirs)
        {
            var ret = 0L;
            char[][][] t = new char[][][] { m };
            char[][][][] ht = new char[][][][] { t };

            for (int i = 0; i < 6; i++)
            {
                ht = Expand(ht);
                ht = Transform(ht, out ret, getdirs());
            }
            Console.WriteLine(ret);
            return ret;
        }

        static List<Tuple<int, int, int, int>> getdirs1()
        {
            var r = new List<Tuple<int, int, int, int>>();
            for (var w = 0; w <= 0; w++)
                for (var z = -1; z <= 1; z++)
                    for (var x = -1; x <= 1; x++)
                        for (var y = -1; y <= 1; y++)
                            if (w != 0 || z != 0 || x != 0 || y != 0)
                                r.Add(new Tuple<int, int, int, int>(z, x, y, w));
            return r;
        }
        static List<Tuple<int, int, int, int>> getdirs2()
        {
            var r = new List<Tuple<int, int, int, int>>();
            for (var w = -1; w <= 1; w++)
                for (var z = -1; z <= 1; z++)
                    for (var x = -1; x <= 1; x++)
                        for (var y = -1; y <= 1; y++)
                            if (w != 0 || z != 0 || x != 0 || y != 0)
                                r.Add(new Tuple<int, int, int, int>(z, x, y, w));
            return r;
        }
        public static char[][][][] Transform(char[][][][] hcube, out long active, List<Tuple<int, int, int, int>> dirs)
        {
            int wmax = hcube.Length;
            int zmax = hcube[0].Length;
            int xmax = hcube[0][0].Length;
            int ymax = hcube[0][0][0].Length;
            var newm = new char[wmax][][][];
            active = 0;

            for (int w = 0; w < wmax; w++)
            {
                var hslice = new char[zmax][][];

                for (int z = 0; z < zmax; z++)
                {
                    var mslice = new char[xmax][];
                    for (int x = 0; x < xmax; x++)
                    {
                        mslice[x] = new char[ymax];

                        for (int y = 0; y < ymax; y++)
                        {
                            var neighbo = dirs
                                .Select(o => (z + o.Item1, x + o.Item2, y + o.Item3, w + o.Item4))
                                .Where(o => 0 <= o.Item1 && o.Item1 < zmax
                                    && 0 <= o.Item2 && o.Item2 < xmax
                                    && 0 <= o.Item3 && o.Item3 < ymax
                                    && 0 <= o.Item4 && o.Item4 < wmax
                                );

                            var cntActive = neighbo.Where(o => hcube[o.Item4][o.Item1][o.Item2][o.Item3] == '#').Count();

                            var val = hcube[w][z][x][y];
                            if (val == '#' && cntActive != 2 && cntActive != 3)
                                val = '.';
                            if (val == '.' && cntActive == 3)
                                val = '#';
                            mslice[x][y] = val;
                            if (val == '#') active++;
                        }
                    }

                    hslice[z] = mslice;
                }
                newm[w] = hslice;
            }
            return newm;
        }

        public static char[][][][] Expand(char[][][][] hcube)
        {
            int wmax = hcube.Length + 2;
            int zmax = hcube[0].Length + 2;
            int xmax = hcube[0][0].Length + 2;
            int ymax = hcube[0][0][0].Length + 2;

            var newhcube = new char[wmax][][][];

            //expand
            for (int w = 0; w < wmax; w++)
            {
                var hslice = new char[zmax][][];

                for (int z = 0; z < zmax; z++)
                {
                    var mslice = new char[xmax][];
                    for (int x = 0; x < xmax; x++)
                    {
                        mslice[x] = Enumerable.Repeat('.', ymax).ToArray();
                    }
                    hslice[z] = mslice;

                }
                newhcube[w] = hslice;
            }

            //copy
            for (int w = 1; w < wmax - 1; w++)
                for (int z = 1; z < zmax - 1; z++)
                    for (int x = 1; x < xmax - 1; x++)
                        for (int y = 1; y < ymax - 1; y++)
                        {
                            char val = hcube[w - 1][z - 1][x - 1][y - 1];
                            newhcube[w][z][x][y] = val;
                        }
            return newhcube;
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
