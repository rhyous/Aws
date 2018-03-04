using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Rhyous.SimpleArgs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace Rhyous.AmazonS3BucketManager
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
                if (paramInfo.ParameterType == typeof(AmazonS3Client) || paramInfo.ParameterType == typeof(TransferUtility))
                    parameters.Add(Activator.CreateInstance(paramInfo.ParameterType, region));
                if (paramInfo.ParameterType == typeof(string))
                    parameters.Add(Args.Value(paramInfo.Name));
            }

            return parameters;
        }
    }
}