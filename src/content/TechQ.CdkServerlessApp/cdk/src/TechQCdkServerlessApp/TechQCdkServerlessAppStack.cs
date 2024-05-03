using Amazon.CDK;
using Constructs;
using System.Collections.Generic;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.Route53;
using Amazon.CDK.CloudAssembly.Schema;
using System;
using Amazon.CDK.AWS.SecretsManager;
using Amazon.CDK.AWS.KMS;
using Amazon.CDK.AWS.CertificateManager;
using Amazon.CDK.AWS.Route53.Targets;
using Cdklabs.CdkNag;
using Amazon.CDK.AWS.WAFv2;

namespace TechQCdkServerlessApp
{
    public class StackDetails
    {
        public string AlertRecipient { get; set; }
        public string Owner { get; set; }
        public string Repo { get; set; }

        public StackDetails()
        {
        }

        // public StackDetails(string alertRecipient, string owner, string application, string project, string repo, string environment)
        // {
        //     AlertRecipient = alertRecipient;
        //     Owner = owner;
        //     Application = application;
        //     Project = project;
        //     Repo = repo;
        //     Environment = environment;
        // }

        public Dictionary<string, string> AsDictionary()
        {
            return new Dictionary<string, string>
            {
                { "AlertRecipient", AlertRecipient },
                { "Owner", Owner },
                { "Repo", Repo },
            };
        }
    }
    public class LambdaConfig
    {
        /// <summary>
        /// The name of the Lambda function.
        /// </summary>
        public string FunctionName { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this is the default handler.
        /// </summary>
        public bool IsDefaultHandler { get; set; } = true;

        /// <summary>
        /// The path on the API Gateway that proxies to this Lambda.
        ///
        public string ProxyPath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Lambda function is joined to a VPC.
        /// </summary>
        public bool IsVpcJoined { get; set; } = false;

        /// <summary>
        /// Gets or sets the handler for the Lambda function.
        /// </summary>
        public string Handler { get; set; }

        /// <summary>
        /// Gets or sets the folder path of the code located
        /// under the `src` folder in the root of the repo
        /// for the Lambda function.
        /// </summary>
        public string CodeSrcFolder { get; set; }

        /// <summary>
        /// Gets or sets the code archive path of a ready-to-deploy Lambda function.
        /// If this is set, the `CodeProjectPath` will be ignored.
        /// </summary>
        public string CodeArchivePath { get; set; }

        /// <summary>
        /// Gets or sets the memory size for the Lambda function.
        /// </summary>
        public int MemorySize { get; set; } = 512;

        /// <summary>
        /// Gets or sets the timeout for the Lambda function.
        /// </summary>
        public int Timeout { get; set; } = 60;

        /// <summary>
        /// Gets or sets the environment variables for the Lambda function.
        /// </summary>
        public IDictionary<string, string> EnvironmentVariables { get; set; }

        public Dictionary<string, string> AsDictionary()
        {
            return new Dictionary<string, string>
            {
                { "FunctionName", FunctionName },
                { "IsDefaultHandler", IsDefaultHandler.ToString() },
                { "ProxyPath", ProxyPath },
                { "IsVpcJoined", IsVpcJoined.ToString() },
                { "Handler", Handler },
                { "CodeSrcFolder", CodeSrcFolder },
                { "MemorySize", MemorySize.ToString() },
                { "Timeout", Timeout.ToString() },
                { "EnvironmentVariables", EnvironmentVariables.ToString() },
            };
        }
    }

    public class TechQCdkServerlessAppStackProps : StackProps
    {
        public string AppName { get; init; }
        public string Subdomain { get; set; }
        public LambdaConfig[] LambdaConfigs { get; init; }
        public string AspNetCoreEnvironment { get; set; }
        public StackDetails StackOwnershipDetails { get; init; }
        public string ApiGatewayStage { get; set; } = "Prod";
    }

    public class TechQCdkServerlessAppStack : Stack
    {
        public LambdaRestApi RestApi { get; set; }
        public Function DefaultPathHandler { get; set; }
        public ISecret DatabaseSecret { get; set; }

        internal TechQCdkServerlessAppStack(Construct scope, string id, TechQCdkServerlessAppStackProps props) : base(scope, id, props)
        {
            if (props.StackOwnershipDetails == null)
            {
                throw new Exception("StackOwnershipDetails must be defined on the props");
            }

            foreach (var entry in props.StackOwnershipDetails.AsDictionary())
            {
                Console.WriteLine($"Adding tag '{entry.Key}' with value '{entry.Value}' to the stack");
                Tags.SetTag(entry.Key, entry.Value);
            }

            Utils utils = new Utils();
            IRole infraAdminRole = utils.GetInfraAdminRole(this);
            IVpc ctVpc = utils.GetControlTowerVpc(this);

            var domainName = utils.GetConfigValue("DomainName") as string ??
                throw new Exception("DomainName must be defined on the EnvConfig");


            var hostedZone = utils.GetHostedZone(this, "RootHostedZone", domainName);

            string subDomainName;
            IHostedZone appHostedZone;
            ICertificate tlsCertificate;
            if (props.Subdomain != null)
            {

                subDomainName = string.Join(
                    ".", [
                        props.Subdomain,
                        domainName
                    ]
                );

                appHostedZone = new HostedZone(this, "ApiHostedZone", new HostedZoneProps()
                {
                    ZoneName = subDomainName,
                    Comment = $"Hosted zone for {subDomainName}",
                });

                var zoneDelegationRecord = new ZoneDelegationRecord(this, "ZoneDelegationRecord", new ZoneDelegationRecordProps
                {
                    NameServers = appHostedZone.HostedZoneNameServers,
                    RecordName = subDomainName,
                    Zone = hostedZone,
                    Ttl = Duration.Seconds(300),
                });
                zoneDelegationRecord.Node.AddDependency([appHostedZone]);

                tlsCertificate = new Certificate(this, "TlsCertificate", new CertificateProps
                {
                    Validation = CertificateValidation.FromDns(appHostedZone),
                    CertificateName = subDomainName,
                    DomainName = subDomainName,
                });
                tlsCertificate.Node.AddDependency([zoneDelegationRecord]);
            }
            else
            {
                subDomainName = domainName;
                appHostedZone = hostedZone;
                tlsCertificate = new Certificate(this, "TlsCertificate", new CertificateProps
                {
                    Validation = CertificateValidation.FromDns(appHostedZone),
                    CertificateName = domainName,
                    DomainName = domainName,
                });
            }

            var assetOption = new Amazon.CDK.AWS.S3.Assets.AssetOptions()
            {
                Bundling = new BundlingOptions()
                {
                    Image = Runtime.DOTNET_8.BundlingImage,
                    User = "root",
                    OutputType = BundlingOutput.ARCHIVED,
                    Command = new string[]{
                        "/bin/sh",
                        "-c",
                        " curl --silent --location https://rpm.nodesource.com/setup_20.x | bash -"+
                        " && dnf -y install nodejs"+
                        " && dotnet tool install -g Amazon.Lambda.Tools"+
                        " && dotnet build"+
                        " && dotnet lambda package --output-package /asset-output/function.zip"
                    },
                },
            };

            // Resources
            // Create a shared role for the Lambda functions to use
            var lambdaRole = new Role(this, "LambdaRole", new RoleProps
            {
                AssumedBy = new ServicePrincipal("lambda.amazonaws.com"),
                ManagedPolicies = new IManagedPolicy[] {
                    ManagedPolicy.FromAwsManagedPolicyName("service-role/AWSLambdaBasicExecutionRole"),
                    ManagedPolicy.FromAwsManagedPolicyName("service-role/AWSLambdaVPCAccessExecutionRole")
                }
            });

            DatabaseSecret = utils.GetDatabaseSecret(this, "DatabaseSecret");
            DatabaseSecret.GrantRead(lambdaRole);
            DatabaseSecret.EncryptionKey?.GrantEncryptDecrypt(lambdaRole);

            // Loop through LambdaConfigs and create Lambda functions accordingly
            for (int i = 0; i < props.LambdaConfigs.Length; i++)
            {
                var lambdaConfig = props.LambdaConfigs[i];

                if (lambdaConfig.IsDefaultHandler)
                {
                    var lambdaFunction = new Function(this, $"LambdaFunction{lambdaConfig.FunctionName}", new FunctionProps
                    {
                        Runtime = Runtime.DOTNET_8,
                        Code = lambdaConfig.CodeArchivePath != null ?
                                Code.FromAsset(lambdaConfig.CodeArchivePath) :
                                Code.FromAsset($"../src/{lambdaConfig.CodeSrcFolder}/", assetOption),
                        Handler = lambdaConfig.Handler,
                        MemorySize = lambdaConfig.MemorySize,
                        Timeout = Duration.Seconds(lambdaConfig.Timeout),
                        Role = lambdaRole,
                        VpcSubnets = lambdaConfig.IsVpcJoined ? new SubnetSelection() { SubnetType = SubnetType.PRIVATE_WITH_EGRESS } : null,
                        Vpc = lambdaConfig.IsVpcJoined ? ctVpc : null,
                        Environment = new Dictionary<string, string> {
                            { "ASPNETCORE_ENVIRONMENT", props.AspNetCoreEnvironment },
                            { "APP_BASE_URL", $"https://{subDomainName}" },
                            { "APP_FQDN", subDomainName },
                            { "APP_DOMAIN", domainName },
                        },
                    });

                    foreach (KeyValuePair<string, string> entry in props.StackOwnershipDetails.AsDictionary())
                    {
                        lambdaFunction.AddEnvironment(entry.Key, entry.Value);
                    }

                    if (lambdaConfig.EnvironmentVariables != null)
                    {
                        foreach (KeyValuePair<string, string> entry in lambdaConfig.EnvironmentVariables)
                        {
                            lambdaFunction.AddEnvironment(entry.Key, entry.Value);
                        }
                    }
                    DefaultPathHandler = lambdaFunction;
                    break;
                }
            }

            RestApi = new LambdaRestApi(this, $"{props.AppName}API", new LambdaRestApiProps
            {
                Description = $"API Gateway for {props.AppName}",
                EndpointExportName = $"{props.AppName}APIGatewayEndpoint",
                DisableExecuteApiEndpoint = true,
                RestApiName = $"{props.AppName}API",
                Handler = DefaultPathHandler,
                Proxy = true,
                BinaryMediaTypes = ["*/*"],
                DeployOptions = new StageOptions
                {
                    StageName = props.ApiGatewayStage,
                    TracingEnabled = true,
                    MetricsEnabled = true,
                    DataTraceEnabled = true,
                },
                CloudWatchRole = true,
                Deploy = true,
                DefaultCorsPreflightOptions = new CorsOptions
                {
                    AllowOrigins = Cors.ALL_ORIGINS,
                    AllowMethods = Cors.ALL_METHODS,
                    AllowHeaders = Cors.DEFAULT_HEADERS,
                },
                DomainName = new DomainNameOptions
                {
                    DomainName = subDomainName,
                    Certificate = tlsCertificate,
                }
            });
            // RestApi.Root.AddProxy(new ProxyResourceOptions
            // {
            //     AnyMethod = true,
            //     DefaultIntegration = new LambdaIntegration(DefaultPathHandler),
            //     DefaultCorsPreflightOptions = new CorsOptions
            //     {
            //         AllowOrigins = Cors.ALL_ORIGINS,
            //         AllowMethods = Cors.ALL_METHODS,
            //         AllowHeaders = Cors.DEFAULT_HEADERS,
            //     },
            // });

            var wafAssociation = new CfnWebACLAssociation(
                this,
                "OpenWafAclAssociation",
                new CfnWebACLAssociationProps()
                {
                    ResourceArn = $"arn:aws:apigateway:{Stack.Of(this).Region}::/restapis/{RestApi.RestApiId}/stages/{RestApi.DeploymentStage.StageName}",
                    WebAclArn = Fn.ImportValue("OpenWafAclArn"),
                }
            );

            var r53ARecord = new ARecord(this, "AppARecord",
                new ARecordProps
                {
                    Zone = appHostedZone,
                    Target = RecordTarget.FromAlias(new ApiGateway(RestApi)),
                });
            r53ARecord.Node.AddDependency([appHostedZone]);

            for (int i = 0; i < props.LambdaConfigs.Length; i++)
            {
                var lambdaConfig = props.LambdaConfigs[i];

                if (!lambdaConfig.IsDefaultHandler)
                {
                    var lambdaFunction = new Function(this, $"LambdaFunction{lambdaConfig.FunctionName}", new FunctionProps
                    {
                        Runtime = Runtime.DOTNET_8,
                        Code = lambdaConfig.CodeArchivePath != null ?
                                Code.FromAsset(lambdaConfig.CodeArchivePath) :
                                Code.FromAsset($"../src/{lambdaConfig.CodeSrcFolder}/", assetOption),
                        Handler = lambdaConfig.Handler,
                        MemorySize = lambdaConfig.MemorySize,
                        Timeout = Duration.Seconds(lambdaConfig.Timeout),
                        Role = lambdaRole,
                        VpcSubnets = lambdaConfig.IsVpcJoined ? new SubnetSelection() { SubnetType = SubnetType.PRIVATE_WITH_EGRESS } : null,
                        Vpc = lambdaConfig.IsVpcJoined ? ctVpc : null,
                        Environment = new Dictionary<string, string> {
                            { "ASPNETCORE_ENVIRONMENT", props.AspNetCoreEnvironment },
                            { "APP_BASE_URL", $"https://{subDomainName}{lambdaConfig.ProxyPath}" },
                            { "APP_FQDN", subDomainName },
                            { "APP_DOMAIN", domainName },
                        },
                    });

                    foreach (KeyValuePair<string, string> entry in props.StackOwnershipDetails.AsDictionary())
                    {
                        lambdaFunction.AddEnvironment(entry.Key, entry.Value);
                    }

                    foreach (KeyValuePair<string, string> entry in props.StackOwnershipDetails.AsDictionary())
                    {
                        lambdaFunction.AddEnvironment(entry.Key, entry.Value);
                    }
                    if (lambdaConfig.EnvironmentVariables != null)
                    {
                        foreach (KeyValuePair<string, string> entry in lambdaConfig.EnvironmentVariables)
                        {
                            lambdaFunction.AddEnvironment(entry.Key, entry.Value);
                        }
                    }
                    var newFunc = RestApi.Root.AddResource(lambdaConfig.ProxyPath, new ResourceOptions
                    {
                        DefaultIntegration = new LambdaIntegration(lambdaFunction)
                    });
                    newFunc.AddMethod("ANY");
                    newFunc.AddProxy();
                }
            }


            // var customDomain = new CfnDomainName(this, "CustomDomain", new CfnDomainNameProps
            // {
            //     DomainName = subDomainName,
            //     EndpointConfiguration = new CfnDomainName.EndpointConfigurationProperty
            //     {
            //         Types = [ "REGIONAL" ]
            //     },
            //     RegionalCertificateArn = tlsCertificate.CertificateArn
            // });
            // var customDomainBasePathMapping = new CfnBasePathMapping(this, "CustomDomainBasePathMapping", new CfnBasePathMappingProps
            // {
            //     DomainName = subDomainName,
            //     RestApiId = RestApi.RestApiId,
            //     Stage = RestApi.DeploymentStage.StageName,
            // });

            // Outputs
            new CfnOutput(this, "AppBaseUrl", new CfnOutputProps
            {
                Description = "The application base URL using the custom domain",
                Value = $"https://{subDomainName}"
            });

            new CfnOutput(this, "ApiStageUrl", new CfnOutputProps
            {
                Description = $"API endpoint URL for {props.ApiGatewayStage} environment",
                Value = $"https://{RestApi.RestApiId}.execute-api.{Stack.Of(this).Region}.amazonaws.com/{RestApi.DeploymentStage.StageName}/"
            });

            /*
            * CDK Nag rule suppressions
            */
            NagSuppressions.AddStackSuppressions(
                this,
                [
                    new NagPackSuppression()
                    {
                        Id = "AwsSolutions-IAM4",
                        Reason =
                            "ManagedPolicies are being used temporarily to validate permissions needed."
                    },
                    new NagPackSuppression()
                    {
                        Id = "AwsSolutions-APIG2",
                        Reason =
                            "Pending implementation"
                    },
                    new NagPackSuppression()
                    {
                        Id = "AwsSolutions-APIG1",
                        Reason =
                            "Pending implementation"
                    },
                    new NagPackSuppression()
                    {
                        Id = "AwsSolutions-APIG3",
                        Reason =
                            "Pending implementation"
                    },
                    new NagPackSuppression()
                    {
                        Id = "AwsSolutions-APIG6",
                        Reason =
                            "Pending implementation"
                    },
                    new NagPackSuppression()
                    {
                        Id = "AwsSolutions-APIG4",
                        Reason =
                            "Pending implementation"
                    },
                    new NagPackSuppression()
                    {
                        Id = "AwsSolutions-COG4",
                        Reason =
                            "Pending implementation"
                    },
                ]
            );
        }
    }
}
