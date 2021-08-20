using DigitalMenu.DataAccess.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalMenu.DataAccess.Tests.Entities
{
    public class TimeStartEndTests
    {
        [Test]
        public void CtorWithInvalidArgumentsShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new TimeStartEnd(TimeSpan.FromDays(2), TimeSpan.FromHours(8)))
                .Should().ThrowExactly<ArgumentException>().WithMessage("*start*");
            FluentActions.Invoking(() => new TimeStartEnd(TimeSpan.FromHours(8), TimeSpan.FromDays(2)))
                .Should().ThrowExactly<ArgumentException>().WithMessage("*end*");
        }

        [Test]
        public void CtorWithStartBiggerThanEndShouldSwapValues()
        {
            var smaller = TimeSpan.FromHours(0);
            var bigger = TimeSpan.FromHours(8);

            var classUnderTest = new TimeStartEnd(bigger, smaller);
            classUnderTest.Start.Should().Be(smaller);
            classUnderTest.End.Should().Be(bigger);
        }

        [Test]
        public void CtorWithStartSmallerThanEndShouldNotSwapValues()
        {
            var smaller = TimeSpan.FromHours(0);
            var bigger = TimeSpan.FromHours(8);

            var classUnderTest = new TimeStartEnd(smaller, bigger);
            classUnderTest.Start.Should().Be(smaller);
            classUnderTest.End.Should().Be(bigger);
        }
    }
}
