using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Amazon.EC2.Model;
using Rhyous.SimpleArgs;

namespace Rhyous.AmazonEc2InstanceManager
{
    class Program
    {
        static void Main(string[] args)
        {
            new ArgsManager<ArgsHandler>().Start(args);
        }

        internal static void OnArgumentsHandled()
        {
            var action = Args.Value("Action");
            var flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;
            MethodInfo mi = typeof(InstanceManager).GetMethod(action, flags);
            List<object> parameters = mi.DynamicallyGenerateParameters();
            var task = mi.Invoke(null, parameters.ToArray()) as Task;
            task.Wait();
        }
    }
}
