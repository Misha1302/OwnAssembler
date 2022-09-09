using System.Runtime.CompilerServices;
using System.Text;
using Connector;

namespace Cpu.CentralProcessingUnit;

public static class Cpu
{
    public static readonly Dictionary<int, CpuApplication> Applications = new();
    private static int _applicationIndex;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static Cpu()
    {
        Console.OutputEncoding = Encoding.Unicode;
        Console.InputEncoding = Encoding.Unicode;

        File.WriteAllText("stackLog.txt", "");
        File.WriteAllText("ramLog.txt", "");

        new Thread(StartCpuLifeCycle).Start();
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static void StartNewApplication(ByteCode byteCode, bool debugMode)
    {
        var cpuApplication = new CpuApplication(byteCode, _applicationIndex, debugMode);
        Applications.Add(_applicationIndex, cpuApplication);
        cpuApplication.OnApplicationStart();
        _applicationIndex++;
    }

    public static void KillApplication(int applicationsIndex)
    {
        Applications.Remove(applicationsIndex);
    }


    // ReSharper disable once FunctionNeverReturns
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private static void StartCpuLifeCycle()
    {
        while (true)
            CpuTakeOneStep();
    }


    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private static void CpuTakeOneStep()
    {
        var applications = Applications.ToArray();
        foreach (var application in applications)
            application.Value.ApplicationTakeOneStep();
    }
}