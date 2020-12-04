using System;
using System.IO;
using Xunit;
using System.Linq;

namespace day_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = File.ReadAllLines("input.txt").Select(o => int.Parse(o)).ToArray();

            Assert.Equal(514579, find(new int[] { 1721, 979, 366, 299, 675, 1456 }));
            Assert.Equal(1009899, find(numbers));

            Assert.Equal(241861950, find3(new int[] { 1721, 979, 366, 299, 675, 1456 }));
            Assert.Equal(44211152, find3(numbers));

        }

        static int find(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
                for (int j = i + 1; j < a.Length; j++)
                    if (a[i] + a[j] == 2020)
                    {
                        var ret = a[i] * a[j];
                        System.Console.WriteLine(ret);
                        return ret;
                    }

            throw new Exception("not found");
        }

        static int find3(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
                for (int j = i + 1; j < a.Length; j++)
                    for (int k = j + 1; k < a.Length; k++)
                        if (a[i] + a[j] + a[k] == 2020)
                        {
                            var ret = a[i] * a[j] * a[k];
                            System.Console.WriteLine(ret);
                            return ret;
                        }
            throw new Exception("not found");
        }
    }
}
