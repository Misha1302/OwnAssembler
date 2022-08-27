using System.Runtime.CompilerServices;
using OwnAssembler;
using OwnAssembler.CentralProcessingUnit;

Main();

[MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
void Main()
{
    Cpu.StartNewApplication("Code.asmEasy");
}