using System.IO;
using System.Linq;
using Xunit;
using aoc.utils;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace day_24
{
    public class Program
    {

        static void Main(string[] args)
        {
            Solve();
        }

        public static void Solve()
        {
            solve1(File.ReadAllLines("day-24.input.txt"));
        }

        class pos
        {
            public int e;
            public int ne;
            public int se;
        }

        static long solve1(string[] lines)
        {
            var ret = 0;

            var list = new Dictionary<string, int>();

            foreach (var l in lines)
            {
                var e = 0;
                var se = 0;
                var ne = 0;
                pos p = new pos();
                for (int i = 0; i < l.Length; i++)
                {
                    if (l[i] == 'w')
                    {
                        e--;
                    }
                    else if (l[i] == 'e')
                    {
                        e++;
                    }
                    else if (l[i] == 'n' && l[i + 1] == 'w')
                    {
                        se--;
                        i++;
                    }
                    else if (l[i] == 'n' && l[i + 1] == 'e')
                    {
                        ne++;
                        i++;
                    }
                    else if (l[i] == 's' && l[i + 1] == 'w')
                    {
                        ne--;
                        i++;
                    }
                    else if (l[i] == 's' && l[i + 1] == 'e')
                    {
                        se++;
                        i++;
                    }
                    else
                    {
                        Console.WriteLine("!!!!!");
                    }
                }

                Console.WriteLine($"{e}:{se}:{ne}->");
                //normalize (ne or se should be 0) and (ne or se should be positive)

                if (ne > 0 && se > 0)
                {
                    ne -= se;
                    e += se;
                    se = 0;
                }
                while (!((ne == 0 || se == 0) && (ne > 0 || se > 0)))
                {
                    Console.WriteLine($"..{e}:{se}:{ne}");
                    if (ne == 0 && se == 0)
                        break;
                    se++;
                    ne++;
                    e--;
                }
                //var m = Math.Min(Math.Abs(se), Math.Abs(ne));

                //if (m == Math.Abs(se))
                //{

                //    for (int c = 0; c < m; c++)
                //    {
                //        if (se > 0)
                //        {
                //            se--;
                //            ne--;
                //            e++;
                //        }
                //        else
                //        {
                //            se++;
                //            ne++;
                //            e--;
                //        }
                //    }

                //}
                //else
                //{
                //    for (int c = 0; c < m; c++)
                //    {
                //        if (ne > 0)
                //        {
                //            se--;
                //            ne--;
                //            e++;
                //        }
                //        else
                //        {
                //            se++;
                //            ne++;
                //            e--;
                //        }
                //    }
                //}
                //normalize ne or se must be positive

                Console.WriteLine($"       -->{e}:{se}:{ne}");
                string key = getkey(e, se, ne);
                if (!list.ContainsKey(key))
                {
                    list[key] = 1;

                }
                else
                {
                    list[key]++;
                }
                Console.WriteLine();

            }
            foreach (var (k, v) in list)
            {
                Console.WriteLine($"{k} is {v} times");
            }
            ret = list.Where(o => o.Value % 2 == 1).Count();
            Console.WriteLine(ret);
            return ret;
        }

        static string getkey(int e, int se, int ne)
        {
            return $"{e} {se} {ne}";
        }

    }

}
