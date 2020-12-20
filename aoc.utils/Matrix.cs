using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace aoc.utils
{
    public static class Matrix
    {
        //todo - need to redo this method, or extract the line writing from it
        public static void ShowMatrix(this char[][] img, int lmax, int cmax, int? onlyline = null, string title = "", Func<char, (ConsoleColor, ConsoleColor)> applyColor = null)
        {
            if (title != "") System.Console.WriteLine(title);
            for (int l = 0; l < lmax; l++)
                if (onlyline == null || onlyline.Value == l)
                {
                    for (int c = 0; c < cmax; c++)
                    {
                        var prevf = Console.ForegroundColor;
                        var prevb = Console.BackgroundColor;
                        if (applyColor != null)
                        {
                            (var f, var b) = applyColor(img[l][c]);
                            Console.ForegroundColor = f;
                            Console.BackgroundColor = b;
                        }
                        System.Console.Write(img[l][c]);
                        Console.ForegroundColor = prevf;
                        Console.BackgroundColor = prevb;
                    }
                    if (onlyline == null)
                        Console.WriteLine();
                    Thread.Sleep(10);
                }
        }
        public static void ShowMatrix(this char[][] img, int? onlyline = null, string title = "", Func<char, (ConsoleColor, ConsoleColor)> applyColor = null)
        {
            img.ShowMatrix(img.Length, img[0].Length, onlyline, title, applyColor);
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
