using System;
using System.Collections.Generic;
using System.Text;

namespace aoc.utils
{
    public static class Matrix
    {
        //todo - need to redo this method, or extract the line writing from it
        public static void ShowMatrix(this char[][] img, int lmax, int cmax, int? onlyline = null)
        {
            for (int l = 0; l < lmax; l++)
                if (onlyline == null || onlyline.Value == l)
                {
                    for (int c = 0; c < cmax; c++)
                    {
                        System.Console.Write(img[l][c]);
                    }
                    if (onlyline == null)
                        Console.WriteLine();
                }
        }
        public static void ShowMatrix(this char[][] img, int? onlyline = null)
        {
            img.ShowMatrix(img.Length, img[0].Length, onlyline);
        }


        public static string ShowMatrix1(this char[][] img, int lmax, int cmax)
        {
            var ret = "";
            for (int l = 0; l < lmax; l++)
            {
                for (int c = 0; c < cmax; c++)
                {
                    ret += img[l][c];
                }
            }
            return ret;
        }

        public static string GetState(this char[][] img)
        {
            return img.ShowMatrix1(img.Length, img[0].Length);
        }
    }
}
