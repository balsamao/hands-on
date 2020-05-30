using System;

namespace GitCopy.Application.ViewModels
{
    public class LogDetailViewModel
    {
        public Guid LogId { get; set; }
        public string RepositoryName { get; set; }
        public string Commits { get; private set; }
    }
}
