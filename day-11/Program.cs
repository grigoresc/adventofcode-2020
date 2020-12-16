using System.IO;
using System.Linq;
using Xunit;
using aoc.utils;
using System.Collections.Generic;
using System;

namespace day_11
{
    public class Program
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
            //matrix.ShowMatrix();
            return matrix;
        }

        static void Main(string[] args)
        {
            Solve();
        }

        public static void Solve()
        {
            Console.WriteLine(Solve1(readmatrix(File.ReadAllLines("day-11.input.txt"))));
            Console.WriteLine(Solve2(readmatrix(File.ReadAllLines("day-11.input.txt"))));
        }

        static List<Tuple<int, int>> get8dirs()
        {
            var r = new List<Tuple<int, int>>();
            r.Add(new Tuple<int, int>(-1, 0));
            r.Add(new Tuple<int, int>(+1, 0));
            r.Add(new Tuple<int, int>(0, -1));
            r.Add(new Tuple<int, int>(0, +1));
            r.Add(new Tuple<int, int>(-1, -1));
            r.Add(new Tuple<int, int>(-1, +1));
            r.Add(new Tuple<int, int>(+1, -1));
            r.Add(new Tuple<int, int>(+1, 1));
            return r;
        }

        public static char[][] CloneAndChange(char[][] img)
        {
            int lmax = img.Length;
            int cmax = img[0].Length;
            var newimg = new char[lmax][];
            for (int i = 0; i < lmax; i++)
                newimg[i] = new char[cmax];

            for (int l = 0; l < lmax; l++)
            {
                for (int c = 0; c < cmax; c++)
                {
                    char val = img[l][c];

                    if (val == 'L')
                    {
                        var cnt = 0;
                        foreach (var (dl, dc) in get8dirs())
                        {
                            var nl = l;
                            var nc = c;
                            nl += dl;
                            nc += dc;
                            if (0 <= nl && nl < lmax && 0 <= nc && nc < cmax)
                            {
                                if (img[nl][nc] != '#')
                                    cnt++;
                            }
                            else
                            {
                                cnt++;
                            }
                        }

                        if (cnt == 8)
                            val = '#';
                    }
                    else if (val == '#')
                    {
                        var cnt = 0;

                        foreach (var (dl, dc) in get8dirs())
                        {
                            var nl = l;
                            var nc = c;
                            nl += dl;
                            nc += dc;
                            if (0 <= nl && nl < lmax && 0 <= nc && nc < cmax)
                            {
                                if (img[nl][nc] == '#')
                                    cnt++;
                            }
                        }
                        if (cnt >= 4)
                            val = 'L';
                    }
                    newimg[l][c] = val;

                }
            }
            //newimg.ShowMatrix();
            //Console.Read();
            return newimg;
        }


        public static char[][] CloneAndChange2(char[][] img)
        {
            int lmax = img.Length;
            int cmax = img[0].Length;
            var newimg = new char[lmax][];
            for (int i = 0; i < lmax; i++)
                newimg[i] = new char[cmax];

            for (int l = 0; l < lmax; l++)
            {
                for (int c = 0; c < cmax; c++)
                {
                    char val = img[l][c];

                    if (val == 'L')
                    {
                        var cnt = 0;
                        foreach (var (dl, dc) in get8dirs())
                        {
                            var nl = l;
                            var nc = c;
                            nl += dl;
                            nc += dc;

                            while (0 <= nl && nl < lmax && 0 <= nc && nc < cmax)
                            {
                                if (img[nl][nc] == '#')
                                {
                                    cnt++;
                                    break;
                                }
                                else if (img[nl][nc] == 'L')
                                {
                                    break;
                                }

                                nl += dl;
                                nc += dc;
                            }
                        }

                        if (cnt == 0)
                            val = '#';
                    }
                    else if (val == '#')
                    {
                        var cnt = 0;
                        foreach (var (dl, dc) in get8dirs())
                        {
                            var nl = l;
                            var nc = c;
                            nl += dl;
                            nc += dc;
                            while (0 <= nl && nl < lmax && 0 <= nc && nc < cmax)
                            {
                                if (img[nl][nc] == '#')
                                {
                                    cnt++;
                                    break;
                                }
                                else if (img[nl][nc] == 'L')
                                {
                                    break;
                                }

                                nl += dl;
                                nc += dc;
                            }
                        }
                        if (cnt >= 5)
                            val = 'L';
                    }
                    newimg[l][c] = val;

                }
            }
            //newimg.ShowMatrix();
            //Console.Read();
            return newimg;
        }


        static long Solve1(char[][] matrix)
        {
            long ret = 0;
            var states = new List<string>();
            states.Add(matrix.GetState());

            for (; ; )
            {
                matrix = CloneAndChange(matrix);
                var state = matrix.GetState();
                if (!states.Contains(state))
                    states.Add(state);
                else
                    break;
            }
            ret = matrix.Sum(o => o.Count(c => c == '#'));

            return ret;
        }

        static long Solve2(char[][] matrix)
        {
            long ret = 0;
            var states = new List<string>();
            states.Add(matrix.GetState());

            for (; ; )
            {
                matrix = CloneAndChange2(matrix);
                var state = matrix.GetState();
                if (!states.Contains(state))
                    states.Add(state);
                else
                    break;
            }
            ret = matrix.Sum(o => o.Count(c => c == '#'));

            return ret;
        }

    }
}
