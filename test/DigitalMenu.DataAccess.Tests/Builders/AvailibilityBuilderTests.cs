using DigitalMenu.DataAccess.Builders;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace DigitalMenu.DataAccess.Tests.Builders
{
    public class AvailibilityBuilderTests
    {
        [TestCase(DayOfWeek.Monday)]
        [TestCase(DayOfWeek.Tuesday)]
        [TestCase(DayOfWeek.Wednesday)]
        [TestCase(DayOfWeek.Saturday)]
        [TestCase(DayOfWeek.Sunday)]
        [TestCase(DayOfWeek.Friday)]
        public void AddDayShouldCreateAvailibilityWithSingleDay(DayOfWeek dayOfWeek)
        {
            var builder = new AvailibilityBuilder().AddDay(dayOfWeek);
            var availibility = builder.Create();

            availibility.DaysOfWeek.Should().ContainSingle(d => d == dayOfWeek);
        }

        [TestCase(DayOfWeek.Monday)]
        [TestCase(DayOfWeek.Tuesday)]
        [TestCase(DayOfWeek.Sunday)]
        [TestCase(DayOfWeek.Friday)]
        public void AddDayMultipleSameShouldOnlyInsertSingleDay(DayOfWeek dayOfWeek)
        {
            var builder = new AvailibilityBuilder()
                .AddDay(dayOfWeek)
                .AddDay(dayOfWeek)
                .AddDay(dayOfWeek);
            var availibility = builder.Create();

            availibility.DaysOfWeek.Should().HaveCount(1).And.ContainSingle(d => d == dayOfWeek);
        }

        [Test]
        public void AddWeekdaysShouldAddMondayToFriday()
        {
            var builder = new AvailibilityBuilder().AddWeekdays();
            var availibility = builder.Create();

            availibility.DaysOfWeek.Should().HaveCount(5).And.OnlyContain(d => d >= DayOfWeek.Monday && d <= DayOfWeek.Friday);
        }

        [Test]
        public void AddWeekendsShouldAddSaturdayAndSunday()
        {
            var builder = new AvailibilityBuilder().AddWeekends();
            var availibility = builder.Create();

            availibility.DaysOfWeek.Should().Contain(DayOfWeek.Saturday).And.Contain(DayOfWeek.Sunday);
        }

        [Test]
        public void AddAllDaysOfWeekShouldHaveCountSeven()
        {
            var builder = new AvailibilityBuilder().AddAllDaysOfWeek();
            var availibility = builder.Create();

            availibility.DaysOfWeek.Should().HaveCount(7);
        }

        [Test]
        public void AddDaysShouldHandleMultipleSameValues()
        {
            var builder = new AvailibilityBuilder()
                .AddDays(DayOfWeek.Monday, DayOfWeek.Monday, DayOfWeek.Tuesday)
                .AddDays(DayOfWeek.Tuesday)
                .AddDays();
            var availibility = builder.Create();

            availibility.DaysOfWeek.Should().NotBeEmpty();
        }

        [Test]
        public void AddTimeStartEndShouldAddTimeStartEnds()
        {
            var firstStart = new TimeSpan(10, 20, 30);
            var firstEnd = new TimeSpan(12, 30, 0);
            var secondStart = new TimeSpan(5, 0, 0);
            var secondEnd = new TimeSpan(11, 30, 0);

            var builder = new AvailibilityBuilder()
                .AddTimeStartEnd(firstStart, firstEnd)
                .AddTimeStartEnd(secondStart, secondEnd);
            var availibility = builder.Create();

            availibility.TimeStartEnds.Should().HaveCount(2).And
                .ContainSingle(t => t.Start == firstStart && t.End == firstEnd).And
                .ContainSingle(t => t.Start == secondStart && t.End == secondEnd);
        }

        [Test]
        public void AddAnythingShouldNotAlterCreatedAvailibility()
        {
            var builder = new AvailibilityBuilder()
                .AddDay(DayOfWeek.Monday)
                .AddTimeStartEnd(TimeSpan.FromHours(6), TimeSpan.FromHours(9));
            var firstAvailibility = builder.Create();

            builder.AddAllDaysOfWeek().AddTimeStartEnd(new TimeSpan(), TimeSpan.FromHours(20));
            var secondAvailibility = builder.Create();

            firstAvailibility.DaysOfWeek.Should().NotHaveSameCount(secondAvailibility.DaysOfWeek);
            firstAvailibility.TimeStartEnds.Should().NotHaveSameCount(secondAvailibility.TimeStartEnds);
        }
    }
}