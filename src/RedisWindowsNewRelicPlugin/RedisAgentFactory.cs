using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewRelic.Platform.Sdk;

namespace RedisWindowsNewRelicPlugin
{
    class RedisAgentFactory : AgentFactory
    {
        public override Agent CreateAgentWithConfiguration(IDictionary<string, object> properties)
        {
            var name = (string)properties["name"];
            var configuration = (string)properties["configuration"];

            return new RedisAgent(name, configuration);
        }
    }
}
