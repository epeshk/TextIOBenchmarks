using System.Runtime.InteropServices;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Order;

[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
[MedianColumn, MinColumn, InProcess, RankColumn(NumeralSystem.Arabic)]
public class IOBenchmark
{
  private readonly bool withInput;
  public static byte[] input = GenerateInput();

  private static byte[] GenerateInput()
  {
    var ms = new MemoryStream();
    var writer = new StreamWriter(ms, Encoding.ASCII);
    var random = new Random(42);
    writer.Write(200000);
    writer.Write("\r\n");
    for (int i = 0; i < 200000; i++)
    {
      writer.Write($"{random.Next(100_000, 1_000_000)} {random.Next(100_000, 1_000_000)} {random.Next(100_000, 1_000_000)} {random.Next(100_000, 1_000_000)}");
      writer.Write("\r\n");
    }
    writer.Flush();

    return ms.ToArray();
  }
  
  public string Scenario { get; }
  //
  public string[] Cpp => Directory
    .GetFiles($"sources/{Scenario}")
    .Where(x => x.EndsWith(".cpp"))
    .Select(Path.GetFileNameWithoutExtension)
    .ToArray()!;

  public ICompiler[] Compilers { get; } = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
    ? new ICompiler[]
    {
      // new MSVC(32),
      new MSVC(64),
      // new GPlusPlus(32),
      new GPlusPlus(64)
    }
    : new ICompiler[]
    {
      new GPlusPlusLinux(64)
    };

  [ParamsSource(nameof(Cpp))] public string Program;
  [ParamsSource(nameof(Compilers))] public ICompiler Compiler;

  protected IOBenchmark(string scenario, bool withInput=true)
  {
    this.withInput = withInput;
    Scenario = scenario;
  }

  [GlobalSetup]
  public void Setup()
  {
    Compiler.Compile($"sources/{Scenario}/{Program}.cpp", $"{Scenario}_{Program}_{Compiler}.exe");

    if (Program == "empty")
      return;

    var actual = IntsInput();
    var expected = File.Exists($"sources/{Scenario}/answer")
      ? File.ReadAllBytes($"sources/{Scenario}/answer")
      : input;

    var actualString = Encoding.ASCII.GetString(actual.Span).Trim().Replace("\r\n", "\n");
    var expectedString = Encoding.ASCII.GetString(expected).Trim().Replace("\r\n", "\n");
    if (!string.Equals(expectedString, actualString))
      throw new Exception($"Wrong answer: {expectedString.Substring(0, Math.Min(32, expectedString.Length))} != {actualString.Substring(0, Math.Min(32, actualString.Length))}");
  }

  [Benchmark] public Memory<byte> IntsInput() => OS.Run(
    $"{Scenario}_{Program}_{Compiler}.exe",
    !withInput || Program == "empty"
      ? Array.Empty<byte>()
      : input);
}
