rem Scaffold-DbContext "Data Source=databaseinstancestack-sqlserverinstancebce7a1d3-alel7jl7vygl.cf642secgqm9.us-west-2.rds.amazonaws.com,1444;User ID=rdsmaster;Password=PkoGSBW.7_gaIcP2tFaPp0X,CZ4sh=Fj;Initial Catalog=InsuranceDB;TrustServerCertificate=True;MultipleActiveResultSets=true;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

rem dotnet tool update --global dotnet-ef

if exist IInsuranceDbContext.cs del IInsuranceDbContext.cs
if exist extensions.cs del extensions.cs
if exist Models rd /q /s Models

dotnet clean

dotnet ef dbcontext scaffold "SERVER=databaseinstancestack-sqlserverinstancebce7a1d3-alel7jl7vygl.cf642secgqm9.us-west-2.rds.amazonaws.com,1444;USER ID=rdsmaster;PASSWORD=HB31Fes7f=SuWLNnfiU2.pnC6B-AUR=n;INITIAL CATALOG=InsuranceDb;TrustServerCertificate=True;MultipleActiveResultSets=true;" Microsoft.EntityFrameworkCore.SqlServer -o Models

git checkout IInsuranceDbContext.cs
git checkout extensions.cs
git checkout Models/InsuranceDbContext.Customization.cs
git checkout Models/Driver.Customization.cs
git checkout Models/Vehicle.Customization.cs

