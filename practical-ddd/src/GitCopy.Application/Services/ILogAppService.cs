using GitCopy.Application.ViewModels;
using System;
using System.Threading.Tasks;

namespace GitCopy.Application.Services
{
    public interface ILogAppService : IDisposable
    {
        Task ScheduleTask(DateTime dateStart, bool runWhenChanged);

        Task<bool> IsFirstExecution();

        Task<Guid> StartTask();

        Task InsertDetail(LogDetailViewModel logDetail);
    }
}
