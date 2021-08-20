using DigitalMenu.DataAccess.Contexts;
using DigitalMenu.DataAccess.Settings;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;

namespace DigitalMenu.DataAccess.Tests.Contexts
{
    public class DigitalMenuContextTests
    {
        [Test]
        public void CtorShouldWithInvalidArgumentShouldThrowException()
        {
            FluentActions.Invoking(() => new DigitalMenuContext(null)).Should().ThrowExactly<ArgumentNullException>().WithMessage("*setting*");
            FluentActions.Invoking(() => new DigitalMenuContext(Mock.Of<IOptions<MongoDbSettings>>())).Should().ThrowExactly<ArgumentNullException>().WithMessage("*setting*");
        }
    }
}
