using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewRelic.Platform.Sdk;

namespace RedisWindowsNewRelicPlugin
{
    class RedisPlugin : Agent
    {
        public override string Guid
        {
            get { return "io.jennings.newrelic.rediswindows"; }
        }

        public override string GetAgentName()
        {
            return "Redis for Windows Plugin";
        }

        public override string Version
        {
            get { return "0.1.0"; }
        }

        public override void PollCycle()
        {
            throw new NotImplementedException();
        }
    }
}
