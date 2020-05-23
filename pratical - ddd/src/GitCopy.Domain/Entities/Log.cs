using GitCopy.Core.DomainObjects;
using GitCopy.Domain.Enums;
using System;

namespace GitCopy.Domain.Entities
{
    public class Log : Entity, IAggregateRoot
    {
        public DateTime DateStart { get; private set; }
        public DateTime DateEnd { get; private set; }
        public bool RunWhenChanged { get; private set; }
        public LogType LogType { get; private set; }

        public Log(DateTime dateStart, DateTime dateEnd, bool runWhenChanged, LogType logType)
        {
            DateStart = dateStart;
            DateEnd = dateEnd;
            RunWhenChanged = runWhenChanged;
            LogType = logType;
        }
    }
}
