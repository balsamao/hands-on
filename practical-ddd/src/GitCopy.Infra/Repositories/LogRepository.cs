using GitCopy.Domain.Entities;
using GitCopy.Domain.Repositories;
using GitCopy.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitCopy.Infra.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly IContextDB _db;

        public LogRepository(IContextDB context)
        {
            _db = context;
        }

        public async Task Insert(Log log)
        {
            if (log == null)
                throw new ArgumentNullException(typeof(Log).Name + " object is null.");

            var dbCollection = _db.GetCollection<Log>(typeof(Log).Name);

            dbCollection.Insert(log);

            await Task.CompletedTask;
        }

        public async Task InsertDetail(LogDetail logDetail)
        {
            if (logDetail == null)
                throw new ArgumentNullException(typeof(LogDetail).Name + " object is null.");

            var dbCollection = _db.GetCollection<LogDetail>(typeof(LogDetail).Name);

            dbCollection.Insert(logDetail);

            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Log>> GetAll()
        {
            var dbCollection = _db.GetCollection<Log>(typeof(Log).Name);
            if (dbCollection == null)
                return new List<Log>();

            return await Task.FromResult(dbCollection.FindAll().AsEnumerable());
        }
        
        public async Task<Guid> StartTask()
        {
            var log = Log.LogFactory.StartTask();
            await Insert(log);

            return log.Id;
        }

        public async Task ScheduleTask(DateTime dateStart, bool runWhenChanged)
        {
            var log = Log.LogFactory.ScheduleTask(dateStart, runWhenChanged);
            await Insert(log);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
