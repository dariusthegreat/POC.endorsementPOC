using Amazon.CDK;
using Cdklabs.CdkNag;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;

namespace TechQCdkServerlessApp
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();

            string tgtRegion =
                System.Environment.GetEnvironmentVariable("CDK_DEFAULT_REGION")
                ?? "us-west-2";
            string targetEnvironment =
                System.Environment.GetEnvironmentVariable("CDK_TARGET_ENVIRONMENT")
                ?? "development";
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            string aspNetCoreEnvironment =
                System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ??
                textInfo.ToTitleCase(targetEnvironment);

            var cdkEnvironment = new Amazon.CDK.Environment
            {
                Account = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT") ?? throw new Exception("CDK_DEFAULT_ACCOUNT is not set"),
                Region = tgtRegion,
            };

            string deployJson = Path.Combine(
                Directory.GetParent(
                    Directory.GetCurrentDirectory()
                ).FullName,
                $"deploy.{aspNetCoreEnvironment}.json"
            );
            string jsonString = File.ReadAllText(deployJson);
            TechQCdkServerlessAppStackProps stackProps = JsonSerializer.Deserialize<TechQCdkServerlessAppStackProps>(jsonString)!;

            stackProps.Env = cdkEnvironment;
            stackProps.AspNetCoreEnvironment = aspNetCoreEnvironment;

            new TechQCdkServerlessAppStack(app, $"{stackProps.AppName}-{targetEnvironment}", stackProps);
            Aspects.Of(app).Add(new AwsSolutionsChecks());
            app.Synth();
        }
    }
}
