using System.IO;
using System.Linq;
using Xunit;
using aoc.utils;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace day_21
{
    public class Program
    {

        static void Main(string[] args)
        {
            Assert.Equal((2287, "fntg,gtqfrp,xlvrggj,rlsr,xpbxbv,jtjtrd,fvjkp,zhszc"), Solve(File.ReadAllLines("day-21.input.txt")));
        }

        public static (int ret1, string ret2) Solve(string[] lines)
        {
            var foods = lines.Select(s =>
            {
                //mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
                var (lef, rig, _) = s.Replace("(", "").Replace(")", "").Split(" contains ");
                return new
                {
                    ings = lef.Split(" "),
                    ales = rig.Split(", ")
                };
            }).ToArray();
            var allale = CombineStrings(foods.Select(o => o.ales));

            var current = foods.Select(food =>
            {
                var ret = from ing in food.ings
                          from ale in food.ales
                          select new KeyValuePair<string, string[]>(ale, new string[] { ing });
                return ret;
            }).ToArray();

            string[] all = CombineStrings(foods.Select(f => f.ings)).ToArray();
            var knownFoodAlergens = new[] { new { food = "", alergen = "" } }.Skip(1);//todo anyother way of having this? (it's an anonymous type)
            var keepSearching = true;

            while (keepSearching)
            {
                var f = current.Aggregate(
                    new Dictionary<string, string[]>()
                    , (acc, comb) =>
                    {
                        //todo: group then uppper
                        var thisacc = comb
                            //dont use the food and alergen for which have a sln
                            .Where(c => !knownFoodAlergens.Any(o => o.alergen == c.Key))
                            .Where(c => !knownFoodAlergens.Any(o => c.Value.Contains(o.food)))
                            .GroupBy(o => o.Key, o => o.Value, (k, vals) =>
                        {
                            return new KeyValuePair<string, string[]>(k, CombineStrings(vals)./*Distinct().*/ToArray());
                        }).ToDictionary(x => x.Key, v => v.Value);

                        foreach (var co in thisacc)
                        {
                            if (!acc.ContainsKey(co.Key))
                                acc[co.Key] = co.Value;
                            else
                            {
                                acc[co.Key] = acc[co.Key].Intersect(co.Value).ToArray();
                            }
                        }

                        return acc;
                    });

                var found = f
                    .Where(o => o.Value.Length == 1)
                    .Select(o => new { food = o.Value.ElementAt(0), alergen = o.Key });

                if (found.Count() == 0)
                    keepSearching = false;


                knownFoodAlergens = knownFoodAlergens.Concat(found).ToArray();
            }

            var ingWithouthAlerg = all.Where(s => !knownFoodAlergens.Any(o => o.food == s));
            var ret1 = ingWithouthAlerg.Count();
            var ret2 = string.Join(",", knownFoodAlergens.OrderBy(o => o.alergen).Select(o => o.food));
            Console.WriteLine(ret1);
            Console.WriteLine(ret2);
            return (ret1, ret2);
        }

        private static IEnumerable<string> CombineStrings(IEnumerable<string[]> vals)
        {
            return vals.Aggregate(new string[0], (a, b) => { return a.Concat(b).ToArray(); });
        }
    }

}
