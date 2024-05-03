using System.Text;

namespace Poc.Console;

public class DatabaseConnectionDetails
{
	public string Password { get; set; }
	public string Engine { get; set; }
	public int Port { get; set; }
	public string DbInstanceIdentifier { get; set; }
	public string Host { get; set; }
	public string Username { get; set; }

	public string AsConnectionString() => $"Server={Host},{Port};Database=InsuranceDB;User Id={Username};Password={Password};";

	public string ToConnectionString() => new StringBuilder()
		.Append($"Server={Host},{Port};")
		.Append($"Database={DbInstanceIdentifier};")
		.Append($"User Id={Username};")
		.Append($"Password={Password};")
		.Append("Trusted_Connection=True;")
		.ToString();

	public static implicit operator string(DatabaseConnectionDetails details) => details.ToConnectionString();
}
