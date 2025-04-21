namespace CSharpNew;

public class LockExample
{
  int counter = 0;
  public int Count { get => counter; }

  public void Increment()
  {
    counter++;
  }
}