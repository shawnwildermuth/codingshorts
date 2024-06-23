using System;

public class Program
{
  public static void Main()
  {
    string[] array = new string[5];
    array[0] = "Hello";
    array[1] = "World";

    foreach (string message in array)
    {
      Console.WriteLine(message);
    }
  }
}