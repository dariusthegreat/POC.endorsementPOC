rem Scaffold-DbContext "Data Source=databaseinstancestack-sqlserverinstancebce7a1d3-alel7jl7vygl.cf642secgqm9.us-west-2.rds.amazonaws.com,1444;User ID=rdsmaster;Password=PkoGSBW.7_gaIcP2tFaPp0X,CZ4sh=Fj;Initial Catalog=InsuranceDB;TrustServerCertificate=True;MultipleActiveResultSets=true;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models


if exist IInsuranceDbContext.cs del IInsuranceDbContext.cs
if exist Models rd /q /s Models

dotnet ef dbcontext scaffold "Server=databaseinstancestack-sqlserverinstancebce7a1d3-alel7jl7vygl.cf642secgqm9.us-west-2.rds.amazonaws.com,1444;User ID=appuser;Password=tnJ7A3W=DHC3xtPWNFbx_wFrmZdYtlT^;Database=InsuranceDB;TrustServerCertificate=True;MultipleActiveResultSets=true;" Microsoft.EntityFrameworkCore.SqlServer -o Models

git checkout IInsuranceDbContext.cs
git checkout Models/InsuranceDbContext.Customization.cs

