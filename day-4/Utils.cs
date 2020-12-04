using System;
using System.Drawing;
using static System.String;
using System.Linq;

namespace day_4
{
    public static class Utils
    {


        public static void ShowMatrix(this char[][] img, int lmax, int cmax)
        {
            for (int l = 0; l < lmax; l++)
            {
                for (int c = 0; c < cmax; c++)
                {
                    System.Console.Write(img[l][c]);
                    // if (img[l][c] == '0')
                    //     Console.Write(' ');
                    // else
                    //     Console.Write('*');
                }
                Console.WriteLine();
            }
        }
        public static void ShowMatrix(this char[][] img)
        {
            img.ShowMatrix(img.Length, img[0].Length);

        }
        // public static void DrawMatrix(this char[][] img, int lmax, int cmax)
        // {
        //     using (var bmp = new Bitmap(cmax, lmax))
        //     using (var gr = Graphics.FromImage(bmp))
        //     {
        //         for (int l = 0; l < lmax; l++)
        //         {
        //             for (int c = 0; c < cmax; c++)
        //             {
        //                 bmp.SetPixel(c, l, img[l][c] == '1' ? Color.Black : Color.White);
        //             }
        //         }
        //         var path = "out.png";
        //         bmp.Save(path);
        //     }
        // }

        public static string Combine(this char[][] img)
        {
            return Join("", img.Select(o => Join("", o)));
        }
    }
}
