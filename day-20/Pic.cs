using System.Linq;
using System;

namespace day_20
{
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
            Edges = new[] { n, e, s, w };
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
}
