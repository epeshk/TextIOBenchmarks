// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using BenchmarkDotNet.Running;
using PlainTextIOBenchmarks;

BenchmarkRunner.Run<InputBenchmark>();
// BenchmarkRunner.Run<OutputBenchmark>();
// BenchmarkRunner.Run<EchoBenchmark>();
// BenchmarkRunner.Run<DoubleInputBenchmark>();
// BenchmarkRunner.Run<DoubleOutputBenchmark>();
// BenchmarkRunner.Run<DotNetEchoBenchmark>();
// BenchmarkRunner.Run<DoubleEchoBenchmark>();


public static class OS
{
  public static Memory<byte> Run(string program, byte[] input)
  {
    var psi = new ProcessStartInfo(program)
    {
      UseShellExecute = false,
      RedirectStandardInput = true,
      RedirectStandardOutput = true,
      RedirectStandardError = false,
      CreateNoWindow = true,
      Environment = { {"DOTNET_TieredCompilation", "0"}, {"DOTNET_TieredPGO", "0"} }
    };

    var process = Process.Start(psi);
    ArgumentNullException.ThrowIfNull(process);

    using var ms = new MemoryStream(buffer);
    var reader = Task.Run(() => process.StandardOutput.BaseStream.CopyToAsync(ms));
    
    var stdin = process.StandardInput.BaseStream;
    stdin.Write(input);
    stdin.Flush();

    reader.GetAwaiter().GetResult();
    process.WaitForExit();
    return buffer.AsMemory(0, (int)ms.Position);
  }

  public static void Cmd(string commandLine, bool printOutputToConsole = false)
  {
    var psi = new ProcessStartInfo("cmd", $"/c \"{commandLine}\"")
    {
      UseShellExecute = false,
      RedirectStandardInput = false,
      RedirectStandardOutput = false,
      RedirectStandardError = false,
      CreateNoWindow = !printOutputToConsole
    };

    if (!printOutputToConsole)
      psi.RedirectStandardOutput = true;

    using var process = Process.Start(psi);
    ArgumentNullException.ThrowIfNull(process);
    if (psi.RedirectStandardOutput)
    {
      using var ms = new MemoryStream(buffer);
      process.StandardOutput.BaseStream.CopyTo(ms);
    }

    process.WaitForExit();
  }

  private static byte[] buffer = new byte[64 * 1024 * 1024];
}