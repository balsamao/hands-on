using FluentAssertions;
using GitCopy.Domain.Entities;
using GitCopy.Domain.Enums;
using GitCopy.Infra.Data.Context;
using GitCopy.Infra.Repositories;
using LiteDB;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace GitCopy.Infra.Tests.Repositories
{
    public class LogRepositoryTests
    {
        private readonly Mock<IContextDB> _mockContext;
        private readonly Mock<ILiteCollection<Log>> _mockLogCollection;
        private readonly Mock<ILiteCollection<LogDetail>> _mockLogDetailCollection;

        private Log _log;
        private LogDetail _logDetail;

        public LogRepositoryTests() 
        {
            _mockContext = new Mock<IContextDB>();
            _mockLogCollection = new Mock<ILiteCollection<Log>>();
            _mockLogDetailCollection = new Mock<ILiteCollection<LogDetail>>();

            _log = new Log(DateTime.UtcNow, true, LogStatus.Running);
            _logDetail = new LogDetail(_log.Id, "todo-api", "New commit.");
        }

        [Fact]
        public async Task ShouldCreateNewLog()
        {
            _mockLogCollection.Setup(op => op.Insert(_log)).Returns(new BsonValue());

            _mockContext.Setup(c => c.GetCollection<Log>(typeof(Log).Name)).Returns(_mockLogCollection.Object);
            var logRepository = new LogRepository(_mockContext.Object);

            await logRepository.Insert(_log);

            _mockLogCollection.Verify(c => c.Insert(_log), Times.Once);
        }

        [Fact]
        public async void ShouldFailureCreateNewLogNull()
        {
            _log = null;

            var logRepository = new LogRepository(_mockContext.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => logRepository.Insert(_log));
        }

        [Fact]
        public async Task ShouldCreateNewLogDetail()
        {
            _mockLogDetailCollection.Setup(op => op.Insert(_logDetail)).Returns(new BsonValue());

            _mockContext.Setup(c => c.GetCollection<LogDetail>(typeof(LogDetail).Name)).Returns(_mockLogDetailCollection.Object);
            var logRepository = new LogRepository(_mockContext.Object);

            await logRepository.InsertDetail(_logDetail);

            _mockLogDetailCollection.Verify(c => c.Insert(_logDetail), Times.Once);
        }

        [Fact]
        public async void ShouldFailureCreateNewLogDetailNull()
        {
            _logDetail = null;

            var logRepository = new LogRepository(_mockContext.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => logRepository.InsertDetail(_logDetail));
        }

        [Fact]
        public async Task ShouldFindLog()
        {
            var filtro = new Mock<Expression<Func<Log, bool>>>();
            var mockResults = new List<Log> { new Log(DateTime.UtcNow, true, LogStatus.Running) };
            _mockLogCollection.Setup(op => op.Find(It.IsAny<Expression<Func<Log, bool>>>(), 0, int.MaxValue)).Returns(mockResults.AsEnumerable());

            _mockContext.Setup(c => c.GetCollection<Log>(typeof(Log).Name)).Returns(_mockLogCollection.Object);

            var logRepository = new LogRepository(_mockContext.Object);

            var result = await logRepository.GetAll(p => p.Id != Guid.Empty);

            result.Should().NotBeNull();
            result.Should().NotBeEmpty();

            _mockLogCollection.Verify(lnq => lnq.Find(It.IsAny<Expression<Func<Log, bool>>>(), 0, int.MaxValue), Times.Once);
        }
    }
}
