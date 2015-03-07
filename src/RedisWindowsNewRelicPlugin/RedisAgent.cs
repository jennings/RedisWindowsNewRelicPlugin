using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewRelic.Platform.Sdk;
using NewRelic.Platform.Sdk.Processors;
using StackExchange.Redis;

namespace RedisWindowsNewRelicPlugin
{
    class RedisAgent : Agent
    {
        ConnectionMultiplexer Redis;
        string name;
        EpochProcessor keysEvictedProcessor;
        EpochProcessor commandsProcessedProcessor;

        public RedisAgent(string name, string configuration)
        {
            this.name = name;
            Redis = ConnectionMultiplexer.Connect(configuration);
            keysEvictedProcessor = new EpochProcessor();
            commandsProcessedProcessor = new EpochProcessor();
        }

        public override string Guid
        {
            get { return "io.jennings.newrelic.rediswindows"; }
        }

        public override string Version
        {
            get { return "0.1.0"; }
        }

        public override string GetAgentName()
        {
            return this.name;
        }

        public override void PollCycle()
        {
            var endpoint = Redis.GetEndPoints(true).FirstOrDefault();
            if (endpoint == null)
                return;

            var server = Redis.GetServer(endpoint);
            var info = server.Info().ToDictionary(
                g => g.Key,
                g => g.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));

            ReportMetric(info, "memory", "used_memory", "Memory/Used", "Bytes");
            ReportMetric(info, "memory", "mem_fragmentation_ratio", "Memory/FragmentationRatio", "");

            ReportMetric(info, "stats", "keys_evicted", "Stats/KeysEvicted", "Keys/sec", keysEvictedProcessor);
            ReportMetric(info, "stats", "total_commands_processed", "Stats/TotalCommandsProcessed", "Cmds/sec", commandsProcessedProcessor);
        }

        void ReportMetric(Dictionary<string, Dictionary<string, string>> info, string section, string name, string metricName, string unit, IProcessor processor = null)
        {
            Dictionary<string, string> sectionDict;
            string value;

            if (info.TryGetValue(section, out sectionDict))
            {
                if (sectionDict.TryGetValue(name, out value))
                {
                    var f = Convert.ToSingle(value);
                    if (processor != null)
                        ReportMetric(metricName, unit, processor.Process(f));
                    else
                        ReportMetric(metricName, unit, f);
                }
            }
        }
    }
}
