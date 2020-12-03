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


            Compute(readmatrix(File.ReadAllLines("sample.txt")));//7
            Compute(readmatrix(File.ReadAllLines("input.txt")));//292


        }

        static int Compute(char[][] matrix)
        {
            var ilen = matrix.Length;
            var jlen = matrix[0].Length;

            var i = 0;
            var j = 0;
            var cnt = 0;

            do
            {
                System.Console.WriteLine($"{i} {j}");
                i += 1;
                j += 3;
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
