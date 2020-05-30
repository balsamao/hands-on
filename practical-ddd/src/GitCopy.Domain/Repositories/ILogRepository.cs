using GitCopy.Core.Data;
using GitCopy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitCopy.Domain.Repositories
{
    public interface ILogRepository : IRepository<Log>
    {
        Task Insert(Log log);
        Task InsertDetail(LogDetail logDetail);
        Task<IEnumerable<Log>> GetAll();

        Task<Guid> StartTask();
        Task ScheduleTask(DateTime dateStart, bool runWhenChanged);
    }
}
