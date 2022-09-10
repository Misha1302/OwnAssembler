using System.Reflection;
using System.Runtime.CompilerServices;

namespace OwnAssembler.Assembler.LowLevelCommands.Dlls;

public static class Dlls
{
    private const string NAMESPACE_NAME = "AssemblerDllNamespace";
    private const string CLASS_NAME = "AssemblerDllClass";

    private static readonly Dictionary<string, Assembly> dllsDictionary = new();
    private static readonly Dictionary<string, Type> classes = new();

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static void AddDll(string dllPath)
    {
        dllsDictionary.Add(dllPath, Assembly.LoadFrom(dllPath));

        var classType = dllsDictionary[dllPath].GetType($"{NAMESPACE_NAME}.{CLASS_NAME}") ??
                        throw new Exception($"{CLASS_NAME} not found");

        classes.Add(dllPath, classType);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static MethodInfo GetMethodFromDll(string dllPath, string methodName)
    {
        var method = classes[dllPath].GetMethod(methodName) ??
                     throw new Exception(
                         $"method: <{methodName}> in class: <{CLASS_NAME}> in dll: <{dllPath}> not found");
        return method;
    }
}