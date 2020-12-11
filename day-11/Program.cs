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
            matrix.ShowMatrix();
            return matrix;
        }

        static void Main(string[] args)
        {
            Console.WriteLine(Solve2(readmatrix(File.ReadAllLines("day-11.input.txt"))));
        }

        static List<Tuple<int, int>> getpos8(int li, int co)
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
            Console.WriteLine("change");

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
                        foreach (var p in getpos8(l, c))
                        {
                            if (0 <= p.Item1 && p.Item1 < lmax && 0 <= p.Item2 && p.Item2 < cmax)
                            {
                                if (img[p.Item1][p.Item2] != '#')
                                    cnt++;
                            }
                            else
                            {
                                cnt++;
                            }
                        }

                        //Console.WriteLine(cnt);
                        if (cnt == 8)
                            val = '#';
                    }
                    else if (val == '#')
                    {
                        var cnt = 0;
                        foreach (var p in getpos8(l, c))
                        {
                            if (0 <= p.Item1 && p.Item1 < lmax && 0 <= p.Item2 && p.Item2 < cmax)
                            {
                                if (img[p.Item1][p.Item2] == '#')
                                    cnt++;
                            }
                        }
                        if (cnt >= 4)
                            val = 'L';
                    }
                    newimg[l][c] = val;

                    //System.Console.Write(img[l][c]);
                }
                //Console.WriteLine();
            }
            //newimg.ShowMatrix();
            //Console.Read();
            return newimg;
        }


        public static char[][] CloneAndChange2(char[][] img)
        {
            //Console.WriteLine("change");

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
                        foreach (var p in getpos8(l, c))
                        {
                            var moved = false;
                            var nl = l;
                            var nc = c;
                            nl += p.Item1;
                            nc += p.Item2;

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

                                nl += p.Item1;
                                nc += p.Item2;

                            }
                        }

                        //Console.WriteLine(cnt);
                        if (cnt == 0)
                            val = '#';
                    }
                    else if (val == '#')
                    {
                        var cnt = 0;
                        foreach (var p in getpos8(l, c))
                        {
                            var nl = l;
                            var nc = c;
                            nl += p.Item1;
                            nc += p.Item2;
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

                                nl += p.Item1;
                                nc += p.Item2;
                            }
                        }
                        if (cnt >= 5)
                            val = 'L';
                    }
                    newimg[l][c] = val;

                    //System.Console.Write(img[l][c]);
                }
                //Console.WriteLine();
            }
            //newimg.ShowMatrix();
            //Console.Read();
            return newimg;
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
