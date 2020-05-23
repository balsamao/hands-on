using FluentAssertions;
using GitCopy.Core.DomainObjects;
using GitCopy.Domain.Entities;
using GitCopy.Domain.Enums;
using System;
using Xunit;

namespace GitCopy.Domain.Tests
{
    public class LogTests
    {
        [Fact]
        public void ShouldRequireMinValidDateStart()
        {
            var domainException = Assert.Throws<DomainException>(() =>
            new Log(DateTime.UtcNow.AddDays(-1), false, LogStatus.Scheduled));

            domainException.Should().NotBeNull();
            domainException.Message.Should().NotBeNullOrEmpty();
            domainException.Message.Should().Be($"The field DateStart value can't be less than {DateTime.UtcNow:yyyy-MM-dd}.");
        }

        [Fact]
        public void ShouldRequireValidStatus()
        {
            var domainException = Assert.Throws<DomainException>(() =>
            new Log(DateTime.UtcNow.Date, false, (LogStatus)3));

            domainException.Should().NotBeNull();
            domainException.Message.Should().NotBeNullOrEmpty();
            domainException.Message.Should().Be($"The field Status value is invalid.");
        }
    }
}
