namespace PlainTextIOBenchmarks;


public class InputBenchmark : IOBenchmark
{
  public InputBenchmark() : base("input")
  {
  }
}
public class DoubleInputBenchmark : IOBenchmark
{
  public DoubleInputBenchmark() : base("input_d")
  {
  }
}
public class DoubleEchoBenchmark : IOBenchmark
{
  public DoubleEchoBenchmark() : base("echo_d")
  {
  }
}
public class EchoBenchmark : IOBenchmark
{
  public EchoBenchmark() : base("echo")
  {
  }
}
