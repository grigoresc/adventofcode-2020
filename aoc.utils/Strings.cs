using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc.utils
{
    public static class Strings
    {
        public static void Deconstruct<T>(this IList<T> list, out T first, out IList<T> rest)
        {
            first = list.Count > 0 ? list[0] : throw new Exception("count is not >0");
            rest = list.Skip(1).ToList();
        }
        public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out IList<T> rest)
        {
            first = list.Count > 0 ? list[0] : throw new Exception("count is not >0");

            second = list.Count > 1 ? list[1] : throw new Exception("count is not >1");
            rest = list.Skip(2).ToList();
        }
    }
}
