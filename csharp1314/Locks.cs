namespace CSharpNew;

public class LockExample
{
  //object l = new();

  Lock l = new Lock();

  int counter = 0;
  public int Count { get => counter; }

  public void Increment()
  {
    lock (l)
    {
      counter++;
    }
  }
}