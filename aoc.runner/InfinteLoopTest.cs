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
            var writer = new StreamWriter($"run.log", true);
            // Redirect standard output from the console to the output file.
            Console.SetOut(writer);
            writer.AutoFlush = true;

            Console.WriteLine($"RUN BEGIN {DateTime.Now}");
            day_9.Program.Solve();
            Console.WriteLine($"RUN END {DateTime.Now}");
        }
    }
}
