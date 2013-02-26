using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace MinimumLoss
{
    public class AmountTest
    {
        [Theory]
        [InlineData(1,3, 3)]
        [InlineData(2,3, 6)]
        [InlineData(3,4, 12)]
        public void Should_be_able_to_calculate_total_amount(int value, int n, int expected)
        {
            var amount = new Amount(value, n);
            amount.Total().Should().Be(expected);
        }

        [Fact]
        public void Should_be_able_to_create_amount()
        {
            var amount = new Amount(1337, 2);
            amount.N.Should().Be(2);
            amount.Value.Should().Be(1337);
        }
    }
}