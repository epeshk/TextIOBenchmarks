using System.Diagnostics;

public interface ICompiler
{
  void Compile(string source, string target);
}


public class MSVC : ICompiler
{
  private readonly int bitness;

  public MSVC(int bitness)
  {
    this.bitness = bitness;
  }

  public void Compile(string source, string target) => OS.Cmd($"msvc{bitness}.bat {source} {target}", true);
  public override string ToString() => $"msvc{bitness}";
}
public class GPlusPlus : ICompiler
{
  private readonly int bitness;

  public GPlusPlus(int bitness)
  {
    this.bitness = bitness;
  }

  public void Compile(string source, string target)
  {
    var psi = new ProcessStartInfo($@"C:\msys64\mingw{bitness}.exe",
      $"g++ {source} -static -O2 -o {target}");
    using var process = Process.Start(psi);
    ArgumentNullException.ThrowIfNull(process);
    process.WaitForExit();
    Thread.Sleep(2000);
  }

  public override string ToString() => $"g++{bitness}";
}

public class GPlusPlusLinux : ICompiler
{
  private readonly int bitness;

  public GPlusPlusLinux(int bitness)
  {
    this.bitness = bitness;
  }

  public void Compile(string source, string target)
  {
    var psi = new ProcessStartInfo($@"g++",
      $"{source} -static -O2 -o {target}");
    using var process = Process.Start(psi);
    ArgumentNullException.ThrowIfNull(process);
    process.WaitForExit();
  }

  public override string ToString() => $"g++{bitness}";
}
