using Rhyous.SimpleArgs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rhyous.AmazonS3BucketManager
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
                    AllowedValues = new ObservableCollection<string>
                    {
                        "CreateBucket",
                        "CreateBucketDirectory",
                        "CreateTextFile",
                        "DeleteBucket",
                        "DeleteBucketDirectory",
                        "ListFiles",
                        "UploadFile",
                        "UploadFiles"
                    },
                    IsRequired = true,
                    Action = (value) =>
                    {
                        Console.WriteLine(value);
                    }
                },
                new Argument
                {
                    Name = "Bucket",
                    ShortName = "b",
                    Description = "The bucket name to create. No uppercase or underscores allowed.",
                    Example = "{name}=my.first.bucket",
                    DefaultValue = "my.first.bucket",
                    IsRequired = true,
                    CustomValidation = (value) => 
                    {
                        return Regex.IsMatch(value, "^[a-z0-9.]+$");
                    },
                    Action = (value) =>
                    {
                        Console.WriteLine(value);
                    }
                },
                new Argument
                {
                    Name = "File",
                    ShortName = "f",
                    Description = "The file name.",
                    Example = "{name}=c:\\some\file.txt",
                    CustomValidation = (value) =>
                    {
                        return File.Exists(value);
                    },
                    Action = (value) =>
                    {
                        Console.WriteLine(value);
                    }
                },
                new Argument
                {
                    Name = "Directory",
                    ShortName = "D",
                    Description = "The directory name.",
                    Example = "{name}=MyFolder",
                    CustomValidation = (value) =>
                    {
                        return value.All(c=>char.IsLetterOrDigit(c));
                    },
                    Action = (value) =>
                    {
                        Console.WriteLine(value);
                    }
                },
                new Argument
                {
                    Name = "FileName",
                    ShortName = "N",
                    Description = "The name of text a file to create.",
                    Example = "{name}=MyTextfile.txt",
                    Action = (value) =>
                    {
                        Console.WriteLine(value);
                    }
                },
                new Argument
                {
                    Name = "Text",
                    ShortName = "T",
                    Description = "The text to put in a text file.",
                    Example = "{name}=\"This is some text!\"",
                    Action = (value) =>
                    {
                        Console.WriteLine(value);
                    }
                },
                new Argument
                {
                    Name = "LocalDirectory",
                    ShortName = "ld",
                    Description = "The local directory to copy to S3.",
                    Example = "{name}=C:\\TestDir",
                    Action = (value) =>
                    {
                        Console.WriteLine(value);
                    }
                },
                new Argument
                {
                    Name = "RemoteDirectory",
                    ShortName = "rd",
                    Description = "The remote directory on the S3 Bucket.",
                    Example = "{name}=/My/Remote/Directory",
                    Action = (value) =>
                    {
                        Console.WriteLine(value);
                    }
                }
            });
        }

        public override void HandleArgs(IReadArgs inArgsHandler)
        {
            base.HandleArgs(inArgsHandler);
            Program.OnArgumentsHandled();
        }
    }
}