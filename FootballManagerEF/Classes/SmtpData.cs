using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Interfaces
{
    public class SmtpData
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string AgentSine { get; set; }
        public string AgentDutyCode { get; set; }
        public string SmtpAgentDutyParam1 { get; set; }
        public string SmtpAgentDutyParam2 { get; set; }
        public string SmtpAgentDutyParam3 { get; set; }
    }
}
