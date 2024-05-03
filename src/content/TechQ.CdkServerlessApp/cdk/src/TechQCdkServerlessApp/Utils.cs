using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Route53;
using Amazon.CDK.AWS.SecretsManager;
using Constructs;
using Vpc = Amazon.CDK.AWS.EC2.Vpc;

namespace TechQCdkServerlessApp;

public class Utils
{
    public string TargetEnvironment;
    public Dictionary<string, IDictionary<string, object>> EnvConfig;
    public Dictionary<string, string> GroupDictionary;
    public List<string> OrgSharedBucketNames;

    public Utils()
    {
        TargetEnvironment =
            System.Environment.GetEnvironmentVariable("CDK_TARGET_ENVIRONMENT")
            ?? "development";

        GroupDictionary = new Dictionary<string, string>
        {
            { "AWSControlTowerAdmins", "7841d3e0-d061-70d3-0412-5e152647ff7a" },
            { "TechQAdmins", "18d1e350-70a1-7079-3327-849eb7b13f25" },
            { "TechQDevelopers", "18a1e3b0-90d1-70db-1e59-a8d45a63e009" },
            { "VPN_DBA", "68c1f390-f021-7029-7de1-f2d8ee0b9346" },
            { "VPN_DevOps", "58a1a350-b0b1-7000-fef5-d9a2bb71ea69" },
            { "VPN_Developers", "48419370-60b1-70bc-17a4-10301cb35798" },
        };

        OrgSharedBucketNames =
        [
            "techq-com-shared-internal",
            "techq-com-shared-dsc-mofs",
            "techq-com-shared-dsc-reports",
            "techq-com-shared-dsc-status",
            "techq-com-shared-dsc-output"
        ];

        EnvConfig = new Dictionary<string, IDictionary<string, object>>
        {
            {
                "development", new Dictionary<string, object>
                {
                    { "AllocatedStorage", 100 },
                    { "SqlServerPort", 1444 },
                    { "BackupRetention", Duration.Days(1) },
                    { "MasterUserRotationDuration", Duration.Days(7) },
                    { "LogRetention", Duration.Days(7) },
                    { "AppUserRotationDuration", Duration.Days(30) },
                    { "WebAppInstanceClass", InstanceClass.T3 },
                    { "WebAppInstanceSize", InstanceSize.MEDIUM },
                    { "WebAppInstanceDesiredCount", 2 },
                    { "ClientVpnCidr", "192.168.128.0/22" },
                    { "VpcCidr", "10.0.0.0/16" },
                    {
                        "VpnServerCertArn",
                        "arn:aws:acm:us-west-2:354903695540:certificate/a6fb95cc-08dc-4860-aae3-ab24db44f0dd"
                    },
                    { "VpnGroupsIdsToAdd", new[] { "VPN_DBA", "VPN_DevOps", "VPN_Developers" } },
                    { "DomainName", "dev.techq.app" },
                    { "InternalDomainName", "dev.mytechq.com" },
                    { "HealthCheckPath", "/healthz" },
                }
            },
            {
                "production", new Dictionary<string, object>
                {
                    { "AllocatedStorage", 1000 },
                    { "SqlServerPort", 1444 },
                    { "BackupRetention", Duration.Days(14) },
                    { "LogRetention", Duration.Days(7) },
                    { "MasterUserRotationDuration", Duration.Days(7) },
                    { "AppUserRotationDuration", Duration.Days(30) },
                    { "WebAppInstanceClass", InstanceClass.M5 },
                    { "WebAppInstanceSize", InstanceSize.MEDIUM },
                    { "WebAppInstanceDesiredCount", 5 },
                    { "ClientVpnCidr", "192.168.148.0/22" },
                    { "VpcCidr", "10.0.0.0/16" },
                    { "VpnServerCertArn", "" },
                    { "VpnGroupsIdsToAdd", new[] { "VPN_DBA", "VPN_DevOps" } },
                    { "DomainName", "techq.app" },
                    { "InternalDomainName", "mytechq.com" },
                    { "HealthCheckPath", "/healthz" },
                }
            }
        };
    }

    public object GetConfigValue(string key, object defaultValue = null)
    {
        return EnvConfig[TargetEnvironment].TryGetValue(key, out var value) ? value : defaultValue;
    }

    public IRole GetInfraAdminRole(Construct scope)
    {
        return Role.FromRoleName(scope, "GitHubCICDInfraAdminRole", "GitHubCICDInfraAdminRole");
    }

    public IVpc GetControlTowerVpc(Construct scope)
    {
        return Vpc.FromLookup(scope, "ControlTowerVPC",
            new VpcLookupOptions { VpcName = "aws-controltower-VPC" });
    }

    public ISecret GetDatabaseSecret(Construct scope, string id, bool isMaster = false)
    {
        string secretName = isMaster ? "/database/sqlserver/master_secret" : "/database/sqlserver/appuser_secret";
        return Secret.FromSecretNameV2(scope, id, secretName);
    }

    public IHostedZone GetHostedZone(Construct scope, string id, string domainName = null, string vpcId = null)
    {
        domainName ??= this.GetConfigValue("DomainName").ToString() ?? "";
        if (domainName == "")
        {
            return null;
        }

        HostedZoneProviderProps props = vpcId == null
            ? new HostedZoneProviderProps { DomainName = domainName, PrivateZone = false }
            : new HostedZoneProviderProps { DomainName = domainName, PrivateZone = true, VpcId = vpcId };

        return HostedZone.FromLookup(scope, id, props);
    }
}
