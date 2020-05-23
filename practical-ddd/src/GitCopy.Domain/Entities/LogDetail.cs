using GitCopy.Core.DomainObjects;
using System;

namespace GitCopy.Domain.Entities
{
    public class LogDetail : Entity
    {
        public Guid LogId { get; private set; }
        public string RepositoryName { get; private set; }
        public string Commits { get; private set; }

        public LogDetail(Guid logId, string repositoryName, string commits)
        {
            LogId = logId;
            RepositoryName = repositoryName;
            Commits = commits;

            Validate();
        }

        public override void Validate()
        {
            Validation.IsEquals(LogId, Guid.Empty, $"The field LogId value can't be empty.");
            Validation.IsEmpty(RepositoryName, $"The RepositoryName LogId value can't be empty.");
            Validation.IsEmpty(Commits, $"The field Commits value can't be empty.");
        }
    }
}
