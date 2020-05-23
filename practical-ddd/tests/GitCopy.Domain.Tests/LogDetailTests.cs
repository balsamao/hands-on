using FluentAssertions;
using GitCopy.Core.DomainObjects;
using GitCopy.Domain.Entities;
using System;
using Xunit;

namespace GitCopy.Domain.Tests
{
    public class LogDetailTests
    {
        [Fact]
        public void ShouldRequireValidLogId()
        {
            var domainException = Assert.Throws<DomainException>(() =>
            new LogDetail(Guid.Empty, "", ""));

            domainException.Should().NotBeNull();
            domainException.Message.Should().NotBeNullOrEmpty();
            domainException.Message.Should().Be($"The field LogId value can't be empty.");
        }

        [Fact]
        public void ShouldRequireValidRepositoryName()
        {
            var domainException = Assert.Throws<DomainException>(() =>
            new LogDetail(Guid.NewGuid(), "", ""));

            domainException.Should().NotBeNull();
            domainException.Message.Should().NotBeNullOrEmpty();
            domainException.Message.Should().Be($"The RepositoryName LogId value can't be empty.");
        }

        [Fact]
        public void ShouldRequireValidCommits()
        {
            var domainException = Assert.Throws<DomainException>(() =>
            new LogDetail(Guid.NewGuid(), "todo-api", ""));

            domainException.Should().NotBeNull();
            domainException.Message.Should().NotBeNullOrEmpty();
            domainException.Message.Should().Be($"The field Commits value can't be empty.");
        }
    }
}
