using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace aoc.utils
{
    public static class Log
    {
        public static void WriteLine<T>(T m)
        {
            Debug.WriteLine("aoc:" + m);
        }
        public static void WriteLine<T>(this IList<T> list)
        {
            WriteLine(string.Join(", ", list.ToArray()));
        }
        public static void WriteLine<T>(this T[] array)
        {
            WriteLine(string.Join(", ", array));
        }
        public static void WriteLines<T>(this T[] array)
        {
            for (int i = 0; i < array.Length; i++)
                WriteLine(array[i]);
        }
        public static void WriteLine<T>(this T[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
                WriteLine(string.Join(", ", matrix[i]));
        }
    }
}
