using GitCopy.Core.DomainObjects;
using System;

namespace GitCopy.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
    }
}
