using Newtonsoft.Json;
using System.CodeDom;
using System.Text;

namespace TechQ.Core.Extensions;

public static class ConsoleExtensions
{
	public static object Dump(this object obj, string title = null, TextWriter output = null)
	{
		output ??= Console.Out;

		if (title != null)
			output.WriteLine($"\n{title}:");

		if (obj == null)
		{
			WriteLine("(null)", ConsoleColor.Cyan);
			return obj;
		}

		var type = obj.GetType();

		return obj switch
		{
			_ when type.IsSimpleType() => ((Func<object>)(() => { Console.Out.WriteLine($"{type.FullName} {obj}"); return obj; }))(),
			IDictionary<string, string> map => ((Func<object>)(() => { DumpDictionary(obj, output); return obj; }))(),
			_ => obj
		};
	}

	public static StringBuilder Dump<T>(this StringBuilder stringBuilder, string title = null, TextWriter output = null)
	{
		stringBuilder.ToString().Dump(title, output);
		return stringBuilder;
	}

	private static object DumpDictionary(object obj, TextWriter output)
	{
		output.WriteLine(JsonConvert.SerializeObject(obj, Formatting.Indented));
		return obj;
	}


	private static void WriteLine(string line, ConsoleColor color)
	{
		ConsoleColor originalColor = Console.ForegroundColor;

		try
		{
			Console.ForegroundColor = color;
			Console.WriteLine(line);
		}
		finally
		{
			Console.ForegroundColor = originalColor;
		}
	}

	private static void WriteLine(string line, ConsoleColor color, ConsoleColor backgroundColor)
	{
		ConsoleColor originalForeColor = Console.ForegroundColor;
		ConsoleColor originalBackground = Console.BackgroundColor;

		try
		{
			Console.ForegroundColor = color;
			Console.BackgroundColor = backgroundColor;

			Console.WriteLine(line);
		}
		finally
		{
			Console.ForegroundColor = originalForeColor;
			Console.BackgroundColor = originalBackground;
		}
	}
}
