using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace aoc.utils.tests
{
    public class StringsShould
    {

        [Fact]
        public void SplitInTwoValues()
        {
            var line = "a=b";
            var (first, second, _) = line.Split('=');
            Assert.Equal("a", first);
            Assert.Equal("b", second);
        }

        [Fact]
        public void SplitInOneValueAndTheRest()
        {
            var line = "a=b";
            var (first, _) = line.Split('=');
            Assert.Equal("a", first);
        }

        [Fact]
        public void SplitInTwoValues_WhenMoreThanTwoValues()
        {
            var line = "a:b:c:d";
            var (first, second, _) = line.Split(':');
            Assert.Equal("a", first);
            Assert.Equal("b", second);
        }

        [Fact]
        public void SplitInTwoValues_WhenOnlyOneValue()
        {
            var line = "a";

            Assert.Throws<Exception>(() =>
            {
                var (first, second, _) = line.Split(':');
            });

        }
    }
}
