using static System.Console;

var math = new Math();
var m = (IAddition)math;
var s = (ISubtraction)math;
WriteLine(m.Add(4, 5));
WriteLine(s.Subtract(4, 5));

public interface IAddition
{
  int Add(int x, int y) => x + y;
}

public interface ISubtraction
{ 
  int Subtract(int x, int y) => x - y;
}

public class Math : Object, IAddition, ISubtraction
{
  public int Add(int x, int y) => (x + y) * 10;
  public int Subtract(int x, int y) => (x - y) * 10;
}