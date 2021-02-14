using Requistador.WebApi.AppConfiguration;
using System;

namespace Requistador.WebApi.ApiModels
{
    public class Syslog
    {
        private readonly string _subject;
        private readonly string _description;
        private readonly eLogSeverity _severity;

        public Syslog(string subject, string description, eLogSeverity severity, string createdBy)
        {
            _subject = subject;
            _description = description;
            _severity = severity;

            CreatedBy = createdBy;
            CreatedOn = DateTime.UtcNow.ToString(AppConstants.Format_SyslogTime);
        }

        
        public string CreatedOn { get; init; }
        public string CreatedBy { get; init; }

        public string[] GetLines()
        {
            return new string[]
            {
                $"By: {CreatedBy}",
                $"Severity: {_severity}",
                $"Subject: {_subject}",
                Environment.NewLine,
                "Description:",
                _description
            };
        }
    }
}
