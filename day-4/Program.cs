using System;
using System.IO;
using Xunit;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_4
{
    class Program
    {

        static void Main(string[] args)
        {
            Assert.Equal(2, Solve1(File.ReadAllText("sample.txt")));
            Assert.Equal(245, Solve1(File.ReadAllText("input.txt")));

            Assert.Equal(0, Solve2(File.ReadAllText("sample2.txt")));
            Assert.Equal(133, Solve2(File.ReadAllText("input.txt")));
        }

        private static int Solve1(string str)
        {
            var ps = str.Split(Environment.NewLine + Environment.NewLine);
            var cnt = 0;
            foreach (var p in ps)
            {
                var vals = p.Replace(Environment.NewLine, " ").Split(" ").Select(s =>
                {
                    var kv = s.Split(":");
                    return kv[0];
                });

                var req = new[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
                var ok = true;
                foreach (var r in req)
                    if (!vals.Contains(r)) ok = false;

                if (ok)
                    cnt++;
            }
            System.Console.WriteLine(cnt);
            return cnt;
        }

        private static int Solve2(string str)
        {
            var ps = str.Split(Environment.NewLine + Environment.NewLine);
            var cnt = 0;
            foreach (var p in ps)
            {
                var vals = p.Replace(Environment.NewLine, " ").Split(" ").Select(s =>
                    {
                        var kv = s.Split(":");
                        return new Tuple<string, string>(kv[0], kv[1]);
                    }
                );

                var req = new[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
                var oktotal = true;
                foreach (var r in req)
                {
                    var ok = true;
                    var pair = vals.SingleOrDefault(o => o.Item1 == r);

                    if (pair == null) ok = false;
                    else
                    {
                        var value = pair.Item2;
                        switch (r)
                        {
                            case "byr":
                                ok = findrange(value, 1920, 2002);
                                break;
                            case "iyr":
                                ok = findrange(value, 2010, 2020);
                                break;
                            case "eyr":
                                ok = findrange(value, 2020, 2030);
                                break;
                            case "hgt":
                                if (matches(value, "^[0-9]{3,3}cm$"))
                                {
                                    ok = findrange(value.Replace("cm", ""), 150, 193);
                                }
                                else
                                if (matches(value, "^([0-9]{2,2}in)$"))
                                {
                                    ok = findrange(value.Replace("in", ""), 59, 76);
                                }
                                else
                                {
                                    ok = false;
                                }
                                break;
                            case "hcl":
                                ok = matches(value, "^#[0-9a-f]{6,6}$");
                                break;
                            case "ecl":
                                ok = matches(value, "^(amb|blu|brn|gry|grn|hzl|oth)$");
                                break;
                            case "pid":
                                ok = matches(value, "^[0-9]{9,9}$");
                                break;
                        }
                        // System.Console.WriteLine($"{r}:{value} {ok}");
                    }
                    oktotal = oktotal && ok;
                }
                // System.Console.WriteLine(oktotal);
                if (oktotal)
                    cnt++;
            }
            System.Console.WriteLine(cnt);
            return cnt;
        }

        private static bool matches(string value, string reg)
        {
            Regex rx = new Regex(reg);
            return rx.Match(value).Success;
        }

        private static bool findrange(string value, int min, int max)
        {
            if (!int.TryParse(value, out var byr))
                return false;
            return min <= byr && byr <= max;
        }

    }
}
