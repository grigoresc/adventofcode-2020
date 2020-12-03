using System;
using System.IO;
using Xunit;
using System.Linq;

namespace day_3
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
        static void Main(string[] args)
        {


            // Compute(readmatrix(File.ReadAllLines("sample.txt")));//7
            // Compute(readmatrix(File.ReadAllLines("input.txt")));//292
            Compute2(readmatrix(File.ReadAllLines("sample.txt")));//336
            Compute2(readmatrix(File.ReadAllLines("input.txt")));//9354744432

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
