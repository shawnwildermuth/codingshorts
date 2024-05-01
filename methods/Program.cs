using static System.Console;

var maths = new Maths();

var addFunction = Maths.Add;

var doAddition = () => {
  return Maths.Add(6,6) == 12;
};

var doAction = (string foo) => WriteLine(foo);

doAction("Hello World");

WriteLine(addFunction(2,3));
WriteLine(doAddition());

Maths.RunThis(() => true);

public class Maths
{
  public static int Add(int x , int y) => x + y;

  public static bool RunThis(Func<bool> operation) => operation();
}