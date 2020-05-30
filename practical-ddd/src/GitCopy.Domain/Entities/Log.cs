using GitCopy.Core.DomainObjects;
using GitCopy.Domain.Enums;
using System;
using System.Collections.Generic;

namespace GitCopy.Domain.Entities
{
    public class Log : Entity, IAggregateRoot
    {
        public DateTime DateStart { get; private set; }
        public DateTime DateEnd { get; private set; }
        public bool RunWhenChanged { get; private set; }
        public LogStatus Status { get; private set; }

        public Log(DateTime dateStart, bool runWhenChanged, LogStatus situation)
        {
            DateStart = dateStart;
            RunWhenChanged = runWhenChanged;
            Status = situation;

            Validate();
        }

        public void EndTask()
        {
            Status = LogStatus.Executed;
            DateEnd = DateTime.UtcNow;
        }

        public override void Validate()
        {
            Validation.LessThan(DateStart, DateTime.UtcNow.Date, $"The field DateStart value can't be less than {DateTime.UtcNow:yyyy-MM-dd}.");
            Validation.ExistsBetween(Status, new List<object> { LogStatus.Executed, LogStatus.Running, LogStatus.Scheduled }, $"The field Status value is invalid.");
        }

        public static class LogFactory
        {
            public static Log StartTask()
            {
                return new Log(DateTime.Now, true, LogStatus.Running);
            }

            public static Log ScheduleTask(DateTime dateStart, bool runWhenChanged)
            {
                return new Log(dateStart, runWhenChanged, LogStatus.Scheduled);
            }
        }
    }
}
