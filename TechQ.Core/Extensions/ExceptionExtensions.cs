using System.Text;

namespace TechQ.Core.Extensions;

public static class ExceptionExtensions
{
	public static string Format(this Exception exception, ExceptionFields fields = ExceptionFields.All)
	{
		var stringBuilder = new StringBuilder();
		exception.Format(stringBuilder, fields);
		return stringBuilder.ToString();
	}

	public static void Format(this Exception exception, StringBuilder stringBuilder, ExceptionFields fields = ExceptionFields.All)
	{
		using var stringWriter = new StringWriter(stringBuilder);
		exception.Format(stringWriter, fields);
	}

	public static void Format(this Exception exception, TextWriter output, ExceptionFields fields = ExceptionFields.All)
	{
		foreach(var e in exception.IterateThroughInnerExceptionsChain((fields & ExceptionFields.InnerException)==ExceptionFields.InnerException))
		{
			if(e!=exception)
			{
				output.WriteLine("\n");
				output.WriteLine(new string('-', 120));
				output.WriteLine("Inner exception");
			}
			
			e.FormatSingleException(output, fields);
		}
	}

	public static void Format(this Exception exception, TextWriter output, FormattingOptions options) => exception.Format(output, options.Fields);
	
	
	public static string FormatSingleException(this Exception exception)
	{
		var stringBuilder = new StringBuilder();
		exception.FormatSingleException(stringBuilder);
		return stringBuilder.ToString();
	}

	public static void FormatSingleException(this Exception exception, StringBuilder output)
	{
		using var stringWriter = new StringWriter(output);
		exception.FormatSingleException(stringWriter);
	}
	
	public static void FormatSingleException(this Exception exception, TextWriter output, FormattingOptions options) => exception.FormatSingleException(output, options?.Fields ?? throw new ArgumentNullException(nameof(options)) /* irony!! */);
	
	public static void FormatSingleException(this Exception exception, TextWriter output, ExceptionFields fields = ExceptionFields.All)
	{
		output.WriteLine($"{exception.GetType()} caught: {exception.Message}\n");	output.WriteLine($"exception type: {exception.GetType().FullName}");

		FormattingOptions options = fields;

		

		if ((fields & ExceptionFields.Source	) == ExceptionFields.Source		) 	output.WriteLine($"Source: {exception.Source}");
		if ((fields & ExceptionFields.TargetSite) == ExceptionFields.TargetSite	) 	output.WriteLine($"TargetSite: {exception.TargetSite}");
		if ((fields & ExceptionFields.HelpLink	) == ExceptionFields.HelpLink	) 	output.WriteLine($"HelpLink: {exception.HelpLink}");
		if ((fields & ExceptionFields.HResult	) == ExceptionFields.HResult	) 	output.WriteLine($"HResult: {exception.HResult}");

		if (options.IncludeStacktrace)
		{
			output.WriteLine("\nStacktrace:");
			output.WriteLine(exception.StackTrace);
		}

		if(options.IncludeExceptionData && exception?.Data.Count > 0)
		{
			output.WriteLine($"\nException Data: ({exception.Data.Count} {(exception.Data.Count == 1 ? "entry" : "entries")})");

			foreach (var key in exception.Data.Keys)
				output.WriteLine($"exception[\"{key}\"]: {exception.Data[key]}");
		}
	}

	public static IEnumerable<Exception> IterateThroughInnerExceptionsChain(this Exception exception, bool returnEntireChain=true)
	{
		for (var e = exception; e != null; e = e.InnerException)
		{
			yield return e;
			if (!returnEntireChain) yield break;
		}
	}

	public record FormattingOptions(bool IncludeStacktrace		= true, 
									bool IncludeExceptionData	= true, 
									bool IncludeInnerExceptions	= true,
									bool IncludeSource			= true,
									bool IncludeTargetSite		= true,
									bool IncludeHelpLink		= true,
									bool IncludeHResult			= true)
	{
		public ExceptionFields Fields => 		(IncludeStacktrace			? 	ExceptionFields.Stacktrace 		: ExceptionFields.None) 
											|	(IncludeExceptionData		? 	ExceptionFields.Data 			: ExceptionFields.None) 
											|	(IncludeInnerExceptions 	? 	ExceptionFields.InnerException 	: ExceptionFields.None)	
											|	(IncludeSource				?	ExceptionFields.Source			: ExceptionFields.None)
											|	(IncludeTargetSite			?	ExceptionFields.TargetSite		: ExceptionFields.None)
											|	(IncludeHelpLink			?	ExceptionFields.HelpLink		: ExceptionFields.None)
											|	(IncludeHResult				?	ExceptionFields.HResult			: ExceptionFields.None);

		public static FormattingOptions Default => new(true, true, true, true, true, true, true);

		
		public static implicit operator FormattingOptions(ExceptionFields fields)	=> new(	(fields & ExceptionFields.Stacktrace	) == ExceptionFields.Stacktrace		,
																							(fields & ExceptionFields.Data			) == ExceptionFields.Data			,
																							(fields & ExceptionFields.InnerException) == ExceptionFields.InnerException ,
																							(fields & ExceptionFields.Source		) == ExceptionFields.Source			,
																							(fields & ExceptionFields.TargetSite	) == ExceptionFields.TargetSite		,
																							(fields & ExceptionFields.HelpLink		) == ExceptionFields.HelpLink		,
																							(fields & ExceptionFields.HResult		) == ExceptionFields.HResult		);
	}
}



[Flags]
public enum ExceptionFields
{
	None 			= 0,
	HelpLink 		= 1 << 0,
	HResult 		= 1 << 1,
	Source 			= 1 << 2,
	TargetSite 		= 1 << 3,
	Stacktrace 		= 1 << 4,
	Data 			= 1 << 5,
	InnerException 	= 1 << 6,
	All 			= (1 << 7) - 1
}
