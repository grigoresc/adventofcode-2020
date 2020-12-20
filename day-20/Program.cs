using System.IO;
using System.Linq;
using Xunit;
using aoc.utils;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

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
            Solve2(File.ReadAllText("day-20.input.txt"));
        }

        public class Pic
        {
            public char[][] M { get; private set; }
            public string[] Edges { get; private set; }
            public int Number { get; private set; }

            public Pic(int number, char[][] ma)
            {
                Number = number;
                M = ma;
                var n = String.Join("", ma[0]);
                var e = String.Join("", ma.Select(line => line[line.Length - 1]));
                var s = String.Join("", ma[ma.Length - 1]);
                var w = String.Join("", ma.Select(line => line[0]));
                // System.Console.WriteLine();
                // System.Console.WriteLine(n);
                // System.Console.WriteLine(e);
                // System.Console.WriteLine(s);
                // System.Console.WriteLine(w);
                Edges = new[] { n, e, s, w };
            }

            public string Reverse(int edge)
            {
                return String.Join("", Edges[edge].Reverse());
            }

            public Pic Rotate()
            {
                var N = this.M.Length;
                char[][] newM = new char[N][];
                for (int i = 0; i < N; i++)
                    newM[i] = new char[N];//it works in this situatio, since it's a square..
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < N; j++)
                        newM[j][N - i - 1] = M[i][j];

                var ret = new Pic(this.Number, newM);
                return ret;
            }

            public Pic Flip()
            {
                var N = this.M.Length;
                //todo rotate here!
                char[][] newM = new char[N][];
                for (int i = 0; i < N; i++)
                    newM[i] = new char[N];//it works in this situatio, since it's a square..
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < N; j++)
                        newM[j][i] = M[i][j];

                var ret = new Pic(this.Number, newM);
                return ret;
            }

            public Pic Copy()
            {
                var N = this.M.Length;
                //todo rotate here!
                char[][] newM = new char[N][];
                for (int i = 0; i < N; i++)
                    newM[i] = new char[N];//it works in this situatio, since it's a square..
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < N; j++)
                        newM[i][j] = M[i][j];

                var ret = new Pic(this.Number, newM);
                return ret;
            }
        }

        public struct Piece
        {
            public int Number;
            // public bool Flipped;
            // public int Rotated;//0,1,2,3
        }


        private static long Solve2(string input)
        {
            long ret = 0L;

            var inputs = input.Split(Environment.NewLine + Environment.NewLine);

            var pics = new Dictionary<int, Pic>();

            Read(inputs, pics);

            var len = (int)Math.Sqrt(pics.Count());
            System.Console.WriteLine(len);

            Pic[,] puzzle = new Pic[len, len];
            Dictionary<int, Pic> inner;
            var cnt = 0;
            FindPossibleMatches(pics, out ret, out inner, puzzle, cnt++);
            System.Console.WriteLine("sln1=" + ret);
            while (inner.Count() > 0)
                FindPossibleMatches(inner, out ret, out inner, puzzle, cnt++);

            var noBordersImage = CombineAndRemoveBorders(puzzle);
            // Show(puzzle);

            var newM = readmatrix(noBordersImage.ToArray());
            var newPic = new Pic(-1, newM);

            System.Console.WriteLine("\nreduced image");
            newPic.M.ShowMatrix();

            newPic = FillPattern(newPic);

            ret = newPic.M.Sum(line => line.Where(c => c == '#').Count());


            // System.Console.WriteLine("\nimage with patterns");
            // newPic.M.ShowMatrix();
            Console.WriteLine(ret);
            return ret;
        }

        private static char[][] GetPattern()
        {
            var p =
                "                  # " + Environment.NewLine +
                "#    ##    ##    ###" + Environment.NewLine +
                " #  #  #  #  #  #   ";// + Environment.NewLine;
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
            pattern.ShowMatrix();
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
            pattern.ShowMatrix(title: "pattern");

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

            f.M.ShowMatrix(title: "with patterns");
            return f;
        }

        private static List<String> CombineAndRemoveBorders(Pic[,] puzzle)
        {
            var lines = new List<String>();
            for (int i = 0; i < puzzle.GetLength(0); i++)
            {
                // Console.Write(" | ");
                for (int j = 0; j < puzzle.GetLength(1); j++)
                {
                    // Console.Write(puzzle[i, j]?.Number + " | ");
                }
                // System.Console.WriteLine();

                for (int line = 1; line < puzzle[0, 0].M.Length - 1; line++)
                {
                    var l = "";
                    // Console.Write(" | ");
                    for (int j = 0; j < puzzle.GetLength(1); j++)
                    {
                        puzzle[i, j]?.M.ShowMatrix(line);
                        var p = puzzle[i, j].M;

                        for (var c = 1; c < puzzle[0, 0].M.Length - 1; c++)
                            l += p[line][c];

                        // Console.Write(" | ");

                        // Console.Write(puzzle[i, j]?.Number + ":");
                    }
                    lines.Add(l);
                    // System.Console.WriteLine("");
                }
                // System.Console.WriteLine("");

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
                    // System.Console.WriteLine($"match with {mpic.Number} on {r} and {mr}");
                    if (match)
                        throw new Exception("already match!");
                    return current;
                    // Pic found = new Pic{Number=mpic.Numbe,r}
                }
                else if (pic.Edges[edge] == reverse.Edges[comparingEdge])
                {
                    if (match)
                        throw new Exception("already match!");

                    return reverse;
                    // System.Console.WriteLine($"match with {mpic.Number} on {r} and {mr}/R");
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
                    // break;
                }                //this happens only on the sample and not on the real input
                else
                    throw new Exception("shouldnt happen!");
                // puzzle[BEGIN,END-1]=pics.
                return;
            }

            foreach (var (n, pic) in pics)
            {
                links[pic.Number] = new List<int>();
                // System.Console.WriteLine($"pic={pic.Number}");
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
                    // System.Console.WriteLine($"{pic.Number}");
                }
                else if (cnt == 3)
                {
                    // System.Console.WriteLine("EDGE FOUND" + pic.Number);
                    edges.Add(pic.Number, pic);

                }
                else if (cnt == 4)
                {
                    inner.Add(pic.Number, pic);
                }
                else
                    throw new Exception("shouldnt reach here!");
            }
            System.Console.WriteLine($"c/e/i={corners.Count()}:{edges.Count()}:{inner.Count()}");

            //fill the puzzle
            // if (iteration == 0)
            // {

            //top,left corner
            // if (iteration == 0)
            // {
            //     System.Console.WriteLine($"will take for corner {corners.ElementAt(0).Key}");
            //     corners.ElementAt(0).Value.M.ShowMatrix();
            //     puzzle[iteration, iteration] = corners.ElementAt(0).Value;
            // }
            // else
            // {
            //     var upper = puzzle[iteration - 1, iteration];




            var usedCorners = new List<int>();
            var usedEdges = new List<int>();

            //top left corner
            // Show(corners, "corner");
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
                    // Show(puzzle);
                    // System.Console.WriteLine(puzzle[BEGIN, BEGIN].Number);

                    // corners.Remove(puzzle[iteration, iteration].Number);

                }

            //top edges
            System.Console.WriteLine("top edges");
            for (var column = BEGIN + 1; column < END - 1; column++)
            {
                System.Console.WriteLine($"top edges column={column}");
                // var found = false;
                foreach (var (ne, edge) in edges)
                    if (!usedEdges.Contains(edge.Number))
                    {

                        var f = TranformToMatchEdge(puzzle[BEGIN, column - 1], edge, 1, out var cedge);
                        if (f != null)
                        {
                            puzzle[BEGIN, column] = f;
                            usedEdges.Add(edge.Number);
                            // found = true;
                            break;
                        }
                    }
                // if (found)
                // break;
            }

            // Show(puzzle);
            System.Console.WriteLine("corner top right");
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

            // Show(corners);

            // Show(puzzle);
            //right edges
            System.Console.WriteLine("right edges");
            for (var row = BEGIN + 1; row < END - 1; row++)
            {
                // var found = false;
                foreach (var (ne, edge) in edges)
                    if (!usedEdges.Contains(edge.Number))
                    {
                        var f = TranformToMatchEdge(puzzle[row - 1, END - 1], edge, 2, out var cedge);
                        if (f != null)
                        {
                            puzzle[row, END - 1] = f;
                            usedEdges.Add(edge.Number);
                            // found = true;
                            break;
                        }
                    }
                // if (found)
                // break;
            }

            // System.Console.WriteLine("try find bottom right");
            // puzzle[END - 1 - 1, END - 1].M.ShowMatrix();
            //corner bottom right
            System.Console.WriteLine("corner bottom right");
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
            System.Console.WriteLine("left edges");
            for (var row = BEGIN + 1; row < END - 1; row++)
            {
                // var found = false;
                foreach (var (ne, edge) in edges)
                    if (!usedEdges.Contains(edge.Number))
                    {
                        var f = TranformToMatchEdge(puzzle[row - 1, BEGIN], edge, 2, out var cedge);
                        if (f != null)
                        {
                            puzzle[row, BEGIN] = f;
                            usedEdges.Add(edge.Number);
                            // found = true;
                            break;
                        }
                    }
                // if (found)
                // break;
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
                // var found = false;

                foreach (var (ne, edge) in edges)
                    if (!usedEdges.Contains(edge.Number))
                    {
                        // System.Console.WriteLine();
                        // System.Console.WriteLine($"COMPARE {edge.Number}");
                        // puzzle[END - 1, column - 1].M.ShowMatrix();
                        // System.Console.WriteLine();
                        // edge.M.ShowMatrix();

                        var f = TranformToMatchEdge(puzzle[END - 1, column - 1], edge, 1, out var cedge);
                        if (f != null)
                        {
                            puzzle[END - 1, column] = f;
                            usedEdges.Add(edge.Number);
                            // found = true;
                            break;
                        }
                    }
                // if (found)
                // break;
            }
        }

        private static void Show(Dictionary<int, Pic> items, string msg)
        {
            System.Console.WriteLine(msg);
            foreach (var p in items)
            {
                System.Console.WriteLine(p.Value.Number);
                p.Value.M.ShowMatrix();
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
                // foreach (var rows in puzzle)
                // foreach (var columns in rows)
                {
                    Console.Write(puzzle[i, j]?.Number + " | ");
                }
                System.Console.WriteLine();

                for (int line = 0; line < puzzle[0, 0].M.Length; line++)
                {

                    Console.Write(" | ");
                    for (int j = 0; j < puzzle.GetLength(1); j++)
                    // foreach (var rows in puzzle)
                    // foreach (var columns in rows)
                    {
                        puzzle[i, j]?.M.ShowMatrix(line);
                        Console.Write(" | ");

                        // Console.Write(puzzle[i, j]?.Number + ":");
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
                // System.Console.WriteLine(picno);
                // ma.ShowMatrix();
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
            // matrix.ShowMatrix();
            return matrix;
        }
    }

    internal struct NewStruct
    {
        public int i;
        public int j;

        public NewStruct(int i, int j)
        {
            this.i = i;
            this.j = j;
        }

        public override bool Equals(object obj)
        {
            return obj is NewStruct other &&
                   i == other.i &&
                   j == other.j;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(i, j);
        }

        public void Deconstruct(out int i, out int j)
        {
            i = this.i;
            j = this.j;
        }

        public static implicit operator (int i, int j)(NewStruct value)
        {
            return (value.i, value.j);
        }

        public static implicit operator NewStruct((int i, int j) value)
        {
            return new NewStruct(value.i, value.j);
        }
    }
}

public static class Ex
{
    public static IEnumerable<IEnumerable<T>> DifferentCombinations<T>(this IEnumerable<T> elements, int k)
    {
        return k == 0 ? new[] { new T[0] } :
          elements.SelectMany((e, i) =>
            elements.Skip(i + 1).DifferentCombinations(k - 1).Select(c => (new[] { e }).Concat(c)));
    }
}