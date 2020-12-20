using System.IO;
using System.Linq;
using Xunit;
using aoc.utils;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace day_20
{
    public class Program
    {
        static void Main(string[] args)
        {
            Solve();
        }

        public static void Solve()
        {
            Assert.Equal(new Tuple<long, long>(22878471088273, 1680), SolveBoth(File.ReadAllText("day-20.input.txt")));
        }

        private static Tuple<long, long> SolveBoth(string input)
        {
            long retProblem2 = 0L;
            long retProblem1 = 1L;

            var inputs = input.Split(Environment.NewLine + Environment.NewLine);

            var pics = new Dictionary<int, Pic>();

            Read(inputs, pics);

            var len = (int)Math.Sqrt(pics.Count());
            // System.Console.WriteLine(len);

            Pic[,] puzzle = new Pic[len, len];
            Dictionary<int, Pic> inner;
            var cnt = 0;
            FindPossibleMatches(pics, out retProblem1, out inner, puzzle, cnt++);

            while (inner.Count() > 0)
                FindPossibleMatches(inner, out var prod, out inner, puzzle, cnt++);

            var noBordersImage = CombineAndRemoveBorders(puzzle);
            // Show(puzzle);

            var newM = readmatrix(noBordersImage.ToArray());
            var newPic = new Pic(-1, newM);

            System.Console.WriteLine("\nreduced image");
            // newPic.M.ShowMatrix();

            newPic = FillPattern(newPic);
            newPic.M.ShowMatrix(title: "with monsters", applyColor: (c) =>
             {
                 //  Thread.Sleep(2);
                 if (c == 'o')
                 {
                     return (ConsoleColor.Yellow, ConsoleColor.DarkYellow);
                 }
                 else
                 {
                     return (ConsoleColor.DarkBlue, ConsoleColor.Cyan);
                 }
             });

            retProblem2 = newPic.M.Sum(line => line.Where(c => c == '#').Count());

            Console.WriteLine($"{retProblem1} {retProblem2}");
            return new Tuple<long, long>(retProblem1, retProblem2);
        }

        private static char[][] GetPattern()
        {
            var p =
                "                  # " + Environment.NewLine +
                "#    ##    ##    ###" + Environment.NewLine +
                " #  #  #  #  #  #   ";
            var pm = readmatrix(p.Split(Environment.NewLine));
            return pm;
        }

        private static void ApplyPattern(char[][] m, char[][] pattern, int i, int j)
        {
            for (var pi = 0; pi < pattern.Length; pi++)
                for (var pj = 0; pj < pattern[0].Length; pj++)
                {
                    var p = pattern[pi][pj];
                    var el = m[i + pi][j + pj];
                    if (p == '#')
                        m[i + pi][j + pj] = 'o';
                }
        }

        private static IEnumerable<KeyValuePair<int, int>> PatternPositions(char[][] m, char[][] pattern)
        {
            // pattern.ShowMatrix();
            for (var i = 0; i < m.Length - pattern.Length; i++)
                for (var j = 0; j < m[0].Length - pattern[0].Length; j++)
                {
                    var ispattern = true;
                    for (var pi = 0; pi < pattern.Length; pi++)
                        for (var pj = 0; pj < pattern[0].Length; pj++)
                        {
                            var p = pattern[pi][pj];
                            var el = m[i + pi][j + pj];
                            if (p == '#')
                            {
                                if (el != '#')
                                    ispattern = false;
                            }
                        }
                    if (ispattern)
                        yield return new KeyValuePair<int, int>(i, j);
                }
        }

        private static Pic FillPattern(Pic pic)
        {
            var pattern = GetPattern();
            // pattern.ShowMatrix(title: "pattern");

            var current = pic.Copy();
            Pic f = null;
            List<KeyValuePair<int, int>> places = null;

            for (int reps = 0; reps < 4; reps++)
            {
                places = PatternPositions(current.M, pattern).ToList();

                if (places.Count() == 0)
                {
                    var inverse = current.Flip();
                    places = PatternPositions(inverse.M, pattern).ToList();
                    if (places.Count() > 1)
                    {
                        f = inverse;
                        break;
                    }
                }
                else
                {
                    f = current;
                    break;
                }

                current = current.Rotate();
            }

            if (f != null)
            {
                foreach (var place in places)
                {
                    ApplyPattern(f.M, pattern, place.Key, place.Value);
                }
            }

            // f.M.ShowMatrix(title: "with patterns");
            return f;
        }

        private static List<String> CombineAndRemoveBorders(Pic[,] puzzle)
        {
            var lines = new List<String>();
            for (int i = 0; i < puzzle.GetLength(0); i++)
            {
                for (int line = 1; line < puzzle[0, 0].M.Length - 1; line++)
                {
                    var l = "";
                    for (int j = 0; j < puzzle.GetLength(1); j++)
                    {
                        // puzzle[i, j]?.M.ShowMatrix(line);
                        var p = puzzle[i, j].M;

                        for (var c = 1; c < puzzle[0, 0].M.Length - 1; c++)
                            l += p[line][c];
                    }
                    lines.Add(l);
                }
            }
            return lines;
        }

        static Pic Match(Pic pic, Pic mpic)
        {
            for (int r = 0; r < 4; r++)
            {
                var f = TranformToMatchEdge(pic, mpic, r, out var comparingEdge);
                if (f != null)
                    return f;
            }
            return null;
        }

        static Pic TransformSrcToMatch(int edge, Pic src, Pic mpic, int edge2, Pic mpic2)
        {
            var current = src.Copy();

            for (int r = 0; r < 4; r++)
            {
                if (TranformToMatchEdge(current, mpic, edge, out var comparingEdge) != null)
                    if (TranformToMatchEdge(current, mpic2, edge2, out var comparingEdge3) != null)
                        return current;

                var reverse = current.Flip();

                if (TranformToMatchEdge(reverse, mpic, edge, out var comparingEdge2) != null)
                    if (TranformToMatchEdge(reverse, mpic2, edge2, out var comparingedge3) != null)
                        return reverse;

                current = current.Rotate();
            }
            return null;
        }

        private static Pic TranformToMatchEdge(Pic pic, Pic mpic, int edge, out int comparingEdge)
        {
            var match = false;
            var current = mpic.Copy();

            if (edge == 0)
                comparingEdge = 2;
            else if (edge == 1)
                comparingEdge = 3;
            else if (edge == 2)
                comparingEdge = 0;
            else if (edge == 3)
                comparingEdge = 1;
            else
                throw new Exception("should not be here!");

            for (int rotation = 0; rotation < 4; rotation++)
            {
                var reverse = current.Flip();

                if (pic.Edges[edge] == current.Edges[comparingEdge])
                {
                    if (match)
                        throw new Exception("already match!");
                    return current;
                }
                else if (pic.Edges[edge] == reverse.Edges[comparingEdge])
                {
                    if (match)
                        throw new Exception("already match!");

                    return reverse;
                }

                current = current.Rotate();
            }
            return null;
        }

        static void FindPossibleMatches(Dictionary<int, Pic> pics, out long prod, out Dictionary<int, Pic> inner, Pic[,] puzzle, int iteration)
        {

            var N = puzzle.GetLength(0);
            var BEGIN = iteration;
            var END = N - iteration;

            System.Console.WriteLine($"N={N} BEGIN={BEGIN} END={END}");
            System.Console.WriteLine($"possible matches iteration {iteration}");

            prod = 1;
            var corners = new Dictionary<int, Pic>();
            var edges = new Dictionary<int, Pic>();
            inner = new Dictionary<int, Pic>();
            var links = new Dictionary<int, List<int>>();

            if (pics.Count() == 1)
            {
                var theonlyone = pics.ElementAt(0).Value;
                var f = TranformToMatchEdge(puzzle[BEGIN - 1, BEGIN], theonlyone, 2, out var cedge);
                if (f != null)
                {
                    puzzle[BEGIN, BEGIN] = f;
                }                //this happens only on the sample and not on the real input
                else
                    throw new Exception("shouldnt happen!");
                return;
            }

            foreach (var (n, pic) in pics)
            {
                links[pic.Number] = new List<int>();
                var cnt = 0;
                foreach (var (mn, mpic) in pics)
                    if (pic.Number != mpic.Number)
                    {
                        var ma = Match(pic, mpic);
                        if (ma != null)
                        {
                            links[pic.Number].Add(ma.Number);
                            cnt++;
                        }
                    }
                if (cnt == 2)
                {
                    corners.Add(pic.Number, pic);
                    prod *= pic.Number;
                }
                else if (cnt == 3)
                {
                    edges.Add(pic.Number, pic);
                }
                else if (cnt == 4)
                {
                    inner.Add(pic.Number, pic);
                }
                else
                    throw new Exception("shouldnt reach here!");
            }
            // System.Console.WriteLine($"c/e/i={corners.Count()}:{edges.Count()}:{inner.Count()}");

            var usedCorners = new List<int>();
            var usedEdges = new List<int>();

            //top left corner
            foreach (var (nctl, cTL) in corners)
                if (!usedCorners.Contains(cTL.Number))
                {
                    if (iteration > 0)
                    {
                        var f = TranformToMatchEdge(puzzle[BEGIN - 1, BEGIN], cTL, 2, out var cedge);
                        if (f != null)
                        {
                            puzzle[BEGIN, BEGIN] = f;
                            // break;
                        }
                    }
                    else
                    {
                        foreach (var (nel, eLeft) in edges)
                        {
                            foreach (var (ned, eDown) in edges)
                            {
                                var f = TransformSrcToMatch(1, cTL, eLeft, 2, eDown);
                                if (f != null)
                                {
                                    puzzle[BEGIN, BEGIN] = f.Flip();
                                    // System.Console.WriteLine("c=" + f.Number);
                                    break;
                                }
                            }

                        }
                    }

                    if (puzzle[BEGIN, BEGIN] != null)
                    {
                        usedCorners.Add(cTL.Number);
                        break;
                    }
                }

            //top edges
            // System.Console.WriteLine("top edges");
            for (var column = BEGIN + 1; column < END - 1; column++)
            {
                foreach (var (ne, edge) in edges)
                    if (!usedEdges.Contains(edge.Number))
                    {
                        var f = TranformToMatchEdge(puzzle[BEGIN, column - 1], edge, 1, out var cedge);
                        if (f != null)
                        {
                            puzzle[BEGIN, column] = f;
                            usedEdges.Add(edge.Number);
                            break;
                        }
                    }
            }

            // System.Console.WriteLine("corner top right");
            //corner top right 
            foreach (var (nctr, cTR) in corners)
                if (!usedCorners.Contains(cTR.Number))
                {
                    var f = TranformToMatchEdge(puzzle[BEGIN, END - 1 - 1], cTR, 1, out var cedge);
                    if (f != null)
                    {
                        puzzle[BEGIN, END - 1] = f;
                        usedCorners.Add(cTR.Number);
                        break;
                    }
                }
            //right edges
            // System.Console.WriteLine("right edges");
            for (var row = BEGIN + 1; row < END - 1; row++)
            {
                foreach (var (ne, edge) in edges)
                    if (!usedEdges.Contains(edge.Number))
                    {
                        var f = TranformToMatchEdge(puzzle[row - 1, END - 1], edge, 2, out var cedge);
                        if (f != null)
                        {
                            puzzle[row, END - 1] = f;
                            usedEdges.Add(edge.Number);
                            break;
                        }
                    }
            }

            //corner bottom right
            // System.Console.WriteLine("corner bottom right");
            foreach (var (ncbr, cBR) in corners)
                if (!usedCorners.Contains(cBR.Number))
                {
                    var f = TranformToMatchEdge(puzzle[END - 1 - 1, END - 1], cBR, 2, out var cedge);
                    if (f != null)
                    {
                        puzzle[END - 1, END - 1] = f;
                        usedCorners.Add(cBR.Number);
                        break;
                    }
                }

            //left edges
            // System.Console.WriteLine("left edges");
            for (var row = BEGIN + 1; row < END - 1; row++)
            {
                foreach (var (ne, edge) in edges)
                    if (!usedEdges.Contains(edge.Number))
                    {
                        var f = TranformToMatchEdge(puzzle[row - 1, BEGIN], edge, 2, out var cedge);
                        if (f != null)
                        {
                            puzzle[row, BEGIN] = f;
                            usedEdges.Add(edge.Number);
                            break;
                        }
                    }
            }

            //corner bottom left
            foreach (var (ncbl, cBL) in corners)
                if (!usedCorners.Contains(cBL.Number))
                {
                    var f = TranformToMatchEdge(puzzle[END - 1 - 1, BEGIN], cBL, 2, out var cedge);
                    if (f != null)
                    {
                        puzzle[END - 1, BEGIN] = f;
                        usedCorners.Add(cBL.Number);
                        break;
                    }
                }

            //bottom edge
            for (var column = BEGIN + 1; column < END - 1; column++)
            {
                foreach (var (ne, edge) in edges)
                    if (!usedEdges.Contains(edge.Number))
                    {

                        var f = TranformToMatchEdge(puzzle[END - 1, column - 1], edge, 1, out var cedge);
                        if (f != null)
                        {
                            puzzle[END - 1, column] = f;
                            usedEdges.Add(edge.Number);
                            break;
                        }
                    }
            }
        }

        private static void Show(Dictionary<int, Pic> items, string msg)
        {
            System.Console.WriteLine(msg);
            foreach (var p in items)
            {
                System.Console.WriteLine(p.Value.Number);
                // p.Value.M.ShowMatrix();
            }
            System.Console.WriteLine("");
        }

        static void Show(Pic[,] puzzle)
        {
            System.Console.WriteLine("puzzle:");
            for (int i = 0; i < puzzle.GetLength(0); i++)
            {
                Console.Write(" | ");
                for (int j = 0; j < puzzle.GetLength(1); j++)
                {
                    Console.Write(puzzle[i, j]?.Number + " | ");
                }
                System.Console.WriteLine();

                for (int line = 0; line < puzzle[0, 0].M.Length; line++)
                {

                    Console.Write(" | ");
                    for (int j = 0; j < puzzle.GetLength(1); j++)
                    {
                        // puzzle[i, j]?.M.ShowMatrix(line);
                        Console.Write(" | ");
                    }
                    System.Console.WriteLine("");
                }
                System.Console.WriteLine("");

            }
            return;
        }
        static void Read(string[] inputs, Dictionary<int, Pic> pics)
        {
            foreach (var m in inputs)
            {
                (var fi, var la) = m.Split(Environment.NewLine);
                var picno = int.Parse(fi.Replace("Tile ", "").Replace(":", ""));
                var ma = readmatrix(la.ToArray());
                pics.Add(picno, new Pic(picno, ma));
            }
        }

        static char[][] readmatrix(string[] input)
        {
            char[][] matrix = new char[input.Length][];
            for (int i = 0; i < input.Length; i++)
            {
                matrix[i] = new char[input[i].Length];
                for (int j = 0; j < input[i].Length; j++)
                    matrix[i][j] = input[i][j];
            }
            return matrix;
        }
    }

}
