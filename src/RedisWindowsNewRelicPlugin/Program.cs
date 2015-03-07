using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewRelic.Platform.Sdk;

namespace RedisWindowsNewRelicPlugin
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                var runner = new Runner();
                runner.Add(new RedisAgentFactory());
                runner.SetupAndRun();
                return 0;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("EXCEPTION: " + ex.Message);
                return -1;
            }
        }
    }
}
