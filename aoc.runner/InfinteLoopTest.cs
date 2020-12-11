using System;
using System.IO;
using Xunit;

namespace aoc.runner
{
    public class InfinteLoopTest
    {
        [Fact]
        public void Run()
        {
            var writer = new StreamWriter($"run.log");
            // Redirect standard output from the console to the output file.
            Console.SetOut(writer);
            writer.AutoFlush = true;

            Console.WriteLine($"RUN BEGIN {DateTime.Now}");
            day_11.Program.Solve();
            Console.WriteLine($"RUN END {DateTime.Now}");
        }
    }
}
