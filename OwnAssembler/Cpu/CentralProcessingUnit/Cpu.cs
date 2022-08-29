using System.Runtime.CompilerServices;
using System.Text;
using Connector;

namespace Cpu.CentralProcessingUnit;

public static class Cpu
{
    private static readonly List<CpuApplication> Applications = new();

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static Cpu()
    {
        Console.OutputEncoding = Encoding.Unicode;
        Console.InputEncoding = Encoding.Unicode;

        new Thread(StartCpuLifeCycle).Start();
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static void StartNewApplication(ByteCode byteCode)
    {
        var cpuApplication = new CpuApplication(byteCode);
        cpuApplication.OnApplicationStart();
        Applications.Add(cpuApplication);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private static void StartCpuLifeCycle()
    {
        while (true)
            CpuTakeOneStep();
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private static void CpuTakeOneStep()
    {
        foreach (var application in Applications.ToArray())
            application.ApplicationTakeOneStep();
    }
}