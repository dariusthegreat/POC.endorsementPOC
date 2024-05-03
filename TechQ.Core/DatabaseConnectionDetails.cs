namespace TechQ.Core;

public class DatabaseConnectionDetails
{
	public string Database	 { get; set; }
	public string Server     { get;     set; }
	public int    PortNumber { get;     set; }
	public string UserName   { get;     set; }
	public string Password   { get;     set; }
	
	public override string ToString() => CreateConnectionString();

	public string CreateConnectionString() => $"Server={Server},{PortNumber};User ID={UserName};Password={Password};Database={Database};TrustServerCertificate=True;MultipleActiveResultSets=true;";

	public static implicit operator string(DatabaseConnectionDetails connectionDetails) => connectionDetails.CreateConnectionString();
}