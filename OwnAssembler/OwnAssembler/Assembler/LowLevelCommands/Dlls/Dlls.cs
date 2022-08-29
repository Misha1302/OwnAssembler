using System.Reflection;
using System.Runtime.CompilerServices;

namespace OwnAssembler.Assembler.LowLevelCommands.Dlls;

public static class Dlls
{
    private const string NamespaceName = "AssemblerDllNamespace";
    private const string ClassName = "AssemblerDllClass";

    private static readonly Dictionary<string, Assembly> DllsDictionary = new();
    private static readonly Dictionary<string, Type> Classes = new();

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static void AddDll(string dllPath)
    {
        DllsDictionary.Add(dllPath, Assembly.LoadFrom(dllPath));

        var classType = DllsDictionary[dllPath].GetType($"{NamespaceName}.{ClassName}") ??
                        throw new Exception($"{ClassName} not found");

        Classes.Add(dllPath, classType);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static MethodInfo GetMethodFromDll(string dllPath, string methodName)
    {
        var method = Classes[dllPath].GetMethod(methodName) ??
                     throw new Exception(
                         $"method: <{methodName}> in class: <{ClassName}> in dll: <{dllPath}> not found");
        return method;
    }
}