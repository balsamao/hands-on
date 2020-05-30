using GitCopy.Domain.Enums;
using System;

namespace GitCopy.Application.ViewModels
{
    public class LogViewModel
    {
        public LogViewModel()
        {
            DateStart = DateTime.UtcNow;
        }

        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public bool RunWhenChanged { get; set; }
        public LogStatus Status { get; set; }
    }
}
