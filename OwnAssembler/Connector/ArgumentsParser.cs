using System.Runtime.CompilerServices;

namespace Connector;

public static class ArgumentsParser
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static Dictionary<string, object> GetParameters(IReadOnlyList<string> args,
        IEnumerable<(string key, object value)> defaultParameters, string[] booleanParameters, string[] pathParameters)
    {
        var parameters = ParseArguments(args, booleanParameters, pathParameters);

        foreach (var pair in defaultParameters.Where(pair => !parameters.ContainsKey(pair.key)))
            parameters.Add(pair.key, pair.value);

        return parameters;
    }

    private static Dictionary<string, object> ParseArguments(IReadOnlyList<string> args, string[] booleanParameters,
        string[] pathParameters)
    {
        var parameters = new Dictionary<string, object>();

        
        for (var index = 0; index < args.Count - 1; index++)
        {
            var arg = args[index].ToLower();

            index++;

            object value = !booleanParameters.Contains(arg) ? args[index] : args[index].ToLower() == "true";
            if (pathParameters.Contains(arg))
            {
                var str = value as string ?? throw new Exception("Parameter is not a path (string)");
                if (!Path.IsPathFullyQualified(str)) str = Path.GetFullPath(str);
                value = str;
            }

            parameters.Add(arg, value);
        }

        return parameters;
    }
}