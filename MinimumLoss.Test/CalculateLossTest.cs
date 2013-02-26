using System.Linq;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace MinimumLoss
{
    public class CalculateLossTest
    {
        
        [Theory]
        [InlineData(new[] { 3 }, 6, 6)]
        //[InlineData(new[] { 3 }, 12, 12)]
        //[InlineData(new[] { 3 }, 24, 24)]
        //[InlineData(new[] { 8 }, 20, 24)]
        public void Should_be_solve_the_easiest_possible_calculation(int[] values, int wanted, int expectedN)
        {
            var calc = new LossCalculator(values, wanted);

            Match bestMatch = calc.BestMatch();

            bestMatch.TotalValue.Should().Be(expectedN);

        }

        [Fact]
        public void Should_find_an_easy_way_with_2_values()
        {
            var calc = new LossCalculator(new int[] {3, 2, 1}, 5);

            var bestMatch = calc.BestMatch();

            bestMatch.TotalValue.Should().Be(5);
        }

        [Fact]
        public void Should_find_an_easy_with_the_first_and_the_last_value()
        {
            var calc = new LossCalculator(new int[] { 3, 2, 1 }, 4);

            var bestMatch = calc.BestMatch();

            bestMatch.TotalValue.Should().Be(4);
        }

        [Fact]
        public void Should_find_an_easy_way_the_the_2_last_values()
        {
            var calc = new LossCalculator(new int[] { 1337, 2, 1 }, 3);

            var bestMatch = calc.BestMatch();

            bestMatch.TotalValue.Should().Be(3);
        }

    }
}