using System;
using System.Collections.Generic;
using System.Text;

namespace aoc.utils
{
    public static class Matrix
    {
        public static void ShowMatrix(this char[][] img, int lmax, int cmax)
        {
            for (int l = 0; l < lmax; l++)
            {
                for (int c = 0; c < cmax; c++)
                {
                    System.Console.Write(img[l][c]);
                }
                Console.WriteLine();
            }
        }
        public static void ShowMatrix(this char[][] img)
        {
            img.ShowMatrix(img.Length, img[0].Length);

        }
    }
}
