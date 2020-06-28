using FluentAssertions;
using GitCopy.Domain.Entities;
using GitCopy.Infra.Data.Context;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.IO;
using Xunit;

namespace GitCopy.Infra.Tests.Data
{
    public class ContextDBTests
    {
        private readonly Mock<IOptions<SettingsDB>> _mockOptions;
        private readonly string _path;

        public ContextDBTests()
        {
            _mockOptions = new Mock<IOptions<SettingsDB>>();
            _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tests");

            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);
        }

        [Fact]
        public void ShouldCreateContextDBSuccess()
        {
            var settingsDB = new SettingsDB()
            {
                Connection = _path,
                DatabaseName = "TestDB_1"
            };

            _mockOptions.Setup(s => s.Value).Returns(settingsDB);

            var context = new ContextDB(_mockOptions.Object);

            context.Should().NotBeNull();
        }

        [Fact]
        public void ShouldFailureGetCollectionNameEmpty()
        {
            var settingsDB = new SettingsDB()
            {
                Connection = _path,
                DatabaseName = "TestDB_2"
            };

            _mockOptions.Setup(s => s.Value).Returns(settingsDB);

            var context = new ContextDB(_mockOptions.Object);
            var collection = context.GetCollection<Log>("");

            context.Should().NotBeNull();
            collection.Should().BeNull();
        }

        [Fact]
        public void ShouldGetCollectionValidName()
        {
            var settingsDB = new SettingsDB()
            {
                Connection = _path,
                DatabaseName = "TestDB_3"
            };

            _mockOptions.Setup(s => s.Value).Returns(settingsDB);

            var context = new ContextDB(_mockOptions.Object);
            var collection = context.GetCollection<Log>(typeof(Log).Name);

            context.Should().NotBeNull();
            collection.Should().NotBeNull();
        }
    }
}
