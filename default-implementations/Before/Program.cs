using static System.Console;

var m = new Math();
WriteLine(m.Add(4, 5));
WriteLine(m.Subtract(4, 5));

public class Math
{
  public int Add(int x, int y) => x + y;
  public int Subtract(int x, int y) => x - y;
}