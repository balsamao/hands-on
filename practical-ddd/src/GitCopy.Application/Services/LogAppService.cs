using GitCopy.Application.ViewModels;
using GitCopy.Domain.Entities;
using GitCopy.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GitCopy.Application.Services
{
    public class LogAppService : ILogAppService
    {
        private readonly ILogRepository _logRepository;

        public LogAppService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task ScheduleTask(DateTime dateStart, bool runWhenChanged)
        {
            await _logRepository.ScheduleTask(dateStart, runWhenChanged);
        }

        public async Task<bool> IsFirstExecution()
        {
            var results = await _logRepository.GetAll(p => p.Id != Guid.Empty);
            return results.Count() == 0;
        }

        public async Task<Guid> StartTask()
        {
            return await _logRepository.StartTask();
        }

        public async Task InsertDetail(LogDetailViewModel logDetailViewModel)
        {
            var logDetail = new LogDetail(logDetailViewModel.LogId, logDetailViewModel.RepositoryName, logDetailViewModel.Commits);
            await _logRepository.InsertDetail(logDetail);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
