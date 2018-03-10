using Amazon.EC2.Util;
using Rhyous.SimpleArgs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Rhyous.AmazonEc2InstanceManager
{
    public class ArgsHandler : ArgsHandlerBase
    {
        public override void InitializeArguments(IArgsManager argsManager)
        {
            Arguments.AddRange(new List<Argument>
            {
                new Argument
                {
                    Name = "Action",
                    ShortName = "a",
                    Description = "The action to run.",
                    Example = "{name}=default",
                    DefaultValue = "Default",
                    AllowedValues = new ObservableCollection<string>(
                        typeof(InstanceManager).GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                                               .Select(m=>m.Name)),                    
                    IsRequired = true,
                    Action = (value) =>
                    {
                        Console.WriteLine(value);
                    }
                },
                new Argument
                {
                    Name = "KeyName",
                    ShortName = "k",
                    Description = "A key pair name.",
                    Example = "{name}=MyKeyPair",
                    Action = (value) =>
                    {
                        Console.WriteLine(value);
                    }
                },
                new Argument
                {
                    Name = "KeyOutputDirectory",
                    ShortName = "ko",
                    Description = "A key pair output directory. If not path is provided, the key pair will save to the Desktop by default.",
                    Example = "{name}=c:\\My\\Path",
                    DefaultValue = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    Action = (value) =>
                    {
                        Console.WriteLine(value);
                    }
                },
                new Argument
                {
                    Name = "KeyFile",
                    ShortName = "pem",
                    Description = "The full path to a public key already created on your file system in PEM format. The full Private key won't work.",
                    Example = "{name}=c:\\My\\Path\\mykeyfile.pem",
                    CustomValidation = (value) => File.Exists(value),
                    Action = (value) =>
                    {
                        Console.WriteLine(value);
                    }
                },
                new Argument
                {
                    Name = "ImageId",
                    ShortName = "i",
                    Description = "The operating system image to use. From the ImageUtilities class.",
                    Example = "{name}=WINDOWS_2012_BASE",
                    DefaultValue = "WINDOWS_2012_BASE",
                    AllowedValues = new ObservableCollection<string>(ImageUtilities.ImageKeys),
                    Action = (value) =>
                    {
                        Console.WriteLine(value);
                    }
                },
                new Argument
                {
                    Name = "InstanceId",
                    ShortName = "id",
                    Description = "The instance id. Amazon usually creates this for you.",
                    Example = "{name}=i-00f5ce7f5a30e62ac",
                    Action = (value) =>
                    {
                        Console.WriteLine(value);
                    }
                },
                new Argument
                {
                    Name = "InstanceName",
                    ShortName = "n",
                    Description = "The instance name to use.",
                    Example = "{name}=MyServer1",
                    DefaultValue = "",
                    Action = (value) =>
                    {
                        Console.WriteLine(value);
                    }
                },
                new Argument
                {
                    Name = "InstanceFqdn",
                    ShortName = "fwdn",
                    Description = "The instance fqdn to use.",
                    Example = "{name}=MyServer1.domain.tld",
                    DefaultValue = "",
                    Action = (value) =>
                    {
                        Console.WriteLine(value);
                    }
                },
                new Argument
                {
                    Name = "username",
                    ShortName = "u",
                    Description = "The user name to login with.",
                    Example = "{name}=Adminstrator",
                    DefaultValue = "Administrator",
                    Action = (value) =>
                    {
                        Console.WriteLine(value);
                    }
                },
                new Argument
                {
                    Name = "password",
                    ShortName = "p",
                    Description = "The password to login with.",
                    Example = "{name}=p@ssw0rd!",
                    DefaultValue = "",
                    Action = (value) =>
                    {
                        Console.WriteLine(value);
                    }
                },
                new Argument
                {
                    Name = "File",
                    ShortName = "f",
                    Description = "A file.",
                    Example = "{name}=c:\\my\\file.txt",
                    Action = (value) =>
                    {
                        Console.WriteLine(value);
                    }
                },
                new Argument
                {
                    Name = "Port",
                    ShortName = "po",
                    Description = "The port to open.",
                    Example = "{name}=22",
                    CustomValidation = (value) =>
                    {
                        return Regex.IsMatch(value, CommonAllowedValues.Digits);
                    },
                    Action = (value) =>
                    {
                        Console.WriteLine(value);
                    }
                },
                new Argument
                {
                    Name = "Protocol",
                    ShortName = "pro",
                    Description = "The Protocol, such as tcp or udp.",
                    Example = "{name}=tcp",
                    DefaultValue = "tcp",
                    Action = (value) =>
                    {
                        Console.WriteLine(value);
                    }
                },
                new Argument
                {
                    Name = "SecurityGroupName",
                    ShortName = "sgn",
                    Description = "The Security Group Name.",
                    Example = "{name}=MySecurityGroup",
                    DefaultValue = "Default",
                    Action = (value) =>
                    {
                        Console.WriteLine(value);
                    }
                },
            });
        }

        public override void HandleArgs(IReadArgs inArgsHandler)
        {
            base.HandleArgs(inArgsHandler);
            Program.OnArgumentsHandled();
        }
    }
}