using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace aoc.utils
{
    public static class Log
    {
        public static void WriteLine<T>(this IList<T> list)
        {
            Console.WriteLine(string.Join(", ", list.ToArray()));
        }
        public static void WriteLine<T>(this T[] array)
        {
            Console.WriteLine(string.Join(", ", array));
        }
        public static void WriteLines<T>(this T[] array)
        {
            for (int i = 0; i < array.Length; i++)
                Console.WriteLine(array[i]);
        }
        public static void WriteLine<T>(this T[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
                Console.WriteLine(string.Join(", ", matrix[i]));
        }
    }
}
