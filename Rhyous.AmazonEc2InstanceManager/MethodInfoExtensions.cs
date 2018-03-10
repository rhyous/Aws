using Amazon;
using Amazon.EC2;
using Rhyous.SimpleArgs;
using Rhyous.StringLibrary;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace Rhyous.AmazonEc2InstanceManager
{
    public static class MethodInfoExtensions
    {
        public static List<object> DynamicallyGenerateParameters(this MethodInfo mi)
        {
            var parameterInfoArray = mi.GetParameters();
            var parameters = new List<object>();
            var region = RegionEndpoint.GetBySystemName(ConfigurationManager.AppSettings["AWSRegion"]);
            foreach (var paramInfo in parameterInfoArray)
            {
                if (paramInfo.ParameterType == typeof(AmazonEC2Client))
                    parameters.Add(new AmazonEC2Client(region));
                if (paramInfo.ParameterType == typeof(string))
                    parameters.Add(Args.Value(paramInfo.Name));
                if (paramInfo.ParameterType.IsPrimitive)
                    parameters.Add(Args.Value(paramInfo.Name).ToType(paramInfo.ParameterType));
            }
            return parameters;
        }
    }
}