using GitCopy.Core.Data;
using GitCopy.Domain.Entities;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GitCopy.Domain.Repositories
{
    public interface ILogRepository : IRepository<Log>
    {
        Task Insert(Log log);
        Task InsertDetail(LogDetail logDetail);
        Task<IEnumerable<Log>> GetAll(Expression<Func<Log, bool>> filter);
        Task<Guid> StartTask();
        Task<Guid> ScheduleTask(DateTime dateStart, bool runWhenChanged);
    }
}
